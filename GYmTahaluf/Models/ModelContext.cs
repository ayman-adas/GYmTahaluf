using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GYmTahaluf.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCustomer> ProductCustomers { get; set; }

    public virtual DbSet<Promocode> Promocodes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<URole> URoles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("DATA SOURCE=192.168.1.15:1521;USER ID=Ayman123;PASSWORD=Ayman123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("AYMAN123")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("SYS_C008340");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008371");

            entity.ToTable("CONTACT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Facebook)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FACEBOOK");
            entity.Property(e => e.Insta)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("INSTA");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PHONE");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("SYS_C008337");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LNAME");
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008408");

            entity.ToTable("DAYS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.DaysName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DAYS_NAME");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008383");

            entity.ToTable("EXERCISE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Difficulty)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DIFFICULTY");
            entity.Property(e => e.PlanId)
                .HasColumnType("NUMBER")
                .HasColumnName("PLAN_ID");
            entity.Property(e => e.Repetition)
                .HasColumnType("NUMBER")
                .HasColumnName("REPETITION");
            entity.Property(e => e.RestPeriod)
                .HasColumnType("NUMBER")
                .HasColumnName("REST_PERIOD");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.Plan).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXERCISE_PLAN");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008403");

            entity.ToTable("INVOICES");

            entity.HasIndex(e => e.InvoiceNumber, "SYS_C008404").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.GeneratedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("GENERATED_AT");
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("INVOICE_NUMBER");
            entity.Property(e => e.PaymentId)
                .HasColumnType("NUMBER")
                .HasColumnName("PAYMENT_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("'pending'")
                .HasColumnName("STATUS");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URL");

            entity.HasOne(d => d.Payment).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INVOICE_PAYMENT");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008369");

            entity.ToTable("PAGES");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasColumnType("CLOB")
                .HasColumnName("CONTENT");
            entity.Property(e => e.Image)
                .HasColumnType("BLOB")
                .HasColumnName("IMAGE");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP         -- Timestamp for last update\n")
                .HasColumnName("UPDATED_AT");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008395");

            entity.ToTable("PAYMENT");

            entity.HasIndex(e => e.TransactionRef, "SYS_C008396").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Amount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.IsUsedPromo)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("IS_USED_PROMO");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("'pending'")
                .HasColumnName("PAYMENT_STATUS");
            entity.Property(e => e.PlanId)
                .HasColumnType("NUMBER")
                .HasColumnName("PLAN_ID");
            entity.Property(e => e.Promocode)
                .HasColumnType("NUMBER")
                .HasColumnName("PROMOCODE");
            entity.Property(e => e.TransactionDate)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("TRANSACTION_DATE");
            entity.Property(e => e.TransactionRef)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TRANSACTION_REF");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Plan).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PAYMENT_PLAN");

            entity.HasOne(d => d.PromocodeNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Promocode)
                .HasConstraintName("FK_PAYMENT_PROMO");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PAYMENT_USER");
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008426");

            entity.ToTable("PAYMENT_STATUS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("SYS_C008342");

            entity.ToTable("PRODUCT");

            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PRODUCT_ID");
            entity.Property(e => e.CategoryId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Namee)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAMEE");
            entity.Property(e => e.Price)
                .HasColumnType("FLOAT")
                .HasColumnName("PRICE");
            entity.Property(e => e.Sale)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SALE");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_CATEGORY_ID");
        });

        modelBuilder.Entity<ProductCustomer>(entity =>
        {
            entity.HasKey(e => e.ProductCustomerId).HasName("SYS_C008345");

            entity.ToTable("PRODUCT_CUSTOMER");

            entity.Property(e => e.ProductCustomerId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PRODUCT_CUSTOMER_ID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.DateFrom)
                .HasColumnType("DATE")
                .HasColumnName("DATE_FROM");
            entity.Property(e => e.DateTo)
                .HasColumnType("DATE")
                .HasColumnName("DATE_TO");
            entity.Property(e => e.ProductId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("QUANTITY");

            entity.HasOne(d => d.Customer).WithMany(p => p.ProductCustomers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK1_CUSTOMER_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCustomers)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK1_PRODUCT_ID");
        });

        modelBuilder.Entity<Promocode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008364");

            entity.ToTable("PROMOCODE");

            entity.HasIndex(e => e.Code, "SYS_C008365").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODE");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.EndDate)
                .HasPrecision(6)
                .HasColumnName("END_DATE");
            entity.Property(e => e.Isvisible)
                .HasPrecision(1)
                .HasDefaultValueSql("1")
                .HasColumnName("ISVISIBLE");
            entity.Property(e => e.MaxUses)
                .HasColumnType("NUMBER")
                .HasColumnName("MAX_USES");
            entity.Property(e => e.MinAmount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("MIN_AMOUNT");
            entity.Property(e => e.Percent)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("PERCENT");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008350");

            entity.ToTable("ROLE");

            entity.HasIndex(e => e.RoleName, "SYS_C008351").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008420");

            entity.ToTable("SCHEDULE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Isavailable)
                .HasPrecision(1)
                .HasDefaultValueSql("1")
                .HasColumnName("ISAVAILABLE");
            entity.Property(e => e.PlanId)
                .HasColumnType("NUMBER")
                .HasColumnName("PLAN_ID");
            entity.Property(e => e.StartDay)
                .HasColumnType("NUMBER")
                .HasColumnName("START_DAY");
            entity.Property(e => e.StartTime)
                .HasPrecision(6)
                .HasColumnName("START_TIME");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("UPDATED_AT");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Plan).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SCHEDULE_PLAN");

            entity.HasOne(d => d.StartDayNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.StartDay)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SCHEDULE_DAYS");

            entity.HasOne(d => d.User).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SCHEDULE_USER");
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008376");

            entity.ToTable("SUBSCRIPTION_PLANS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DaysInWeek)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("DAYS_IN_WEEK");
            entity.Property(e => e.Descriptions)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTIONS");
            entity.Property(e => e.EndDate)
                .HasPrecision(6)
                .HasColumnName("END_DATE");
            entity.Property(e => e.Goal)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("GOAL");
            entity.Property(e => e.Isvisible)
                .HasPrecision(1)
                .HasDefaultValueSql("1")
                .HasColumnName("ISVISIBLE");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.StartDate)
                .HasPrecision(6)
                .HasColumnName("START_DATE");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITLE");
            entity.Property(e => e.TrainerId)
                .HasColumnType("NUMBER")
                .HasColumnName("TRAINER_ID");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("UPDATED_AT");

            entity.HasOne(d => d.Trainer).WithMany(p => p.SubscriptionPlans)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("FK_SUBSCRIPTION_TRAINER");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008428");

            entity.ToTable("TESTIMONIAL");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("AYMAN123.ISEQ$$_78324.nextval ")
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE");
            entity.Property(e => e.Isadminreview)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ISADMINREVIEW");
            entity.Property(e => e.Isapproved)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ISAPPROVED");
            entity.Property(e => e.Isdeleted)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ISDELETED");
            entity.Property(e => e.Priority)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("'low'")
                .HasColumnName("PRIORITY");
            entity.Property(e => e.Rate)
                .HasColumnType("NUMBER")
                .HasColumnName("RATE");
            entity.Property(e => e.RatedTime)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("RATED_TIME");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TEXT");
            entity.Property(e => e.TrainerId)
                .HasColumnType("NUMBER")
                .HasColumnName("TRAINER_ID");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("UPDATED_AT");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Trainer).WithMany(p => p.TestimonialTrainers)
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_TESTIMONIAL_TRAINER");

            entity.HasOne(d => d.User).WithMany(p => p.TestimonialUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TESTIMONIAL_USER");
        });

        modelBuilder.Entity<URole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("SYS_C008332");

            entity.ToTable("U_ROLES");

            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008357");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Email, "SYS_C008358").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Isblock)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ISBLOCK");
            entity.Property(e => e.LastLogin)
                .HasPrecision(6)
                .HasColumnName("LAST_LOGIN");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.UpdateAt)
                .HasPrecision(6)
                .HasColumnName("UPDATE_AT");
            entity.Property(e => e.UserImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("USER_IMAGE");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_ROLE");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.UserLoginId).HasName("SYS_C008334");

            entity.ToTable("USER_LOGIN");

            entity.Property(e => e.UserLoginId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_LOGIN_ID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Passwordd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASSWORDD");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");

            entity.HasOne(d => d.Customer).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_CUSTOMER_ID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ROLE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
