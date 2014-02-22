// LegacyMessageProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using WebApi2Book.Web.Legacy.ProcessingStrategies;

namespace WebApi2Book.Web.Legacy
{
    public class LegacyMessageProcessor
    {
        private LegacyMessageParser _legacyMessageParser;
        private IEnumerable<ILegacyMessageProcessingStrategy> _legacyMessageProcessingStrategies;

        public LegacyMessageProcessor()
        {
            //  todo: inject
            _legacyMessageParser = new LegacyMessageParser();
            _legacyMessageProcessingStrategies =
                new List<ILegacyMessageProcessingStrategy> {new GetCategoriesMessageProcessingStrategy()};
        }

        public virtual LegacyResponse ProcessLegacyMessage(XDocument request)
        {
            var operationElement = _legacyMessageParser.GetOperationElement(request);
            var opName = operationElement.Name.LocalName;

            foreach (var legacyMessageProcessingStrategy in _legacyMessageProcessingStrategies)
            {
                if (legacyMessageProcessingStrategy.CanProcess(opName))
                {
                    var legacyResponse = new LegacyResponse
                    {
                        Request = request,
                        ProcessingResult = legacyMessageProcessingStrategy.Execute(operationElement)
                    };
                    return legacyResponse;
                }
            }

            throw new NotSupportedException(opName);
        }
    }
}