// <auto-generated />
using DBLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DBLayer.Migrations
{
    [DbContext(typeof(ZhestContext))]
    [Migration("20220525100326_add ammount to order")]
    partial class addammounttoorder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DBLayer.Models.Good", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Image1")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Image1");

                    b.Property<string>("Image2")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Image2");

                    b.Property<string>("Image3")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Image3");

                    b.Property<bool>("IsAvaliable")
                        .HasColumnType("bit")
                        .HasColumnName("IsAvaliable");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasColumnName("Price");

                    b.HasKey("Id")
                        .HasName("Good_PK");

                    b.ToTable("Good", (string)null);
                });

            modelBuilder.Entity("DBLayer.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Ammount")
                        .HasColumnType("int")
                        .HasColumnName("Ammount");

                    b.Property<string>("City")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("City");

                    b.Property<int>("GoodId")
                        .HasColumnType("int")
                        .HasColumnName("GoodId");

                    b.Property<string>("OrderAdress")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("OrderAdress");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id")
                        .HasName("Order_PK");

                    b.HasIndex("GoodId");

                    b.HasIndex("UserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("DBLayer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Phone");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Role");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("SurName");

                    b.HasKey("Id")
                        .HasName("User_PK");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("DBLayer.Models.Order", b =>
                {
                    b.HasOne("DBLayer.Models.Good", "Good")
                        .WithMany("Orders")
                        .HasForeignKey("GoodId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Order_GoodId_FK");

                    b.HasOne("DBLayer.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Order_User_FK");

                    b.Navigation("Good");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DBLayer.Models.Good", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("DBLayer.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
