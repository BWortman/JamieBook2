﻿// IAddTaskMaintenanceProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public interface IAddTaskMaintenanceProcessor
    {
        Task AddTask(NewTask newTask);
    }
}