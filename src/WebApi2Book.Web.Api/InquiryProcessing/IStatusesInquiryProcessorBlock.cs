// IStatusesInquiryProcessorBlock.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public interface IStatusesInquiryProcessorBlock
    {
        IAllStatusesInquiryProcessor AllStatusesInquiryProcessor { get; }
        IStatusByIdInquiryProcessor StatusByIdInquiryProcessor { get; }
    }
}