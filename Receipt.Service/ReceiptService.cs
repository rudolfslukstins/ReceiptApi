using Microsoft.EntityFrameworkCore;
using Receipt.Core.Models;
using Receipt.Core.Services;
using Receipt.Db;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Receipt.Service
{
    public class ReceiptService : EntityService<Receipts>, IReceiptService
    {
        public ReceiptService(IReceiptDbContext context) : base(context)
        {
        }

        public Receipts CreateReceipt(DateTime createdOn, List<Items> products)
        {
            

            var receipt = new Receipts
            {
                CreatedOn = createdOn,
                ItemsList = products,
            };
            Create(receipt);

            return receipt;
        }

        public void DeleteReceipt(int id)
        {
            var receipt = _context.Receipts
                .Include(r => r.ItemsList)
                .FirstOrDefault(r => r.Id == id);

            if (receipt != null)
            {
                foreach (var item in receipt.ItemsList)
                {
                    _context.Items.Remove(item);
                }

                _context.SaveChanges();

                Delete(receipt);
            }
        }

        public List<Receipts> GetReceiptsFromDateToDate(DateTime from, DateTime to)
        {
            return _context.Receipts
                .Include(r => r.ItemsList)
                .Where(r => r.CreatedOn <= to && r.CreatedOn > from).ToList();
        }

        public List<Receipts> GetReceiptsByProductName(string searchItem)
        {
            return _context.Receipts
                .Include(r => r.ItemsList)
                .Where(r => r.ItemsList
                    .Any(i => i.ProductName.Contains(searchItem))).ToList();
        }

        public List<Receipts> GetAllReceipts()
        {
            return _context.Receipts.Include(r => r.ItemsList).ToList();
        }

        public Receipts GetReceiptsByIdWithItems(int id)
        {
            return _context.Receipts
                .Include(r => r.ItemsList)
                .FirstOrDefault(r => r.Id == id);
        }
    }
}
