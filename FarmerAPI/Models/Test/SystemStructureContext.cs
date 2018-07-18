using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FarmerAPI.Models
{
    public partial class SystemStructureContext : DbContext
    {        
        public virtual DbSet<TestTable> TestTable { get; set; }
        public virtual DbSet<VwTest> VwTest { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS; Database=SystemStructure;Trusted_Connection=True; User ID=sa;Password=2ooixuui;");
        //            }
        //        }

        public SystemStructureContext(DbContextOptions<SystemStructureContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VwTest>(entity => 
            {
                entity.HasKey(e => e.TestPk);
            });

            modelBuilder.Entity<TestTable>(entity =>
            {
                entity.HasKey(e => e.TestPk);

                entity.Property(e => e.TestPk)
                    .HasColumnName("TestPK")
                    .ValueGeneratedNever();

                entity.Property(e => e.Test1).HasColumnType("nchar(10)");

                entity.Property(e => e.Test2)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
