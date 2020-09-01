using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InlaksAlumniWebsite.Models
{
    public class InlaksAlumniDbContext : DbContext
    {
        public InlaksAlumniDbContext() : base("MyConn") { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Alumni> Alumnis { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<EventImage> EventImages { get; set; }

    }
}