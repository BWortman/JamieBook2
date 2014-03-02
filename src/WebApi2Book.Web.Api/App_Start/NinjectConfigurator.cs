// NinjectConfigurator.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Security.Principal;
using System.Threading;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net.Config;
using NHibernate;
using NHibernate.Context;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using WebApi2Book.Common.Logging;
using WebApi2Book.Data.SqlServer;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Security;
using WebApi2Book.Web.Legacy;
using WebApi2Book.Web.Legacy.ProcessingStrategies;

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

            container.Bind<IUserSession>().ToMethod(CreateUserSession).InRequestScope();

            container.Bind<IActionTransactionHelper>().To<ActionTransactionHelper>();

            container.Bind<ISqlCommandFactory>().To<SqlCommandFactory>();
        }

        private IUserSession CreateUserSession(IContext arg)
        {
            return new UserSession(Thread.CurrentPrincipal as GenericPrincipal);
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

            container.Bind<ILegacyMessageProcessingStrategy>().To<GetCategoriesMessageProcessingStrategy>();
            container.Bind<ILegacyMessageProcessingStrategy>().To<GetCategoryByIdMessageProcessingStrategy>();
        }

        private void ConfigureLog4net(IKernel container)
        {
            XmlConfigurator.Configure();

            var logManager = new LogManagerAdapter();
            container.Bind<ILogManager>().ToConstant(logManager);
        }
    }
}