// AllTasksInquiryProcessorTest.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;
using PagedTaskDataInquiryResponse =
    WebApi2Book.Web.Api.Models.PagedDataInquiryResponse<WebApi2Book.Web.Api.Models.Task>;

namespace WebApi2Book.Web.Api.Tests.InquiryProcessing
{
    [TestFixture]
    public class AllTasksInquiryProcessorTest
    {
        [SetUp]
        public void SetUp()
        {
            _autoMapperMock = new Mock<IAutoMapper>();
            _commonLinkServiceMock = new Mock<ICommonLinkService>();
            _allTasksQueryProcessorMock = new Mock<IAllTasksQueryProcessor>();
            _taskLinkServiceMock = new Mock<ITaskLinkService>();

            _inquiryProcessor = new AllTasksInquiryProcessorTestDouble(_allTasksQueryProcessorMock.Object,
                _autoMapperMock.Object, _taskLinkServiceMock.Object, _commonLinkServiceMock.Object);
        }

        private const string QueryStringFormat = "pagenumber={0}&pagesize={1}";

        private Mock<IAutoMapper> _autoMapperMock;
        private Mock<ICommonLinkService> _commonLinkServiceMock;
        private Mock<IAllTasksQueryProcessor> _allTasksQueryProcessorMock;
        private Mock<ITaskLinkService> _taskLinkServiceMock;

        private AllTasksInquiryProcessorTestDouble _inquiryProcessor;

        private const int PageNumber = 1;
        private const int PageSize = 20;

        private class AllTasksInquiryProcessorTestDouble : AllTasksInquiryProcessor
        {
            public AllTasksInquiryProcessorTestDouble(IAllTasksQueryProcessor queryProcessor, IAutoMapper autoMapper,
                ITaskLinkService taskLinkService, ICommonLinkService commonLinkService)
                : base(queryProcessor, autoMapper, taskLinkService, commonLinkService)
            {
            }

            public Func<IEnumerable<Data.Entities.Task>, IEnumerable<Task>> GetTasksTestDouble { get; set; }

            public Action<PagedDataInquiryResponse<Task>> AddLinksToInquiryResponseTestDouble { get; set; }

            public Func<PagedDataInquiryResponse<Task>, string> GetCurrentPageQueryStringTestDouble { get; set; }

            public Func<PagedDataInquiryResponse<Task>, string> GetNextPageQueryStringTestDouble { get; set; }

            public Func<PagedDataInquiryResponse<Task>, string> GetPreviousPageQueryStringTestDouble { get; set; }

            public override IEnumerable<Task> GetTasks(IEnumerable<Data.Entities.Task> taskEntities)
            {
                return GetTasksTestDouble == null ? base.GetTasks(taskEntities) : GetTasksTestDouble(taskEntities);
            }

            public override void AddLinksToInquiryResponse(PagedDataInquiryResponse<Task> inquiryResponse)
            {
                if (AddLinksToInquiryResponseTestDouble == null)
                {
                    base.AddLinksToInquiryResponse(inquiryResponse);
                }
                else
                {
                    AddLinksToInquiryResponseTestDouble(inquiryResponse);
                }
            }

            public override string GetCurrentPageQueryString(PagedDataInquiryResponse<Task> inquiryResponse)
            {
                return GetCurrentPageQueryStringTestDouble == null
                    ? base.GetCurrentPageQueryString(inquiryResponse)
                    : GetCurrentPageQueryStringTestDouble(inquiryResponse);
            }

            public override string GetNextPageQueryString(PagedDataInquiryResponse<Task> inquiryResponse)
            {
                return GetNextPageQueryStringTestDouble == null
                    ? base.GetNextPageQueryString(inquiryResponse)
                    : GetNextPageQueryStringTestDouble(inquiryResponse);
            }

            public override string GetPreviousPageQueryString(PagedDataInquiryResponse<Task> inquiryResponse)
            {
                return GetPreviousPageQueryStringTestDouble == null
                    ? base.GetPreviousPageQueryString(inquiryResponse)
                    : GetPreviousPageQueryStringTestDouble(inquiryResponse);
            }
        }

        [Test]
        public void AddLinksToInquiryResponse_adds_AllTasks_link()
        {
            var link = new Link();
            var inquiryResponse = new PagedTaskDataInquiryResponse();

            _taskLinkServiceMock.Setup(x => x.GetAllTasksLink()).Returns(link);

            _inquiryProcessor.AddLinksToInquiryResponse(inquiryResponse);

            Assert.AreSame(link, inquiryResponse.Links.Single());
        }

