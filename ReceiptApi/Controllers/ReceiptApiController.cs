using Microsoft.AspNetCore.Mvc;
using Receipt.Core.Models;
using Receipt.Core.Services;
using Receipt.Core.Validations;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ReceiptApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
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

        [Microsoft.AspNetCore.Mvc.Route("Create-Receipt")]
        [Microsoft.AspNetCore.Mvc.HttpPut]
        public IActionResult CreateReceipt(DateTime createTime, List<Items> products)
        {
            var receipt = _receiptService.CreateReceipt(createTime, products);

            return Created("", receipt);
        }

        [Microsoft.AspNetCore.Mvc.Route("Get-Receipt-By-Id/{id}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult GetReceiptById(int id)
        {
            var receipt = _receiptService.GetReceiptsByIdWithItems(id);

            if (receipt == null) return NotFound(id);

            return Ok(receipt);
        }


        [Microsoft.AspNetCore.Mvc.Route("Get-All-Receipts")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult GetAllReceipts()
        {
            var receipts = _receiptService.GetAllReceipts();

            return Ok(receipts);
        }

        [Microsoft.AspNetCore.Mvc.Route("Get-Receipts-By-Date-Range")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult GetReceiptsByDateRange(DateTime from, DateTime to)
        {
            if (from > to) return BadRequest();

            var receipts = _receiptService.GetReceiptsFromDateToDate(from, to);

            return Ok(receipts);
        }

        [Microsoft.AspNetCore.Mvc.Route("Get-Receipts-By-Product-Name/{productName}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult GetReceiptsByProductName(string productName)
        {
            var receipts = _receiptService.GetReceiptsByProductName(productName);

            return Ok(receipts);
        }

        [Microsoft.AspNetCore.Mvc.Route("Delete-Receipt/{id}")]
        [Microsoft.AspNetCore.Mvc.HttpDelete]
        public IActionResult DeleteReceipt(int id)
        {
            var receipt = _receiptService.GetReceiptsByIdWithItems(id);

            if (receipt == null) return NotFound(id);

            _receiptService.DeleteReceipt(id);
            return Ok(id);

        }
    }
}
