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

        public static class StandardParameterNames
        {
            public const string PageNumber = "pageNumber";
            public const string PageSize = "pageSize";
        }
    }
}