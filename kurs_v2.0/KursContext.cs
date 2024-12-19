using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace kurs_v2._0;

public partial class KursContext : DbContext
{
    public KursContext()
    {
    }

    public KursContext(DbContextOptions<KursContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agency> Agencys { get; set; }

    public virtual DbSet<AveragePriceByPropertyType> AveragePriceByPropertyTypes { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<MostProfitablePropertyType> MostProfitablePropertyTypes { get; set; }

    public virtual DbSet<Property> Propertys { get; set; }

    public virtual DbSet<TotalTransactionAmountByMonthAndAgency> TotalTransactionAmountByMonthAndAgencies { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionCountAndTotalAmountByTransactionType> TransactionCountAndTotalAmountByTransactionTypes { get; set; }

    public virtual DbSet<TransactionsDetail> TransactionsDetails { get; set; }

    public virtual DbSet<TypeTransaction> TypeTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress; Database=Kurs; User=исп-44; Password=1234567890; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agency>(entity =>
        {
            entity.HasKey(e => e.AgencyId).HasName("PK__Agencys__95C546FB6EA8777F");

            entity.Property(e => e.AgencyId).HasColumnName("AgencyID");
            entity.Property(e => e.AddressAgency).HasMaxLength(200);
            entity.Property(e => e.NameAgency).HasMaxLength(100);
            entity.Property(e => e.PhoneAgency)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<AveragePriceByPropertyType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("AveragePriceByPropertyType");

            entity.Property(e => e.AveragePrice).HasColumnType("money");
            entity.Property(e => e.PropertyType).HasMaxLength(50);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientsId).HasName("PK__Clients__6EDBB772FD0A2F77");

            entity.Property(e => e.ClientsId).HasColumnName("ClientsID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<MostProfitablePropertyType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MostProfitablePropertyType");

            entity.Property(e => e.PropertyType).HasMaxLength(50);
            entity.Property(e => e.TotalProfit).HasColumnType("money");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("PK__Property__70C9A755383CE8EF");

            entity.Property(e => e.PropertyId).HasColumnName("PropertyID");
            entity.Property(e => e.AddressProperty).HasMaxLength(200);
            entity.Property(e => e.Class).HasMaxLength(50);
            entity.Property(e => e.Condition).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<TotalTransactionAmountByMonthAndAgency>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TotalTransactionAmountByMonthAndAgency");

            entity.Property(e => e.NameAgency).HasMaxLength(100);
            entity.Property(e => e.TotalAmount).HasColumnType("money");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionsId).HasName("PK__Transact__B872AB6A7BFF160F");

            entity.Property(e => e.TransactionsId).HasColumnName("TransactionsID");
            entity.Property(e => e.AgencyId).HasColumnName("AgencyID");
            entity.Property(e => e.AmountTransaction).HasColumnType("money");
            entity.Property(e => e.ClientsId).HasColumnName("ClientsID");
            entity.Property(e => e.PropertyId).HasColumnName("PropertyID");

            entity.HasOne(d => d.Agency).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AgencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Agenc__412EB0B6");

            entity.HasOne(d => d.Clients).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ClientsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Clien__403A8C7D");

            entity.HasOne(d => d.Property).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Prope__3F466844");
        });

        modelBuilder.Entity<TransactionCountAndTotalAmountByTransactionType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TransactionCountAndTotalAmountByTransactionType");

            entity.Property(e => e.TotalAmount).HasColumnType("money");
            entity.Property(e => e.TransactionType).HasMaxLength(50);
        });

        modelBuilder.Entity<TransactionsDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TransactionsDetails");

            entity.Property(e => e.AddressProperty).HasMaxLength(200);
            entity.Property(e => e.AmountTransaction).HasColumnType("money");
            entity.Property(e => e.ClientFirstName).HasMaxLength(100);
            entity.Property(e => e.ClientLastName).HasMaxLength(100);
            entity.Property(e => e.ClientMiddleName).HasMaxLength(100);
            entity.Property(e => e.NameAgency).HasMaxLength(100);
            entity.Property(e => e.PropertyClass).HasMaxLength(50);
            entity.Property(e => e.TransactionsId).HasColumnName("TransactionsID");
        });

        modelBuilder.Entity<TypeTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId).HasName("PK__TypeTran__20266CEB5806AAD7");

            entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");
            entity.Property(e => e.TransactionsId).HasColumnName("TransactionsID");
            entity.Property(e => e.TypeName).HasMaxLength(50);

            entity.HasOne(d => d.Transactions).WithMany(p => p.TypeTransactions)
                .HasForeignKey(d => d.TransactionsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TypeTrans__Trans__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
