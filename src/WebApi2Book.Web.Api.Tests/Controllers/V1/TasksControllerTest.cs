// TasksControllerTest.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using Moq;
using NUnit.Framework;
using WebApi2Book.Data;
using WebApi2Book.Web.Api.Controllers.V1;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.Tests.Controllers.V1
{
    [TestFixture]
    public class TasksControllerTest
    {
        [SetUp]
        public void SetUp()
        {
            _pagedDataRequestFactoryMock = new Mock<IPagedDataRequestFactory>();
            _allTasksInquiryProcessorMock = new Mock<IAllTasksInquiryProcessor>();
            _taskByIdInquiryProcessorMock = new Mock<ITaskByIdInquiryProcessor>();
            _addTaskMaintenanceProcessorMock = new Mock<IAddTaskMaintenanceProcessor>();
            _updateTaskMaintenanceProcessorMock = new Mock<IUpdateTaskMaintenanceProcessor>();
            _deleteTaskQueryProcessorMock = new Mock<IDeleteTaskQueryProcessor>();

            _controller = new TasksController(_pagedDataRequestFactoryMock.Object,
                _allTasksInquiryProcessorMock.Object,
                _taskByIdInquiryProcessorMock.Object,
                _addTaskMaintenanceProcessorMock.Object,
                _updateTaskMaintenanceProcessorMock.Object,
                _deleteTaskQueryProcessorMock.Object);
        }

        private Mock<IPagedDataRequestFactory> _pagedDataRequestFactoryMock;
        private Mock<IAllTasksInquiryProcessor> _allTasksInquiryProcessorMock;
        private Mock<ITaskByIdInquiryProcessor> _taskByIdInquiryProcessorMock;
        private Mock<IAddTaskMaintenanceProcessor> _addTaskMaintenanceProcessorMock;
        private Mock<IUpdateTaskMaintenanceProcessor> _updateTaskMaintenanceProcessorMock;
        private Mock<IDeleteTaskQueryProcessor> _deleteTaskQueryProcessorMock;

        private TasksController _controller;

        [Test]
        public void GetTasks_returns_correct_response()
        {
            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage();
            var request = new PagedDataRequest(1, 25);
            var response = new PagedDataInquiryResponse<Task>();

            _pagedDataRequestFactoryMock.Setup(x => x.Create(requestMessage.RequestUri)).Returns(request);
            _allTasksInquiryProcessorMock.Setup(x => x.GetTasks(request)).Returns(response);

            var actualResponse = _controller.GetTasks(requestMessage);

            Assert.AreSame(response, actualResponse);
        }
    }
}