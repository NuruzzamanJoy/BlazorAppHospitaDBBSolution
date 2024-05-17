using BlazorAppHospitaDBB.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlazorAppHospitaDBB.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
