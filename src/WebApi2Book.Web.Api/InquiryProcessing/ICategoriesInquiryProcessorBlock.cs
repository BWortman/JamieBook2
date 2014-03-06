// ICategoriesInquiryProcessorBlock.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public interface ICategoriesInquiryProcessorBlock
    {
        IAllCategoriesInquiryProcessor AllCategoriesInquiryProcessor { get; }
        ICategoryByIdInquiryProcessor CategoryByIdInquiryProcessor { get; }
    }
}