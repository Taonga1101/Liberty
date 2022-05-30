using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Liberty.Models
{
    public partial class LIBERTYContext : DbContext
    {
        public LIBERTYContext()
        {
        }

        public LIBERTYContext(DbContextOptions<LIBERTYContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<EmploymentDetail> EmploymentDetails { get; set; }
        public virtual DbSet<LeaveApplication> LeaveApplications { get; set; }
        public virtual DbSet<LeaveProperty> LeaveProperties { get; set; }
        public virtual DbSet<LeaveScheme> LeaveSchemes { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Privilege> Privileges { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePrivilege> RolePrivileges { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCredential> UserCredentials { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Name=LibertyDb", options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("DEPARTMENTS");

                entity.HasIndex(e => e.Code, "IX_DEPARTMENTS")
                    .IsUnique();

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.OfficeId).HasColumnName("OFFICE_ID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEPARTMENTS_OFFICES");
            });

            modelBuilder.Entity<EmploymentDetail>(entity =>
            {
                entity.HasKey(e => e.EmploymentDetailsId);

                entity.ToTable("EMPLOYMENT_DETAILS");

                entity.Property(e => e.EmploymentDetailsId).HasColumnName("EMPLOYMENT_DETAILS_ID");

                entity.Property(e => e.ContractEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("CONTRACT_END");

                entity.Property(e => e.ContractStart)
                    .HasColumnType("datetime")
                    .HasColumnName("CONTRACT_START");

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.EmploymentNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EMPLOYMENT_NUMBER");

                entity.Property(e => e.IsHod).HasColumnName("IS-HOD");

                entity.Property(e => e.PositionId).HasColumnName("POSITION_ID");

                entity.Property(e => e.UserId).HasColumnName("USER-ID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.EmploymentDetails)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYMENT_DETAILS_DEPARTMENTS1");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.EmploymentDetails)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYMENT_DETAILS_POSITION");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EmploymentDetails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYMENT_DETAILS_DEPARTMENTS");
            });

            modelBuilder.Entity<LeaveApplication>(entity =>
            {
                entity.ToTable("LEAVE_APPLICATIONS");

                entity.HasIndex(e => e.Reference, "IX_LEAVE_APPLICATIONS")
                    .IsUnique();

                entity.Property(e => e.LeaveApplicationId).HasColumnName("LEAVE_APPLICATION_ID");

                entity.Property(e => e.ApprovedBy).HasColumnName("APPROVED_BY");

                entity.Property(e => e.Attachment)
                    .HasColumnType("text")
                    .HasColumnName("ATTACHMENT");

                entity.Property(e => e.EmergencyContact)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMERGENCY_CONTACT");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("END_DATE");

                entity.Property(e => e.IsApproved).HasColumnName("IS_APPROVED");

                entity.Property(e => e.LeaveTypeId).HasColumnName("LEAVE_TYPE_ID");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("REASON");

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("REFERENCE");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("START_DATE");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.LeaveApplicationApprovedByNavigations)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_LEAVE_APPLICATIONS_USERS1");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LEAVE_APPLICATIONS_LEAVE_TYPE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LeaveApplicationUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LEAVE_APPLICATIONS_USERS");
            });

            modelBuilder.Entity<LeaveProperty>(entity =>
            {
                entity.ToTable("LEAVE_PROPERTIES");

                entity.Property(e => e.LeavePropertyId).HasColumnName("LEAVE_PROPERTY-ID");

                entity.Property(e => e.IsComputedBit)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("IS_COMPUTED_BIT")
                    .IsFixedLength(true);

                entity.Property(e => e.LeaveTypeId).HasColumnName("LEAVE_TYPE_ID");

                entity.Property(e => e.MaximumNumberOfDays).HasColumnName("MAXIMUM_NUMBER_OF_DAYS");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.LeaveProperties)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LEAVE_PROPERTIES_LEAVE_PROPERTIES");
            });

            modelBuilder.Entity<LeaveScheme>(entity =>
            {
                entity.HasKey(e => e.SchemeId);

                entity.ToTable("LEAVE_SCHEME");

                entity.Property(e => e.SchemeId).HasColumnName("SCHEME_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.LeaveTypeId).HasColumnName("LEAVE_TYPE_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.NumberOfDays).HasColumnName("NUMBER_OF_DAYS");

                entity.Property(e => e.NumberOfMonths).HasColumnName("NUMBER_OF_MONTHS");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.LeaveSchemes)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LEAVE_SCHEME_LEAVE_TYPE");
            });

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.ToTable("LEAVE_TYPE");

                entity.Property(e => e.LeaveTypeId).HasColumnName("LEAVE_TYPE_ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(550)
                    .HasColumnName("DESCRIPTION")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.ToTable("OFFICES");

                entity.HasIndex(e => e.Code, "IX_OFFICES")
                    .IsUnique();

                entity.Property(e => e.OfficeId).HasColumnName("OFFICE_ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(550)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("POSITION");

                entity.Property(e => e.PositionId).HasColumnName("POSITION_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Privilege>(entity =>
            {
                entity.ToTable("PRIVILEGE");

                entity.Property(e => e.PrivilegeId).HasColumnName("PRIVILEGE_ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLES");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.SupervisorId).HasColumnName("SUPERVISOR-ID");
            });

            modelBuilder.Entity<RolePrivilege>(entity =>
            {
                entity.ToTable("ROLE_PRIVILEGE");

                entity.Property(e => e.RolePrivilegeId).HasColumnName("ROLE_PRIVILEGE_ID");

                entity.Property(e => e.PrivilegeId).HasColumnName("PRIVILEGE_ID");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.HasOne(d => d.Privilege)
                    .WithMany(p => p.RolePrivileges)
                    .HasForeignKey(d => d.PrivilegeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_PRIVILEGE_ROLES");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePrivileges)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_PRIVILEGE_ROLES1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("GENDER");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MOBILE_NUMBER");
            });

            modelBuilder.Entity<UserCredential>(entity =>
            {
                entity.ToTable("USER_CREDENTIALS");

                entity.Property(e => e.UserCredentialId)
                    .ValueGeneratedNever()
                    .HasColumnName("USER_CREDENTIAL_ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCredentials)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_CREDENTIALS_USER_CREDENTIALS");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("USER_ROLE");

                entity.Property(e => e.UserRoleId).HasColumnName("USER_ROLE_ID");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_ROLE_ROLES");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_ROLE_USERS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
