using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Receipt.Core.Models;
using Receipt.Core.Validations;

namespace Receipt.Tests
{
    [TestClass]
    public class ReceiptValidatorTests
    {
        private ReceiptValidator _receiptValidator;
        private Receipts _receips;

        [TestInitialize]
        public void Setup()
        {
            _receips = new Receipts();
            _receiptValidator = new ReceiptValidator();
        }

        [TestMethod]
        public void ReceiptValidator_SearchReceiptWhenThereIsNoReceipt_ShouldBeFalse()
        {

            var result = _receiptValidator.Validate(_receips).IsValid;
            result.Should().BeFalse();
        }

        [TestMethod]
        public void ReceiptValidator_SearchReceiptWhenThereIsReceipt_ShouldBeTrue()
        {
            var items = new List<Items>();
            var item = new Items
            {
                ProductName = "C#",
                Id = 1
            };
            items.Add(item);

            var receipt = new Receipts
            {
                CreatedOn = DateTime.Now,
                Id = 1,
                ItemsList = items
            };

            var result = _receiptValidator.Validate(receipt).IsValid;
            result.Should().BeTrue();
        }
    }
}
