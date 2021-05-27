using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NotaryDatabaseDLL.Models
{
    public partial class NotaryOfficeContext : DbContext
    {
        public NotaryOfficeContext()
        {
        }

        public NotaryOfficeContext(DbContextOptions<NotaryOfficeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assistant> Assistants { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Notary> Notaries { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Reception> Receptions { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }
        public virtual DbSet<WorkerService> WorkerServices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=127.0.0.1,8080;Initial Catalog=NotaryOffice;Persist Security Info=True;User ID=sa;Password=Qwerty123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Assistant>(entity =>
            {
                entity.Property(e => e.AssistantId).HasColumnName("Assistant_id");

                entity.Property(e => e.NotaryId).HasColumnName("Notary_id");

                entity.Property(e => e.Specialization)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.WorkerId).HasColumnName("Worker_id");

                entity.HasOne(d => d.Notary)
                    .WithMany(p => p.Assistants)
                    .HasForeignKey(d => d.NotaryId)
                    .HasConstraintName("ASS_NOT_FK");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Assistants)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ASS_WORKER_FK");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(e => e.CityName, "City_City_name");

                entity.Property(e => e.CityId).HasColumnName("City_id");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("City_name");

                entity.Property(e => e.CityType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("City_type");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.FirstName, "Client_First_name");

                entity.HasIndex(e => e.LastName, "Client_Last_name");

                entity.HasIndex(e => e.PassportNumber, "Client_Passport_UK")
                    .IsUnique();

                entity.Property(e => e.ClientId).HasColumnName("Client_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("First_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Last_name");

                entity.Property(e => e.PassportNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("Passport_number");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(12)
                    .HasColumnName("Phone_number");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasIndex(e => e.DocumentName, "Doc_Document_name");

                entity.Property(e => e.DocumentId).HasColumnName("Document_id");

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Document_name");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasIndex(e => e.Address, "Loc_Location_name");

                entity.Property(e => e.LocationId).HasColumnName("Location_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CityId).HasColumnName("City_id");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LOC_CITY_FK");
            });

            modelBuilder.Entity<Notary>(entity =>
            {
                entity.Property(e => e.NotaryId).HasColumnName("Notary_id");

                entity.Property(e => e.CertificateNumber)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Certificate_number");

                entity.Property(e => e.WorkerId).HasColumnName("Worker_id");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Notaries)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("NOT_WORKER_FK");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.HasIndex(e => e.OfficeName, "Off_Office_name");

                entity.Property(e => e.OfficeId).HasColumnName("Office_id");

                entity.Property(e => e.LocationId).HasColumnName("Location_id");

                entity.Property(e => e.OfficeName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Office_name");

                entity.Property(e => e.OfficeSize)
                    .HasMaxLength(20)
                    .HasColumnName("Office_size");

                entity.Property(e => e.OfficeStatus)
                    .HasMaxLength(20)
                    .HasColumnName("Office_status");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OFF_LOC_FK");
            });

            modelBuilder.Entity<Reception>(entity =>
            {
                entity.HasIndex(e => e.ReceptionDate, "Rec_Reception_date");

                entity.Property(e => e.ReceptionId).HasColumnName("Reception_id");

                entity.Property(e => e.ClientId).HasColumnName("Client_id");

                entity.Property(e => e.DocumentId).HasColumnName("Document_id");

                entity.Property(e => e.NotaryId).HasColumnName("Notary_id");

                entity.Property(e => e.ReceptionDate)
                    .HasColumnType("date")
                    .HasColumnName("Reception_date");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("REC_CLIENT_FK");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("REC_DOC_FK");

                entity.HasOne(d => d.Notary)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.NotaryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("REC_NOT_FK");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasIndex(e => e.ServiceName, "Serv_Service_name");

                entity.Property(e => e.ServiceId).HasColumnName("Service_id");

                entity.Property(e => e.Complexity).HasMaxLength(25);

                entity.Property(e => e.Importance).HasMaxLength(25);

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Service_name");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasIndex(e => e.FirstName, "Worker_First_name");

                entity.HasIndex(e => e.LastName, "Worker_Last_name");

                entity.HasIndex(e => e.PassportNumber, "Worker_Passport_UK")
                    .IsUnique();

                entity.Property(e => e.WorkerId).HasColumnName("Worker_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("First_name");

                entity.Property(e => e.HireDate)
                    .HasColumnType("date")
                    .HasColumnName("Hire_date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Last_name");

                entity.Property(e => e.OfficeId).HasColumnName("Office_id");

                entity.Property(e => e.PassportNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("Passport_number");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("Phone_number");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("WORKER_OFFICE_FK");
            });

            modelBuilder.Entity<WorkerService>(entity =>
            {
                entity.ToTable("Worker_Services");

                entity.Property(e => e.WorkerServiceId).HasColumnName("Worker_service_id");

                entity.Property(e => e.ServiceId).HasColumnName("Service_id");

                entity.Property(e => e.WorkerId).HasColumnName("Worker_id");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.WorkerServices)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("WS_SERVICE_FK");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.WorkerServices)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("WS_WORKER_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
