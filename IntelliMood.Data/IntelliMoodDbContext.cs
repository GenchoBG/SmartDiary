﻿using IntelliMood.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntelliMood.Data
{
    public class IntelliMoodDbContext : IdentityDbContext<User>
    {
        public DbSet<Message> Messages { get; set; }

        public IntelliMoodDbContext(DbContextOptions<IntelliMoodDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
