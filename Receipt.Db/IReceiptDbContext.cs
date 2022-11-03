using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Receipt.Core.Models;

namespace Receipt.Db
{
    public interface IReceiptDbContext
    {
        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T entity) where T : class;
        public DbSet<Receipts> Receipts { get; set; }
        public DbSet<Items> Items { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
