using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyFirstWebProject.Models;

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
        => optionsBuilder.UseSqlServer("Server=DESKTOP-9IDNILH;Database=BusinessPartners;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=Yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bp>(entity =>
        {
            entity.HasKey(e => e.Bpcode).HasName("PK__BP__050B43D98EFACC21");

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
                .HasConstraintName("FK__BP__BPType__5070F446");
        });

        modelBuilder.Entity<Bptype>(entity =>
        {
            entity.HasKey(e => e.TypeCode).HasName("PK__BPType__3E1CDC7D16D106FE");

            entity.ToTable("BPType");

            entity.Property(e => e.TypeCode).HasMaxLength(1);
            entity.Property(e => e.TypeName).HasMaxLength(20);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemCode).HasName("PK__Items__3ECC0FEBA71FE406");

            entity.Property(e => e.ItemCode).HasMaxLength(128);
            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.ItemName).HasMaxLength(254);
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Purchase__3214EC27EC2A889C");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bpcode)
                .HasMaxLength(128)
                .HasColumnName("BPCode");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.BpcodeNavigation).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.Bpcode)
                .HasConstraintName("FK__PurchaseO__BPCod__6477ECF3");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PurchaseOrderCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__PurchaseO__Creat__656C112C");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.PurchaseOrderLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("FK__PurchaseO__LastU__66603565");
        });

        modelBuilder.Entity<PurchaseOrdersLine>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK__Purchase__2EAE64C9A8DB2545");

            entity.Property(e => e.LineId).HasColumnName("LineID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DocId).HasColumnName("DocID");
            entity.Property(e => e.ItemCode).HasMaxLength(128);
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(38, 18)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PurchaseOrdersLineCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__PurchaseO__Creat__6B24EA82");

            entity.HasOne(d => d.Doc).WithMany(p => p.PurchaseOrdersLines)
                .HasForeignKey(d => d.DocId)
                .HasConstraintName("FK__PurchaseO__DocID__693CA210");

            entity.HasOne(d => d.ItemCodeNavigation).WithMany(p => p.PurchaseOrdersLines)
                .HasForeignKey(d => d.ItemCode)
                .HasConstraintName("FK__PurchaseO__ItemC__6A30C649");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.PurchaseOrdersLineLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("FK__PurchaseO__LastU__6C190EBB");
        });

        modelBuilder.Entity<SaleOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SaleOrde__3214EC274B36EEDB");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bpcode)
                .HasMaxLength(128)
                .HasColumnName("BPCode");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.BpcodeNavigation).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.Bpcode)
                .HasConstraintName("FK__SaleOrder__BPCod__5629CD9C");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SaleOrderCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__SaleOrder__Creat__571DF1D5");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.SaleOrderLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("FK__SaleOrder__LastU__5812160E");
        });

        modelBuilder.Entity<SaleOrdersLine>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK__SaleOrde__2EAE64C933DE5259");

            entity.Property(e => e.LineId).HasColumnName("LineID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DocId).HasColumnName("DocID");
            entity.Property(e => e.ItemCode).HasMaxLength(128);
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(38, 18)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SaleOrdersLineCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__SaleOrder__Creat__5CD6CB2B");

            entity.HasOne(d => d.Doc).WithMany(p => p.SaleOrdersLines)
                .HasForeignKey(d => d.DocId)
                .HasConstraintName("FK__SaleOrder__DocID__5AEE82B9");

            entity.HasOne(d => d.ItemCodeNavigation).WithMany(p => p.SaleOrdersLines)
                .HasForeignKey(d => d.ItemCode)
                .HasConstraintName("FK__SaleOrder__ItemC__5BE2A6F2");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.SaleOrdersLineLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("FK__SaleOrder__LastU__5DCAEF64");
        });

        modelBuilder.Entity<SaleOrdersLinesComment>(entity =>
        {
            entity.HasKey(e => e.CommentLineId).HasName("PK__SaleOrde__4F38FB07A83F9D4A");

            entity.Property(e => e.CommentLineId).HasColumnName("CommentLineID");
            entity.Property(e => e.DocId).HasColumnName("DocID");
            entity.Property(e => e.LineId).HasColumnName("LineID");

            entity.HasOne(d => d.Doc).WithMany(p => p.SaleOrdersLinesComments)
                .HasForeignKey(d => d.DocId)
                .HasConstraintName("FK__SaleOrder__DocID__60A75C0F");

            entity.HasOne(d => d.Line).WithMany(p => p.SaleOrdersLinesComments)
                .HasForeignKey(d => d.LineId)
                .HasConstraintName("FK__SaleOrder__LineI__619B8048");
        });

        modelBuilder.Entity<UserTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserTbl__3214EC2716B1168C");

            entity.ToTable("UserTbl");

            entity.HasIndex(e => e.UserName, "UQ__UserTbl__C9F2845650428622").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.FullName).HasMaxLength(1024);
            entity.Property(e => e.UserName).HasMaxLength(254);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
