using Donify.API.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace Donify.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Donor> Donors { get; set; }
        public DbSet<Donation> Donations { get; set; }

    }
}
