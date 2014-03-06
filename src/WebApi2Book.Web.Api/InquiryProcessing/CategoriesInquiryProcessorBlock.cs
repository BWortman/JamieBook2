// CategoriesInquiryProcessorBlock.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class CategoriesInquiryProcessorBlock : ICategoriesInquiryProcessorBlock
    {
        public CategoriesInquiryProcessorBlock(ICategoryByIdInquiryProcessor categoryByIdInquiryProcessor,
            IAllCategoriesInquiryProcessor allCategoriesInquiryProcessor)
        {
            CategoryByIdInquiryProcessor = categoryByIdInquiryProcessor;
            AllCategoriesInquiryProcessor = allCategoriesInquiryProcessor;
        }

        public IAllCategoriesInquiryProcessor AllCategoriesInquiryProcessor { get; private set; }
        public ICategoryByIdInquiryProcessor CategoryByIdInquiryProcessor { get; private set; }
    }
}