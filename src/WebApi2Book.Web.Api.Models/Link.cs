// Link.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Web.Api.Models
{
    public class Link
    {
        public string Title { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
    }
}