using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DDDiplom.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public UserProfile Profile { get; set; }
        public enum Roles
        {
            Admin = 1,
            User = 2

        }
    }
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int Experience { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WorkPlaceId { get; set; }
        public WorkPlace WorkPlace { get; set; }
       

    }
    public class Role
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<User> Users { get; set; }
            public Role()
            {
                Users = new List<User>();
            }
        }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
    public class Product
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }

    }

    public class Order
    {
        public int Id { get; set; }
        public string IsPaid { get; set; }
        public double Summary { get; set; }
        public DateTime OrderTime { get; set; }
        public int WorkPlaceId { get; set; }
        public WorkPlace WorkPlace { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public List<Product> OrderProducts { get; set; }
        public Order()
        {
            OrderProducts = new List<Product>();
        }
    }
    public class WorkPlace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserProfile> UserProfiles { get; set; }
        public List<Order> Orders { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
    }
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public WorkPlace WorkPlace { get; set; }
    }
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Secondname { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
    }


    public class DDDiplomContext : DbContext
    {
        public DDDiplomContext()
        {
        }

        public DDDiplomContext(DbContextOptions<DDDiplomContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<WorkPlace> WorkPlaces { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-IIPACAR\\SQLEXPRESS; Database = DDDiplomContext; Trusted_Connection = True;");
        }
    }
}

