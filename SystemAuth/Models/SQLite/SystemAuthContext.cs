using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SystemAuth.Models.SQLite
{
    public partial class SystemAuthContext : DbContext
    {
        public SystemAuthContext()
        {
        }

        public SystemAuthContext(DbContextOptions<SystemAuthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actions> Actions { get; set; }
        public virtual DbSet<App> App { get; set; }
        public virtual DbSet<Ctrl> Ctrl { get; set; }
        public virtual DbSet<IActionRole> IActionRole { get; set; }
        public virtual DbSet<ICtrlRole> ICtrlRole { get; set; }
        public virtual DbSet<IMemberRole> IMemberRole { get; set; }
        public virtual DbSet<IMenuRole> IMenuRole { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<RoleGroup> RoleGroup { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }
        public virtual DbSet<Token> Token { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actions>(entity =>
            {
                entity.HasKey(e => e.ActionId);

                entity.ToTable("actions");

                entity.Property(e => e.ActionId).ValueGeneratedNever();

                entity.Property(e => e.Description).HasColumnType("NVARCHAR (500)");

                entity.Property(e => e.Method)
                    .IsRequired()
                    .HasColumnType("VARCHAR (10)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR (50)");

                entity.HasOne(d => d.Controller)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.ControllerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<App>(entity =>
            {
                entity.ToTable("app");

                entity.Property(e => e.AppId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("NVARCHAR (100)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("NVARCHAR (20)");
            });

            modelBuilder.Entity<Ctrl>(entity =>
            {
                entity.ToTable("ctrl");

                entity.Property(e => e.CtrlId).ValueGeneratedNever();

                entity.Property(e => e.Description).HasColumnType("NVARCHAR (500)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR (50)");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.Ctrl)
                    .HasForeignKey(d => d.AppId);
            });

            modelBuilder.Entity<IActionRole>(entity =>
            {
                entity.HasKey(e => new { e.ActionId, e.RoleId });

                entity.ToTable("i_action_role");

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.IActionRole)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.IActionRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ICtrlRole>(entity =>
            {
                entity.HasKey(e => new { e.ControllerId, e.RoleId });

                entity.ToTable("i_ctrl_role");

                entity.HasOne(d => d.Controller)
                    .WithMany(p => p.ICtrlRole)
                    .HasForeignKey(d => d.ControllerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ICtrlRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<IMemberRole>(entity =>
            {
                entity.HasKey(e => new { e.Account, e.RoleId });

                entity.ToTable("i_member_role");

                entity.Property(e => e.Account).HasColumnType("VARCHAR (30)");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.IMemberRole)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.IMemberRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<IMenuRole>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.RoleId });

                entity.ToTable("i_menu_role");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.IMenuRole)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.IMenuRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Account);

                entity.ToTable("member");

                entity.Property(e => e.Account).HasColumnType("VARCHAR (30)");

                entity.Property(e => e.AddTime).HasColumnType("DATETIME");

                entity.Property(e => e.Domain).HasColumnType("NVARCHAR (50)");

                entity.Property(e => e.Email).HasColumnType("NVARCHAR (50)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("NVARCHAR (20)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("BOOLEAN");

                entity.Property(e => e.LastName).HasColumnType("NVARCHAR (20)");

                entity.Property(e => e.Password).HasColumnType("NVARCHAR (50)");

                entity.Property(e => e.UpdatedTime).HasColumnType("DATETIME");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.MenuId).ValueGeneratedNever();

                entity.Property(e => e.Component).HasColumnType("NVARCHAR (50)");

                entity.Property(e => e.MenuText)
                    .IsRequired()
                    .HasColumnType("NVARCHAR (20)");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnType("NVARCHAR (20)");

                entity.Property(e => e.Selector).HasColumnType("VARCHAR (50)");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.AppId);
            });

            modelBuilder.Entity<RoleGroup>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("role_group");

                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.ApproveScope)
                    .IsRequired()
                    .HasColumnType("BOOLEAN");

                entity.Property(e => e.Description).HasColumnType("NVARCHAR (100)");

                entity.Property(e => e.PassScope)
                    .IsRequired()
                    .HasColumnType("BOOLEAN");

                entity.Property(e => e.PrintScope)
                    .IsRequired()
                    .HasColumnType("BOOLEAN");

                entity.Property(e => e.RejectScope)
                    .IsRequired()
                    .HasColumnType("BOOLEAN");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnType("NVARCHAR (20)");

                entity.Property(e => e.SubmitScope)
                    .IsRequired()
                    .HasColumnType("BOOLEAN");
            });

            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.HasKey(e => e.Num);

                entity.ToTable("system_log");

                entity.Property(e => e.Num).ValueGeneratedNever();

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasColumnType("VARCHAR (30)");

                entity.Property(e => e.Action).HasColumnType("VARCHAR (50)");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasColumnType("VARCHAR (20)");

                entity.Property(e => e.LogTime)
                    .IsRequired()
                    .HasColumnType("DATETIME");

                entity.Property(e => e.Method).HasColumnType("VARCHAR (10)");

                entity.Property(e => e.Route).HasColumnType("VARCHAR (50)");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.SystemLog)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasKey(e => e.Account);

                entity.ToTable("token");

                entity.Property(e => e.Account).HasColumnType("VARCHAR (30)");

                entity.Property(e => e.AuthTime).HasColumnType("DATETIME");

                entity.Property(e => e.ExpiredTime).HasColumnType("DATETIME");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasColumnType("VARCHAR (30)");

                entity.Property(e => e.TokenCode)
                    .IsRequired()
                    .HasColumnType("VARCHAR (300)");

                entity.HasOne(d => d.AccountNavigation)
                    .WithOne(p => p.Token)
                    .HasForeignKey<Token>(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
