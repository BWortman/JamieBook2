﻿// ICategoryByIdQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public interface ICategoryByIdQueryProcessor
    {
        Category GetCategory(long categoryId);
    }
}