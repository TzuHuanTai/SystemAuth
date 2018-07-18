using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FarmerAPI.Models
{
    public partial class SystemStructure2Context : DbContext
    {
        public virtual DbSet<Test2Db> Test2Db { get; set; }        
        public virtual DbSet<VwTest> VwTest { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS; Database=SystemStructure;Trusted_Connection=True; User ID=sa;Password=2ooixuui;");
        //            }
        //        }

        public SystemStructure2Context(DbContextOptions<SystemStructure2Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VwTest>(entity => 
            {
                entity.HasKey(e => e.TestPk);
            });

            modelBuilder.Entity<Test2Db>(entity =>
            {
                entity.HasKey(e => e.TestPk);

                entity.ToTable("Test2DB");

                entity.Property(e => e.TestPk)
                    .HasColumnName("TestPK")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActionName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Test1).HasColumnType("nchar(10)");

                entity.Property(e => e.Test2).HasColumnType("nchar(10)");
            });

            
        }
    }
}
