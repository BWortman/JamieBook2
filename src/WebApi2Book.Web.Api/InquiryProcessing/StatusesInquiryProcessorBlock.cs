// StatusesInquiryProcessorBlock.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class StatusesInquiryProcessorBlock : IStatusesInquiryProcessorBlock
    {
        public StatusesInquiryProcessorBlock(IStatusByIdInquiryProcessor statusByIdInquiryProcessor,
            IAllStatusesInquiryProcessor allStatusesInquiryProcessor)
        {
            StatusByIdInquiryProcessor = statusByIdInquiryProcessor;
            AllStatusesInquiryProcessor = allStatusesInquiryProcessor;
        }

        public IAllStatusesInquiryProcessor AllStatusesInquiryProcessor { get; private set; }
        public IStatusByIdInquiryProcessor StatusByIdInquiryProcessor { get; private set; }
    }
}