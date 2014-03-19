// StatusByIdInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.Exceptions;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class StatusByIdInquiryProcessor : IStatusByIdInquiryProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IStatusByIdQueryProcessor _queryProcessor;
        private readonly IStatusLinkService _statusLinkService;

        public StatusByIdInquiryProcessor(IStatusByIdQueryProcessor queryProcessor, IAutoMapper autoMapper,
            IStatusLinkService statusLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _statusLinkService = statusLinkService;
        }

        public Status GetStatus(long statusId)
        {
            var statusEntity = _queryProcessor.GetStatus(statusId);
            if (statusEntity == null)
            {
                throw new RootObjectNotFoundException("Status not found");
            }

            var status = _autoMapper.Map<Status>(statusEntity);

            _statusLinkService.AddLinks(status);

            return status;
        }
    }
}