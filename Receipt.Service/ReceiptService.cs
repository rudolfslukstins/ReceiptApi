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
        public ReceiptService(ReceiptDbContext context) : base(context)
        {
        }

        public Receipts CreateReceipt(DateTime createdOn, string items)
        {
            var itemsList = MakeListOfItems(items);
            var receipt = new Receipts
            {
                CreatedOn = createdOn,
                ItemsList = itemsList
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
                    .Any(i => i.ProductName == searchItem)).ToList();
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

        public List<Items> MakeListOfItems(string items)
        {
            var itemsList = new List<Items>();
            string[] list = items.Split(' ');

            foreach (var item in list)
            {
                var newItem = new Items
                {
                    ProductName = item
                };

                if (_context.Items.Any(i => i.ProductName.Contains(item)))
                {
                    var existingItem = _context.Items.FirstOrDefault(i => i.ProductName == newItem.ProductName);
                    itemsList.Add(existingItem);
                }
                else
                {
                    itemsList.Add(newItem);
                }
            }

            return itemsList;
        }
    }
}
