using System;
using System.Collections.Generic;
using Receipt.Core.Models;

namespace Receipt.Core.Services
{
    public interface IReceiptService : IEntityService<Receipts>
    {
        Receipts CreateReceipt(DateTime createdOn, List<Items> products);
        void DeleteReceipt(int id);
        List<Receipts> GetReceiptsFromDateToDate(DateTime from, DateTime to);
        List<Receipts> GetReceiptsByProductName(string productName);
        Receipts GetReceiptsByIdWithItems(int id);
        List<Receipts> GetAllReceipts();
    }
}