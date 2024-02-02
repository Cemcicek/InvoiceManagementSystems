using InvoiceManagementSystems.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagementSystems.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<MessageAdmin> MessageAdmins { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
