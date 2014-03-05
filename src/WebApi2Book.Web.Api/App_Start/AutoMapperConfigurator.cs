// AutoMapperConfigurator.cs
// Copyright fiserv 2014.

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi2Book.Common;

namespace WebApi2Book.Web.Api
{
    public class AutoMapperConfigurator
    {
        public void Configure(IEnumerable<IAutoMapperTypeConfigurator> autoMapperTypeConfigurations)
        {
            autoMapperTypeConfigurations.ToList().ForEach(x => x.Configure());

            Mapper.AssertConfigurationIsValid();
        }
    }
}