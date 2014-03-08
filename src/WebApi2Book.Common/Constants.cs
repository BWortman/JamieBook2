// Constants.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Common
{
    public static class Constants
    {
        public const string WildcardSearchCharacter = "*";

        public const string NotAvailable = "n/a";

        public static class MediaTypeNames
        {
            public const string ApplicationXml = "application/xml";
            public const string TextXml = "text/xml";
            public const string ApplicationJson = "application/json";
            public const string TextJson = "text/json";
        }

        public static class Paging
        {
            public const int MinPageSize = 1;
            public const int MinPageNumber = 1;
            public const int DefaultPageNumber = 1;
        }

        public static class CommonParameterNames
        {
            public const string PageNumber = "pageNumber";
            public const string PageSize = "pageSize";
        }

        public static class CommonLinkTitles
        {
            public const string Self = "Self";
            public const string PreviousPage = "PreviousPage";
            public const string NextPage = "NextPage";
        }

        public static class CommonLinkRelValues
        {
            public const string Self = "self";
            public const string All = "all";
        }

        public static class CommonRoutingDefinitions
        {
            public const string ApiSegmentName = "api";
            public const string ApiVersionSegmentName = "apiVersion";
            public const string CurrentApiVersion = "v1";
        }
    }
}