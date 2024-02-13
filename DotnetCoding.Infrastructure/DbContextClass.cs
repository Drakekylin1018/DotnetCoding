using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Infrastructure
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<ProductDetails> Products { get; set; }

        public DbSet<ApprovalQueue> Approvals { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<RequestReason> RequestReasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the entity relationships and constraints here
            modelBuilder.Entity<ProductDetails>()
                .HasOne(p => p.ProductStatusType)
                .WithMany()
                .HasForeignKey(p => p.ProductStatusTypeId);

            modelBuilder.Entity<ApprovalQueue>()
                .HasOne(a => a.RequestReasonType) 
                .WithMany()
                .HasForeignKey(p =>p.RequestReasonTypeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
