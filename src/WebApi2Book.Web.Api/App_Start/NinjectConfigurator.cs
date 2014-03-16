// NinjectConfigurator.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Security.Principal;
using System.Threading;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net.Config;
using NHibernate;
using NHibernate.Context;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using WebApi2Book.Common.Logging;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.SqlServer;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.AutoMappingConfiguration;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.LegacyProcessing;
using WebApi2Book.Web.Api.LegacyProcessing.ProcessingStrategies;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Security;

namespace WebApi2Book.Web.Api
{
    /// <summary>
    ///     Class used to set up the Ninject DI container.
    /// </summary>
    public class NinjectConfigurator
    {
        /// <summary>
        ///     Entry method used by caller to configure the given
        ///     container with all of this application's
        ///     dependencies.
        /// </summary>
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }

        private void AddBindings(IKernel container)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            // IMPORTANT NOTE! *Never* configure a type as singleton if it depends upon IUnitOfWork!!! This is because
            // IUnitOfWork is disposed of at the end of every request. For the same reason, make sure that types
            // dependent upon such types aren't configured as singleton.
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ConfigureLog4net(container);
            ConfigureNHibernate(container);
            ConfigureDependenciesOnlyUsedForLegacyProcessing(container);
            ConfigureAutoMapper(container);

            container.Bind<IUserSession>().ToMethod(CreateUserSession).InRequestScope();

            container.Bind<IStatusesInquiryProcessorBlock>().To<StatusesInquiryProcessorBlock>();
            container.Bind<IAllStatusesInquiryProcessor>().To<AllStatusesInquiryProcessor>();
            container.Bind<IAllStatusesQueryProcessor>().To<AllStatusesQueryProcessor>();
            container.Bind<IStatusByIdInquiryProcessor>().To<StatusByIdInquiryProcessor>();
            container.Bind<IStatusByIdQueryProcessor>().To<StatusByIdQueryProcessor>();
            container.Bind<IStatusLinkService>().To<StatusLinkService>();

            container.Bind<IAllUsersInquiryProcessor>().To<AllUsersInquiryProcessor>();
            container.Bind<IAllUsersQueryProcessor>().To<AllUsersQueryProcessor>();
            container.Bind<IUserByIdInquiryProcessor>().To<UserByIdInquiryProcessor>();
            container.Bind<IUserByIdQueryProcessor>().To<UserByIdQueryProcessor>();
            container.Bind<IUserLinkService>().To<UserLinkService>();

            container.Bind<ITasksMaintenanceProcessorBlock>().To<TasksMaintenanceProcessorBlock>();
            container.Bind<ITaskAddingProcessor>().To<TaskAddingProcessor>();
            container.Bind<IAllTasksInquiryProcessor>().To<AllTasksInquiryProcessor>();
            container.Bind<IAllTasksQueryProcessor>().To<AllTasksQueryProcessor>();
            container.Bind<ITaskByIdInquiryProcessor>().To<TaskByIdInquiryProcessor>();
            container.Bind<ITaskByIdQueryProcessor>().To<TaskByIdQueryProcessor>();
            container.Bind<ITaskLinkService>().To<TaskLinkService>();

        }

        private void ConfigureAutoMapper(IKernel container)
        {
            container.Bind<IAutoMapper>().To<AutoMapperAdapter>().InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>().To<StatusEntityToStatusModelAutoMapperTypeConfigurator>();
            container.Bind<IAutoMapperTypeConfigurator>().To<StatusModelToStatusEntityAutoMapperTypeConfigurator>();
            container.Bind<IAutoMapperTypeConfigurator>().To<UserEntityToUserModelAutoMapperTypeConfigurator>();
            container.Bind<IAutoMapperTypeConfigurator>().To<UserModelToUserEntityAutoMapperTypeConfigurator>();
            container.Bind<IAutoMapperTypeConfigurator>().To<TaskEntityToTaskModelAutoMapperTypeConfigurator>();
            container.Bind<IAutoMapperTypeConfigurator>().To<TaskModelToTaskEntityAutoMapperTypeConfigurator>();
        }

        private IUserSession CreateUserSession(IContext arg)
        {
            var requestingUri = HttpContext.Current.Request.Url;
            var userSession = new UserSession(Thread.CurrentPrincipal as GenericPrincipal, requestingUri);
            return userSession;
        }

        private void ConfigureNHibernate(IKernel container)
        {
            var sessionFactory = Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2008.ConnectionString(
                        c => c.FromConnectionStringWithKey("WebApi2BookDb")))
                .CurrentSessionContext("web")
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SqlCommandFactory>())
                .BuildSessionFactory();

            container.Bind<ISessionFactory>().ToConstant(sessionFactory);
            container.Bind<ISession>().ToMethod(CreateSession);
            container.Bind<ICurrentSessionContextAdapter>().To<CurrentSessionContextAdapter>();
            container.Bind<IActionTransactionHelper>().To<ActionTransactionHelper>();
            container.Bind<ISqlCommandFactory>().To<SqlCommandFactory>();
        }

        private ISession CreateSession(IContext context)
        {
            var sessionFactory = context.Kernel.Get<ISessionFactory>();
            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                var session = sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }

            return sessionFactory.GetCurrentSession();
        }

        private void ConfigureDependenciesOnlyUsedForLegacyProcessing(IKernel container)
        {
            container.Bind<ILegacyMessageProcessor>().To<LegacyMessageProcessor>();
            container.Bind<ILegacyMessageParser>().To<LegacyMessageParser>().InSingletonScope();
            container.Bind<ILegacyMessageTypeFormatter>().To<LegacyMessageTypeFormatter>().InSingletonScope();

            container.Bind<ILegacyMessageProcessingStrategy>().To<GetStatusesMessageProcessingStrategy>();
            container.Bind<ILegacyMessageProcessingStrategy>().To<GetStatusByIdMessageProcessingStrategy>();
        }

        private void ConfigureLog4net(IKernel container)
        {
            XmlConfigurator.Configure();

            var logManager = new LogManagerAdapter();
            container.Bind<ILogManager>().ToConstant(logManager);
        }
    }
}