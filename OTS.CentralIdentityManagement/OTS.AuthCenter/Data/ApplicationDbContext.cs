using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OTS.AuthCenter.Models;

namespace OTS.AuthCenter.Data
{
    public class ApplicationDbContext : IdentityDbContext<AuthCenterIdentity>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SiteLocation> siteLocations { get; set; }

        public DbSet<Building> buildings { get; set; }

        public DbSet<Room> rooms { get; set; }

        public DbSet<OTS.AuthCenter.Models.DummyIdentity> DummyIdentity { get; set; }
    }
}
