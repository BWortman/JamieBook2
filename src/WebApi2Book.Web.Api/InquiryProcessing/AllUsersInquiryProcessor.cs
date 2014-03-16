// AllUsersInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class AllUsersInquiryProcessor : IAllUsersInquiryProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IAllUsersQueryProcessor _queryProcessor;
        private readonly IUserLinkService _userLinkService;

        public AllUsersInquiryProcessor(IAllUsersQueryProcessor queryProcessor, IAutoMapper autoMapper,
            IUserLinkService userLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _userLinkService = userLinkService;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _queryProcessor.GetUsers();

            var modelUsers = users.Select(x => _autoMapper.Map<User>(x)).ToList();

            modelUsers.ForEach(x => _userLinkService.AddLinks(x));

            return modelUsers;
        }
    }
}