        [Test]
        public void AddLinksToInquiryResponse_adds_page_links()
        {
            var inquiryResponse = new PagedTaskDataInquiryResponse();
            const string currentPageQueryString = "current";
            const string previousPageQueryString = "previous";
            const string nextPageQueryString = "next";

            _inquiryProcessor.GetCurrentPageQueryStringTestDouble = response => currentPageQueryString;
            _inquiryProcessor.GetPreviousPageQueryStringTestDouble = response => previousPageQueryString;
            _inquiryProcessor.GetNextPageQueryStringTestDouble = response => nextPageQueryString;

            _inquiryProcessor.AddLinksToInquiryResponse(inquiryResponse);

            _commonLinkServiceMock.Verify(x => x.AddPageLinks(inquiryResponse, currentPageQueryString, previousPageQueryString, nextPageQueryString));
        }

        [Test]
        public void GetTasks_adds_links()
        {
            var requestInfo = new PagedDataRequest(PageNumber, PageSize);
            var taskEntity = new Data.Entities.Task {TaskId = 300};
            var queriedItems = new[] {taskEntity};
            var queryResult = new QueryResult<Data.Entities.Task>(queriedItems, queriedItems.Count(), PageSize);

            var task = new Task {TaskId = taskEntity.TaskId};
            var tasks = new[] {task};

            _allTasksQueryProcessorMock.Setup(x => x.GetTasks(requestInfo)).Returns(queryResult);
            _inquiryProcessor.GetTasksTestDouble = items => items == queriedItems ? tasks : null;

            var linksWereAdded = false;
            _inquiryProcessor.AddLinksToInquiryResponseTestDouble =
                response => linksWereAdded = tasks.SequenceEqual(response.Items)
                                             &&
                                             response.PageCount ==
                                             queryResult.TotalPageCount
                                             && response.PageNumber == PageNumber
                                             && response.PageSize == PageSize;

            _inquiryProcessor.GetTasks(requestInfo);

            Assert.IsTrue(linksWereAdded);
        }

        [Test]
        public void GetTasks_returns_correct_result()
        {
            var requestInfo = new PagedDataRequest(PageNumber, PageSize);
            var taskEntity = new Data.Entities.Task {TaskId = 300};
            var queriedItems = new[] {taskEntity};
            var queryResult = new QueryResult<Data.Entities.Task>(queriedItems, queriedItems.Count(), PageSize);

            var task = new Task {TaskId = taskEntity.TaskId};
            var tasks = new[] {task};

            _allTasksQueryProcessorMock.Setup(x => x.GetTasks(requestInfo)).Returns(queryResult);
            _inquiryProcessor.GetTasksTestDouble = items => items == queriedItems ? tasks : null;

            var actualResult = _inquiryProcessor.GetTasks(requestInfo);

            Assert.IsTrue(tasks.SequenceEqual(actualResult.Items), "Incorrect Items in result");
            Assert.AreEqual(queryResult.TotalPageCount, actualResult.PageCount, "Incorrect PageCount in result");
            Assert.AreEqual(PageNumber, actualResult.PageNumber, "Incorrect PageNumber in result");
            Assert.AreEqual(PageSize, actualResult.PageSize, "Incorrect PageSize in result");
        }

        [Test]
        public void GetTasks_maps_entities_to_web_models()
        {
            var taskEntity1 = new Data.Entities.Task { TaskId = 300 };
            var taskEntity2 = new Data.Entities.Task { TaskId = 600 };
            var task1 = new Task {TaskId = taskEntity1.TaskId};
            var task2 = new Task {TaskId = taskEntity2.TaskId};

            var taskEntities = new List<Data.Entities.Task> {taskEntity1, taskEntity2};
            var tasks = new List<Task> {task1, task2};

            for (var i = 0; i < taskEntities.Count; ++i)
            {
                var index = i;
                _autoMapperMock.Setup(x => x.Map<Task>(taskEntities[index])).Returns(tasks[index]);
            }

            var actualResult = _inquiryProcessor.GetTasks(taskEntities);

            Assert.IsTrue(tasks.SequenceEqual(actualResult));
        }

        [Test]
        public void GetTasks_adds_self_link_to_tasks()
        {
            var taskEntity1 = new Data.Entities.Task { TaskId = 300 };
            var taskEntity2 = new Data.Entities.Task { TaskId = 600 };
            var task1 = new Task {TaskId = taskEntity1.TaskId};
            var task2 = new Task {TaskId = taskEntity2.TaskId};

            var taskEntities = new List<Data.Entities.Task> {taskEntity1, taskEntity2};
            var tasks = new List<Task> {task1, task2};

            for (var i = 0; i < taskEntities.Count; ++i)
            {
                var index = i;
                _autoMapperMock.Setup(x => x.Map<Task>(taskEntities[index])).Returns(tasks[index]);

                _taskLinkServiceMock.Setup(x => x.AddSelfLink(tasks[index])).Verifiable();
            }

            _inquiryProcessor.GetTasks(taskEntities);

            _taskLinkServiceMock.VerifyAll();
        }
    }
}