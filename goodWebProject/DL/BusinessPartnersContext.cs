using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace DL;

public partial class BusinessPartnersContext : DbContext
{
    public BusinessPartnersContext()
    {
    }

    public BusinessPartnersContext(DbContextOptions<BusinessPartnersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bp> Bps { get; set; }

    public virtual DbSet<Bptype> Bptypes { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrdersLine> PurchaseOrdersLines { get; set; }

    public virtual DbSet<SaleOrder> SaleOrders { get; set; }

    public virtual DbSet<SaleOrdersLine> SaleOrdersLines { get; set; }

    public virtual DbSet<SaleOrdersLinesComment> SaleOrdersLinesComments { get; set; }

    public virtual DbSet<UserTbl> UserTbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-E0FAPSB\\SQLEXPRESS;Database=BusinessPartners;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=Yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bp>(entity =>
        {
            entity.HasKey(e => e.Bpcode).HasName("PK__BP__050B43D9D9F524C0");

            entity.ToTable("BP");

            entity.Property(e => e.Bpcode)
                .HasMaxLength(128)
                .HasColumnName("BPCode");
            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.Bpname)
                .HasMaxLength(254)
                .HasColumnName("BPName");
            entity.Property(e => e.Bptype)
                .HasMaxLength(1)
                .HasColumnName("BPType");

            entity.HasOne(d => d.BptypeNavigation).WithMany(p => p.Bps)
                .HasForeignKey(d => d.Bptype)
                .HasConstraintName("FK__BP__BPType__2F10007B");
        });

        modelBuilder.Entity<Bptype>(entity =>
        {
            entity.HasKey(e => e.TypeCode).HasName("PK__BPType__3E1CDC7DAA88612D");

            entity.ToTable("BPType");

            entity.Property(e => e.TypeCode).HasMaxLength(1);
            entity.Property(e => e.TypeName).HasMaxLength(20);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemCode).HasName("PK__Items__3ECC0FEBB23D3A8F");

            entity.Property(e => e.ItemCode).HasMaxLength(128);
            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.ItemName).HasMaxLength(254);
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Purchase__3214EC27310A7CF0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bpcode)
                .HasMaxLength(128)
                .HasColumnName("BPCode");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.BpcodeNavigation).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.Bpcode)
                .HasConstraintName("FK__PurchaseO__BPCod__4316F928");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PurchaseOrderCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__PurchaseO__Creat__440B1D61");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.PurchaseOrderLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("FK__PurchaseO__LastU__44FF419A");
        });

        modelBuilder.Entity<PurchaseOrdersLine>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK__Purchase__2EAE64C9F7C4968D");

            entity.Property(e => e.LineId).HasColumnName("LineID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DocId).HasColumnName("DocID");
            entity.Property(e => e.ItemCode).HasMaxLength(128);
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(38, 18)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PurchaseOrdersLineCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__PurchaseO__Creat__49C3F6B7");

            entity.HasOne(d => d.Doc).WithMany(p => p.PurchaseOrdersLines)
                .HasForeignKey(d => d.DocId)
                .HasConstraintName("FK__PurchaseO__DocID__47DBAE45");

            entity.HasOne(d => d.ItemCodeNavigation).WithMany(p => p.PurchaseOrdersLines)
                .HasForeignKey(d => d.ItemCode)
                .HasConstraintName("FK__PurchaseO__ItemC__48CFD27E");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.PurchaseOrdersLineLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("FK__PurchaseO__LastU__4AB81AF0");
        });

        modelBuilder.Entity<SaleOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SaleOrde__3214EC270A25B4EC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bpcode)
                .HasMaxLength(128)
                .HasColumnName("BPCode");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.BpcodeNavigation).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.Bpcode)
                .HasConstraintName("FK__SaleOrder__BPCod__34C8D9D1");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SaleOrderCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__SaleOrder__Creat__35BCFE0A");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.SaleOrderLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("FK__SaleOrder__LastU__36B12243");
        });

        modelBuilder.Entity<SaleOrdersLine>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK__SaleOrde__2EAE64C9AC6F3B38");

            entity.Property(e => e.LineId).HasColumnName("LineID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DocId).HasColumnName("DocID");
            entity.Property(e => e.ItemCode).HasMaxLength(128);
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(38, 18)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SaleOrdersLineCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__SaleOrder__Creat__3B75D760");

            entity.HasOne(d => d.Doc).WithMany(p => p.SaleOrdersLines)
                .HasForeignKey(d => d.DocId)
                .HasConstraintName("FK__SaleOrder__DocID__398D8EEE");

            entity.HasOne(d => d.ItemCodeNavigation).WithMany(p => p.SaleOrdersLines)
                .HasForeignKey(d => d.ItemCode)
                .HasConstraintName("FK__SaleOrder__ItemC__3A81B327");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.SaleOrdersLineLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("FK__SaleOrder__LastU__3C69FB99");
        });

        modelBuilder.Entity<SaleOrdersLinesComment>(entity =>
        {
            entity.HasKey(e => e.CommentLineId).HasName("PK__SaleOrde__4F38FB07B5502A9B");

            entity.Property(e => e.CommentLineId).HasColumnName("CommentLineID");
            entity.Property(e => e.DocId).HasColumnName("DocID");
            entity.Property(e => e.LineId).HasColumnName("LineID");

            entity.HasOne(d => d.Doc).WithMany(p => p.SaleOrdersLinesComments)
                .HasForeignKey(d => d.DocId)
                .HasConstraintName("FK__SaleOrder__DocID__3F466844");

            entity.HasOne(d => d.Line).WithMany(p => p.SaleOrdersLinesComments)
                .HasForeignKey(d => d.LineId)
                .HasConstraintName("FK__SaleOrder__LineI__403A8C7D");
        });

        modelBuilder.Entity<UserTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserTbl__3214EC27CF3392AE");

            entity.ToTable("UserTbl");

            entity.HasIndex(e => e.UserName, "UQ__UserTbl__C9F28456647F5975").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.FullName).HasMaxLength(1024);
            entity.Property(e => e.UserName).HasMaxLength(254);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
