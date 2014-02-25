// ILegacyMessageProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Xml.Linq;

namespace WebApi2Book.Web.Legacy
{
    public interface ILegacyMessageProcessor
    {
        LegacyResponse ProcessLegacyMessage(XDocument request);
    }
}