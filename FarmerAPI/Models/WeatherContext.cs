using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FarmerAPI.Models
{
    public partial class WeatherContext : DbContext
    {
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<ImenuRole> ImenuRole { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<RealTime> RealTime { get; set; }
        public virtual DbSet<RoleGroup> RoleGroup { get; set; }
        public virtual DbSet<StationInfo> StationInfo { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }
        public virtual DbSet<WeatherData> WeatherData { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS; Database=Weather; Trusted_Connection=True; User ID=sa;Password=2ooixuui;");
        //            }
        //        }
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(10);
            });

            modelBuilder.Entity<ImenuRole>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.RoleId });

                entity.ToTable("IMenuRole");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.MemId);

                entity.Property(e => e.MemId)
                    .HasColumnName("MemID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DeptId)
                    .HasColumnName("DeptID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Domain).HasMaxLength(50);

                entity.Property(e => e.MemPw)
                    .HasColumnName("MemPW")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.MenuId)
                    .HasColumnName("MenuID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Component).HasMaxLength(50);

                entity.Property(e => e.MenuText)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.RootMenuId).HasColumnName("RootMenuID");
            });

            modelBuilder.Entity<RealTime>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Rh)
                    .HasColumnName("RH")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Temperature).HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<RoleGroup>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.ParentRoleId).HasColumnName("ParentRoleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<StationInfo>(entity =>
            {
                entity.HasKey(e => e.Num);

                entity.Property(e => e.Num).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(10);

                entity.HasOne(d => d.NumNavigation)
                    .WithOne(p => p.InverseNumNavigation)
                    .HasForeignKey<StationInfo>(d => d.Num)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StationInfo_StationInfo");
            });

            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.HasKey(e => e.Num);

                entity.Property(e => e.Num).ValueGeneratedNever();

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Detail).HasColumnType("ntext");

                entity.Property(e => e.Domain).HasMaxLength(50);

                entity.Property(e => e.LogTime).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WeatherData>(entity =>
            {
                entity.HasKey(e => new { e.StationNum, e.ObsTime });

                entity.Property(e => e.ObsTime).HasColumnType("datetime");

                entity.Property(e => e.GlobalRad).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Precp).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.PrecpHour).HasColumnType("decimal(3, 1)");

                entity.Property(e => e.Rh)
                    .HasColumnName("RH")
                    .HasColumnType("decimal(3, 0)");

                entity.Property(e => e.SeaPres).HasColumnType("decimal(5, 1)");

                entity.Property(e => e.StnPres).HasColumnType("decimal(5, 1)");

                entity.Property(e => e.SunShine).HasColumnType("decimal(2, 1)");

                entity.Property(e => e.Td).HasColumnType("decimal(3, 1)");

                entity.Property(e => e.Temperature).HasColumnType("decimal(3, 1)");

                entity.Property(e => e.Visb).HasColumnType("decimal(3, 1)");

                entity.Property(e => e.Wd)
                    .HasColumnName("WD")
                    .HasColumnType("decimal(3, 0)");

                entity.Property(e => e.Wdgust)
                    .HasColumnName("WDGust")
                    .HasColumnType("decimal(3, 0)");

                entity.Property(e => e.Ws)
                    .HasColumnName("WS")
                    .HasColumnType("decimal(3, 1)");

                entity.Property(e => e.Wsgust)
                    .HasColumnName("WSGust")
                    .HasColumnType("decimal(3, 1)");

                entity.HasOne(d => d.StationNumNavigation)
                    .WithMany(p => p.WeatherData)
                    .HasForeignKey(d => d.StationNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeatherData_StationInfo");
            });
        }
    }
}
