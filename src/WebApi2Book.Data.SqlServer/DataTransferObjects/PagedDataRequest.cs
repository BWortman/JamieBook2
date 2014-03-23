﻿// PagedDataRequest.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Data.SqlServer.DataTransferObjects
{
    public class PagedDataRequest
    {
        public PagedDataRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
    }
}