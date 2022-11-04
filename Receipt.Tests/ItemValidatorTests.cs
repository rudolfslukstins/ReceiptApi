using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Receipt.Core.Models;
using Receipt.Core.Validations;

namespace Receipt.Tests
{

    [TestClass]
    public class ItemValidatorTests
    {
        private ItemValidator _validator;
        private Items _items;

        [TestInitialize]
        public void Setup()
        {
            _validator = new ItemValidator();
            _items = new Items();
        }

        [TestMethod]
        public void ItemsValidator_SearchItemWhenThereIsNoItem_ShouldBeFalse()
        {

            var result = _validator.Validate(_items).IsValid;
            result.Should().BeFalse();
        }

        [TestMethod]
        public void ItemsValidator_SearchItemWhenThereIsItem_ShouldBeTrue()
        {
            _items.Id = 1;
            _items.ProductName = "C#";
            var result = _validator.Validate(_items).IsValid;
            result.Should().BeTrue();
        }

        [TestMethod]
        public void ItemsValidator_SearchItemWhenProductNameIsEmpty_ShouldBeFalse()
        {
            _items.Id = 1;
            _items.ProductName = "";
            var result = _validator.Validate(_items).IsValid;
            result.Should().BeFalse();
        }
    }
}