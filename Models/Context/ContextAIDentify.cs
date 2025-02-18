using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Data.Common;

namespace AIDentify.Models.Context
{
    public class ContextAIDentify : IdentityDbContext<ApplicationUser>
    {
        public ContextAIDentify() : base() { }
        public ContextAIDentify(DbContextOptions options) : base(options)
        {

        }

        //public DbSet<Admin> Admin { get; set; }
        //public DbSet<AgeM> AgeM { get; set; }
        //public DbSet<DiseaseM> DiseaseM { get; set; }
        //public DbSet<GenderM> GenderM { get; set; }
        //public DbSet<Result> Result { get; set; }
        //public DbSet<Review> Review { get; set; }
        //public DbSet<Model> Models { get; set; }
        //public DbSet<PayDate> PayDate { get; set; }
        //public DbSet<Payment> Payment { get; set; }
        //public DbSet<Plan> Plan { get; set; }

        //public DbSet<Report> Report { get; set; }

        //public DbSet<Subscriber> Subscriber { get; set; }
        //public DbSet<Subscription> Subscription { get; set; }

        //public DbSet<SystemUpdate> SystemUpdate { get; set; }

        //public DbSet<TeethNumberingM> TeethNumberingM { get; set; }

        //public DbSet<User> User { get; set; }

    }
}

