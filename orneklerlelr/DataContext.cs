using Microsoft.EntityFrameworkCore;
using orneklerlelr.Models;

namespace orneklerlelr
{
    public class DataContext:DbContext
    {
       public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=sadik;Trusted_Connection=True;");
        //}
        public DbSet<Product> Products { get; set; }
    }
}
