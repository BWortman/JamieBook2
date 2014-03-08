// ILocationContaining.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;

namespace WebApi2Book.Web.Api.Models
{
    public interface ILocationContaining
    {
        Uri Location { get; }
    }
}