// AllUsersInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.SqlServer.DataTransferObjects;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class AllUsersInquiryProcessor : IAllUsersInquiryProcessor
    {
        public const string QueryStringFormat = "pagenumber={0}&pagesize={1}";
        private readonly IAutoMapper _autoMapper;
        private readonly ICommonLinkService _commonLinkService;
        private readonly IAllUsersQueryProcessor _queryProcessor;
        private readonly IUserLinkService _userLinkService;

        public AllUsersInquiryProcessor(IAllUsersQueryProcessor queryProcessor, IAutoMapper autoMapper,
            IUserLinkService userLinkService, ICommonLinkService commonLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _userLinkService = userLinkService;
            _commonLinkService = commonLinkService;
        }

        public UsersInquiryResponse GetUsers(AllUsersDataRequest requestInfo)
        {
            var queryResult = _queryProcessor.GetUsers(requestInfo);

            var users = GetUsers(queryResult.QueriedItems);

            var inquiryResponse = new UsersInquiryResponse
            {
                Users = users,
                PageCount = queryResult.TotalPageCount,
                PageNumber = requestInfo.PageNumber,
                PageSize = requestInfo.PageSize
            };

            AddLinksToInquiryResponse(inquiryResponse);

            return inquiryResponse;
        }

        public virtual void AddLinksToInquiryResponse(UsersInquiryResponse inquiryResponse)
        {
            inquiryResponse.AddLink(_userLinkService.GetAllUsersLink());

            _commonLinkService.AddPageLinks(inquiryResponse, GetCurrentPageQueryString(inquiryResponse),
                GetPreviousPageQueryString(inquiryResponse),
                GetNextPageQueryString(inquiryResponse));
        }

        public virtual IEnumerable<User> GetUsers(IEnumerable<Data.Entities.User> userEntities)
        {
            var users = userEntities.Select(x => _autoMapper.Map<User>(x)).ToList();

            users.ForEach(x => _userLinkService.AddSelfLink(x));

            return users;
        }

        public virtual string GetCurrentPageQueryString(UsersInquiryResponse inquiryResponse)
        {
            return
                string.Format(QueryStringFormat,
                    inquiryResponse.PageNumber,
                    inquiryResponse.PageSize);
        }

        public virtual string GetPreviousPageQueryString(UsersInquiryResponse inquiryResponse)
        {
            return
                string.Format(QueryStringFormat,
                    inquiryResponse.PageNumber - 1,
                    inquiryResponse.PageSize);
        }

        public virtual string GetNextPageQueryString(UsersInquiryResponse inquiryResponse)
        {
            return string.Format(QueryStringFormat,
                inquiryResponse.PageNumber + 1,
                inquiryResponse.PageSize);
        }
    }
}