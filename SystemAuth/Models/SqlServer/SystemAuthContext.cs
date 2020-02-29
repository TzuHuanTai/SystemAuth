using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SystemAuth.Models.SqlServer
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
        public virtual DbSet<IactionRole> IactionRole { get; set; }
        public virtual DbSet<IctrlRole> IctrlRole { get; set; }
        public virtual DbSet<ImemRole> ImemRole { get; set; }
        public virtual DbSet<ImenuRole> ImenuRole { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<RoleGroup> RoleGroup { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }
        public virtual DbSet<Token> Token { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=SystemAuth;Trusted_Connection=True; User ID=sa;Password=2ooixuui;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actions>(entity =>
            {
                entity.HasKey(e => e.ActionId);

                entity.Property(e => e.ActionId).HasColumnName("ActionID");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Method)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Controller)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.ControllerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Action_Ctrl");
            });

            modelBuilder.Entity<App>(entity =>
            {
                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Ctrl>(entity =>
            {
                entity.Property(e => e.CtrlId).HasColumnName("CtrlID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.App)
                    .WithMany(p => p.Ctrl)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ctrl_Application");
            });

            modelBuilder.Entity<IactionRole>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.ActionId });

                entity.ToTable("IActionRole");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.ActionId).HasColumnName("ActionID");

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.IactionRole)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IActionRole_Action");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.IactionRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IActionRole_RoleGroup");
            });

            modelBuilder.Entity<IctrlRole>(entity =>
            {
                entity.HasKey(e => new { e.ControllerId, e.RoleId });

                entity.ToTable("ICtrlRole");

                entity.Property(e => e.ControllerId).HasColumnName("ControllerID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Controller)
                    .WithMany(p => p.IctrlRole)
                    .HasForeignKey(d => d.ControllerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ICtrlRole_Ctrl");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.IctrlRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ICtrlRole_RoleGroup");
            });

            modelBuilder.Entity<ImemRole>(entity =>
            {
                entity.HasKey(e => new { e.Account, e.RoleId });

                entity.ToTable("IMemRole");

                entity.Property(e => e.Account)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.ImemRole)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IMemRole_Member");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ImemRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IMemRole_RoleGroup");
            });

            modelBuilder.Entity<ImenuRole>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.RoleId });

                entity.ToTable("IMenuRole");

                entity.HasIndex(e => e.MenuId)
                    .HasName("IX_IMenuRole");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.ImenuRole)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IMenuRole_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ImenuRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IMenuRole_RoleGroup");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Account);

                entity.Property(e => e.Account)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.DeptId)
                    .HasColumnName("DeptID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Domain).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.Component).HasMaxLength(50);

                entity.Property(e => e.MenuText)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.RootMenuId).HasColumnName("RootMenuID");

                entity.Property(e => e.Selector)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.App)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_App");
            });

            modelBuilder.Entity<RoleGroup>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.ParentRoleId).HasColumnName("ParentRoleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.HasKey(e => e.Num);

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Detail).HasColumnType("ntext");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LogTime).HasColumnType("datetime");

                entity.Property(e => e.Method)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Route)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.SystemLog)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SystemLog_Member");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasKey(e => e.Account);

                entity.Property(e => e.Account)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AuthTime).HasColumnType("datetime");

                entity.Property(e => e.ExpiredTime).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TokenCode)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccountNavigation)
                    .WithOne(p => p.Token)
                    .HasForeignKey<Token>(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Token_Member");
            });
        }
    }
}
