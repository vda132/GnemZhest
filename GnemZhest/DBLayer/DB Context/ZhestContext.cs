using DBLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace DBLayer;
public partial class ZhestContext : DbContext
{
    private readonly string connectionString = string.Empty;

    public ZhestContext() : base()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("D:\\GnemZhest.Site\\GnemZhest\\DBLayer\\config.json")
            .Build();

        this.connectionString = config["ConnectionStrings:DefaultConnectionVadim"];
    }

    public ZhestContext(string connectionString) : base() =>
        this.connectionString=connectionString;

    public DbSet<Order> Orders { get; set; }
    public DbSet<Good> Goods { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(connectionString);
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Good>(this.GoodConfigure);
        modelBuilder.Entity<User>(this.UserConfigure);
        modelBuilder.Entity<Order>(this.OrderConfigure);
        this.OnModelCreatingPartial(modelBuilder);
    }

    private void GoodConfigure(EntityTypeBuilder<Good> builder)
    {
        builder.ToTable("Good")
            .HasKey(x => x.Id)
            .HasName("Good_PK");

        builder.Property(x => x.Id)
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(x => x.Name)
            .HasColumnType("varchar(max)")
            .HasColumnName("Name");

        builder.Property(x => x.Price)
            .HasColumnType("money")
            .HasColumnName("Price");

        builder.Property(x => x.IsAvaliable)
            .HasColumnType("bit")
            .HasColumnName("IsAvaliable");

        builder.Property(x => x.Image1)
            .HasColumnType("varchar(max)")
            .HasColumnName("Image1");

        builder.Property(x => x.Image2)
           .HasColumnType("varchar(max)")
           .HasColumnName("Image2");

        builder.Property(x => x.Image3)
           .HasColumnType("varchar(max)")
           .HasColumnName("Image3");

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Good)
            .HasForeignKey(x => x.GoodId)   
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void OrderConfigure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order")
            .HasKey(x => x.Id)
            .HasName("Order_PK");

        builder.Property(x => x.Id)
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(x => x.OrderAdress)
            .HasColumnType("varchar(max)")
            .HasColumnName("OrderAdress");

        builder.Property(x => x.City)
            .HasColumnType("varchar(max)")
            .HasColumnName("City");

        builder.Property(x => x.Ammount)
            .HasColumnType("int")
            .HasColumnName("Ammount");

        builder.Property(x => x.GoodId)
            .HasColumnType("int")
            .HasColumnName("GoodId");

        builder.Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasColumnType("int");

        builder.HasOne(x => x.Good)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.GoodId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("Order_GoodId_FK");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("Order_User_FK");
    }

    private void UserConfigure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User")
            .HasKey(x => x.Id)
            .HasName("User_PK");

        builder.Property(x => x.Id)
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(x => x.Name)
            .HasColumnType("varchar(max)")
            .HasColumnName("Name");

        builder.Property(x => x.Email)
            .HasColumnType("varchar(max)")
            .HasColumnName("Email");

        builder.Property(x => x.SurName)
            .HasColumnType("varchar(max)")
            .HasColumnName("SurName");

        builder.Property(x => x.Login)
            .HasColumnType("varchar(max)")
            .HasColumnName("Login");

        builder.Property(x => x.Password)
            .HasColumnType("varchar(max)")
            .HasColumnName("Password");

        builder.Property(x => x.Phone)
            .HasColumnType("varchar(max)")
            .HasColumnName("Phone");

        builder.Property(x => x.Role)
            .HasColumnType("varchar(max)")
            .HasColumnName("Role");

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
