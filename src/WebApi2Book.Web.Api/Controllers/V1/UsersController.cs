// UsersController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Web.Http;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("users")]
    [UnitOfWorkActionFilter]
    public class UsersController : ApiController
    {
        private readonly IAllUsersInquiryProcessor _allUsersInquiryProcessor;
        private readonly IUserByIdInquiryProcessor _userByIdInquiryProcessor;

        public UsersController(IAllUsersInquiryProcessor allUsersInquiryProcessor, IUserByIdInquiryProcessor userByIdInquiryProcessor)
        {
            _allUsersInquiryProcessor = allUsersInquiryProcessor;
            _userByIdInquiryProcessor = userByIdInquiryProcessor;
        }

        [Route("", Name = "GetUsersRoute")]
        public IEnumerable<User> GetUsers()
        {
            var users = _allUsersInquiryProcessor.GetUsers();
            return users;
        }

        [Route("{id:long}", Name = "GetUserRoute")]
        public User GetUser(long id)
        {
            var user = _userByIdInquiryProcessor.GetUser(id);
            return user;
        }
    }
}