using Microsoft.EntityFrameworkCore;
using Receipt.Core.Models;
using System.Threading.Tasks;

namespace Receipt.Db
{
    public class ReceiptDbContext : DbContext, IReceiptDbContext
    {
        public ReceiptDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Receipts> Receipts { get; set; }
        public DbSet<Items> Items { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
