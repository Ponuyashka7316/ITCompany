using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;



namespace ITCompany.Models
{


    public partial class KvadroContext : DbContext
    {
        public virtual DbSet<CustomerInfo> CustomerInfo { get; set; }
        public virtual DbSet<EmployeeInfo> EmployeeInfo { get; set; }
        public virtual DbSet<ParticipationInProject> ParticipationInProject { get; set; }
        public virtual DbSet<ProgrammingLanguages> ProgrammingLanguages { get; set; }
        public virtual DbSet<ProjectInfo> ProjectInfo { get; set; }
        public virtual DbSet<SalaryInfo> SalaryInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public KvadroContext(DbContextOptions<KvadroContext> options)
    : base(options)
        { }

        public IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerInfo>(entity =>
            {
                entity.HasKey(e => e.Inn);

                entity.Property(e => e.Inn)
                    .HasColumnName("INN")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AdressCost)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bank)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Customer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fioworker)
                    .HasColumnName("FIOWorker")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasColumnType("text");

                entity.Property(e => e.PhoneWorker)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeInfo>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Base)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Year).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.EmployeeInfo)
                    .HasForeignKey<EmployeeInfo>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeInfo_SalaryInfo");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.EmployeeInfo)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_EmployeeInfo_ProjectInfo");
            });

            modelBuilder.Entity<ParticipationInProject>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.EmployeeId });

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StopDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ParticipationInProject)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participation in project_EmployeeInfo1");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ParticipationInProject)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participation in project_ProjectInfo");
            });

            modelBuilder.Entity<ProgrammingLanguages>(entity =>
            {
                entity.ToTable("Programming_Languages");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("nchar(20)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ProgrammingLanguages)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Programming Languages_EmployeeInfo");
            });

            modelBuilder.Entity<ProjectInfo>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.Property(e => e.ProjectId).ValueGeneratedNever();

                entity.Property(e => e.Chief)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Inn)
                    .HasColumnName("INN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectStart).HasColumnType("date");

                entity.Property(e => e.ProjectStop).HasColumnType("date");

                entity.HasOne(d => d.InnNavigation)
                    .WithMany(p => p.ProjectInfo)
                    .HasForeignKey(d => d.Inn)
                    .HasConstraintName("FK_ProjectInfo_CustomerInfo");
            });

            modelBuilder.Entity<SalaryInfo>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.Bonus).HasColumnType("money");

                entity.Property(e => e.Exempt).HasColumnType("money");

                entity.Property(e => e.GrossSalary).HasColumnType("money");

                entity.Property(e => e.Salary).HasColumnType("money");
            });
        }
    }
}
