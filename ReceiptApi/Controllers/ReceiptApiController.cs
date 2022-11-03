using Microsoft.AspNetCore.Mvc;
using Receipt.Core.Services;
using Receipt.Core.Validations;
using System;

namespace ReceiptApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptApiController : ControllerBase
    {
        private readonly IReceiptService _receiptService;
        private readonly ReceiptValidator _receiptValidator;

        public ReceiptApiController(IReceiptService receiptService,
            ReceiptValidator receiptValidator)
        {
            _receiptService = receiptService;
            _receiptValidator = receiptValidator;
        }

        [Route("Create-Receipt")]
        [HttpPut]
        public IActionResult CreateReceipt(DateTime createTime, string items)
        {
            var receipt = _receiptService.CreateReceipt(createTime, items);
            return Created("", receipt);
        }

        [Route("Get-receipt-by-id/{id}")]
        [HttpGet]
        public IActionResult GetReceiptById(int id)
        {
            var receipt = _receiptService.GetReceiptsByIdWithItems(id);

            if (_receiptValidator.Validate(receipt).IsValid)
            {
                return Ok(receipt);
            }

            return NotFound(id);
        }


        [Route("Get-all-receipts")]
        [HttpGet]
        public IActionResult GetAllReceipts()
        {
            var receipts = _receiptService.GetAllReceipts();

            return Ok(receipts);
        }

        [Route("Get-Receipts-by-date-range")]
        [HttpGet]
        public IActionResult GetReceiptsByDateRange(DateTime from, DateTime to)
        {
            var receipts = _receiptService.GetReceiptsFromDateToDate(from, to);

            return Ok(receipts);
        }

        [Route("Get-Receipts-by-product-name/{productName}")]
        [HttpGet]
        public IActionResult GetReceiptsByDateRange(string productName)
        {
            var receipts = _receiptService.GetReceiptsByProductName(productName).ToArray();

            return Ok(receipts);
        }

        [Route("Delete-Receipt/{id}")]
        [HttpDelete]
        public IActionResult DeleteReceipt(int id)
        {
            var receipt = _receiptService.GetReceiptsByIdWithItems(id);
            
            if (_receiptValidator.Validate(receipt).IsValid)
            {
                _receiptService.DeleteReceipt(id);
                return Ok(id);
            }

            return NotFound(id);
        }
    }
}
