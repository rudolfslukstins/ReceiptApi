using Microsoft.VisualStudio.TestTools.UnitTesting;
using Receipt.Core.Models;
using Receipt.Core.Validations;

namespace Receipt.Tests
{
    [TestClass]
    public class ReceiptValidatorTests
    {
        private ReceiptValidator _receiptValidator;
        private Receipts _receipts;

        [TestInitialize]
        public void Setup()
        {
            _receipts = new Receipts();
            _receiptValidator = new ReceiptValidator();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
