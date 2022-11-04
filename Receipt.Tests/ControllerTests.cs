using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Receipt.Core.Models;
using Receipt.Core.Services;
using Receipt.Core.Validations;
using ReceiptApi.Controllers;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace Receipt.Tests
{
    [TestClass]
    public class ControllerTests
    {
        private ReceiptApiController _controller;
        private Mock<IReceiptService> _receiptServiceMock;
        private Mock<ReceiptValidator> _receiptValidatorMock;
        private Receipts _receipt1;
        private Receipts _receipt2;
        private List<Receipts> _receiptsList;
        private List<Items> _list;
        private DateTime _created;

        [TestInitialize]
        public void Setup()
        {
            _list = new List<Items>()
            {
                new Items()
                {
                    Id = 1,
                    ProductName = "C# SQL"
                }
            };

            _receipt1 = new Receipts()
            {
                Id = 1,
                CreatedOn = new DateTime(2022, 11, 2),
                ItemsList = _list
            };

            _receipt2 = new Receipts()
            {
                Id = 2,
                CreatedOn = new DateTime(2000, 11, 2),
                ItemsList = _list
            };
            _receiptsList = new List<Receipts>() { _receipt1, _receipt2 };
            _receiptServiceMock = new Mock<IReceiptService>();
            _created = new DateTime(2022, 11, 2);
            _receiptValidatorMock = new Mock<ReceiptValidator>();
            _controller = new ReceiptApiController(_receiptServiceMock.Object, _receiptValidatorMock.Object);
        }

        [TestMethod]
        public void CreateReceipt_CreateReceipt_ShouldReturnReceipt()
        {
            _receiptServiceMock.Setup(r => r.CreateReceipt(_created, _list)).Returns(_receipt1);

            var actionResult = _controller.CreateReceipt(_created, _list) as ObjectResult;

            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Value as Receipts, _receipt1);
        }

        [TestMethod]
        public void GetReceiptById_GetReceiptByIdWhenThereIsNoReceipt_ShouldReturnNotFound()
        {
            var actionResult = _controller.GetReceiptById(10) as ObjectResult;

            Assert.IsNotNull(actionResult);
            Assert.AreEqual(404, actionResult.StatusCode);
        }


        [TestMethod]
        public void GetReceiptById_GetReceiptByIdWhenThereIsReceipt_ShouldGetTheReceipt()
        {
            _receiptServiceMock.Setup(service => service.GetReceiptsByIdWithItems(_receipt1.Id)).Returns(_receipt1);
            var actionResult = _controller.GetReceiptById(_receipt1.Id);

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetReceiptsListFromDateToDate_GetReceiptsListFromDateToDate_ShouldReturnReceiptsWithinDateRange()
        {
            var from = new DateTime(2022, 11, 1);
            var to = new DateTime(2022,11, 3);
            
            _receiptServiceMock.Setup(r => r.GetReceiptsFromDateToDate(from,to)).Returns(_receiptsList);
            var actionResult = _controller.GetReceiptsByDateRange(from,to) as ObjectResult;

            Assert.AreEqual(actionResult.Value as List<Receipts>, _receiptsList);
        }

        [TestMethod]
        public void GetReceiptsListFromDateToDate_GetReceiptsListFromDateToDate_ShouldReturnReceiptsWithinDate()
        {
            var from = new DateTime(2022, 11, 1);
            var to = new DateTime(2020, 11, 3);

            var actionResult = _controller.GetReceiptsByDateRange(from, to) as BadRequestObjectResult;

            Assert.AreEqual(typeof(BadRequestObjectResult) , typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void
            GetReceiptsByProductName_GetReceiptsListByProductName_ShouldReturnListOfProductsWhichContainsProduct()
        {
            _receiptServiceMock.Setup(r => r.GetReceiptsByProductName("C#")).Returns(_receiptsList);
            var actionResult = _controller.GetReceiptsByProductName("C#") as ObjectResult;

            Assert.AreEqual(actionResult.Value as List<Receipts>, _receiptsList);
        }

        [TestMethod]
        public void DeleteReceipts_DeleteReceiptWhenThereIsReceipt()
        {
            _receiptServiceMock.Setup(r => r.GetReceiptsByIdWithItems(_receipt1.Id)).Returns(_receipt1);
            var actionResult = _controller.DeleteReceipt(_receipt1.Id) as ObjectResult;

            Assert.AreEqual(actionResult.Value, _receipt1.Id);
        }

        [TestMethod]
        public void DeleteReceipts_DeleteReceiptWhenThereIsNoReceipt()
        {
            var actionResult = _controller.DeleteReceipt(_receipt1.Id) as ObjectResult;

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }
    }
}