using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Entities;
#nullable disable

namespace DL
{
    public partial class VehicleRental_dbContext : DbContext
    {
        public VehicleRental_dbContext()
        {
        }

        public VehicleRental_dbContext(DbContextOptions<VehicleRental_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerTbl> CustomerTbls { get; set; }
        public virtual DbSet<DescVehicleTbl> DescVehicleTbls { get; set; }
        public virtual DbSet<DesignsTbl> DesignsTbls { get; set; }
        public virtual DbSet<OrdersTbl> OrdersTbls { get; set; }
        public virtual DbSet<StationTbl> StationTbls { get; set; }
        public virtual DbSet<VehicleDesignsTbl> VehicleDesignsTbls { get; set; }
        public virtual DbSet<VehicleTypeTbl> VehicleTypeTbls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=srv2\\pupils;Database=VehicleRentals_db;Trusted_Connection=True;");
            }
            //"Server=LAPTOP-ODKQSO4A;Database=VehicleRental_db;
            //Server=srv2\\pupils;Database=VehicleRentals_db;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<CustomerTbl>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__Customer__DB43864A126208BF");

                entity.ToTable("Customer_tbl");

                entity.HasIndex(e => e.PasswordCust, "UQ__Customer__F6967CAE61B03791")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordCust)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DescVehicleTbl>(entity =>
            {
                entity.HasKey(e => e.IdVehicle)
                    .HasName("PK__DescVehi__64D74CC861A9A4CF");

                entity.ToTable("DescVehicle_tbl");

                entity.Property(e => e.Company)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GearBox).HasDefaultValueSql("((1))");

                entity.Property(e => e.PricePerDay).HasColumnType("money");

                entity.Property(e => e.PricePerHour).HasColumnType("money");

                entity.Property(e => e.Production).HasColumnType("datetime");

                entity.Property(e => e.TrunckSize)
                    .HasMaxLength(20)
                    .IsUnicode(false);


                entity.HasOne(d => d.IdVehicleTypeNavigation)
                    .WithMany(p => p.DescVehicleTbls)
                    .HasForeignKey(d => d.IdVehicleType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DescVehic__IdVeh__38996AB5");
            });

            modelBuilder.Entity<DesignsTbl>(entity =>
            {
                entity.HasKey(e => e.IdDesign)
                    .HasName("PK__Designs___D3386497E6566F22");

                entity.ToTable("Designs_tbl");

                entity.Property(e => e.DescDesign)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DesignPrice).HasColumnType("money");
            });

            modelBuilder.Entity<OrdersTbl>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PK__Orders_t__C38F300919D2EC95");

                entity.ToTable("Orders_tbl");

                entity.Property(e => e.IdCustomer)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderHour).HasColumnType("datetime");

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnHour).HasColumnType("datetime");

               // entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.OrdersTbls)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders_tb__IdCus__44FF419A");

                entity.HasOne(d => d.IdDesignNavigation)
                    .WithMany(p => p.OrdersTbls)
                    .HasForeignKey(d => d.IdDesign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders_tb__IdDes__440B1D61");

                entity.HasOne(d => d.IdStationNavigation)
                    .WithMany(p => p.OrdersTbls)
                    .HasForeignKey(d => d.IdStation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders_tb__IdSta__4222D4EF");

                entity.HasOne(d => d.IdVehicleNavigation)
                    .WithMany(p => p.OrdersTbls)
                    .HasForeignKey(d => d.IdVehicle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders_tb__IdVeh__4316F928");
            });

            modelBuilder.Entity<StationTbl>(entity =>
            {
                entity.HasKey(e => e.IdStation)
                    .HasName("PK__Station___FBB9BA0A2DA3CA7A");
              
                entity.ToTable("Station_tbl");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Neighborhood)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VehicleDesignsTbl>(entity =>
            {
                entity.HasKey(e => e.IdVehicleDesign)
                    .HasName("PK__VehicleD__EC1573170A7153A0");

                entity.ToTable("VehicleDesigns_tbl");

                entity.HasOne(d => d.IdDesignNavigation)
                    .WithMany(p => p.VehicleDesignsTbls)
                    .HasForeignKey(d => d.IdDesign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VehicleDe__IdDes__47DBAE45");

                entity.HasOne(d => d.IdVehicleTypeNavigation)
                    .WithMany(p => p.VehicleDesignsTbls)
                    .HasForeignKey(d => d.IdVehicleType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VehicleDe__IdVeh__48CFD27E");
            });

            modelBuilder.Entity<VehicleTypeTbl>(entity =>
            {
                entity.HasKey(e => e.IdVehicleType)
                    .HasName("PK__VehicleT__4CC6297403FBA570");

                entity.ToTable("VehicleType_tbl");

                entity.Property(e => e.VehicleType)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
