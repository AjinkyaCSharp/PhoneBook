using Microsoft.EntityFrameworkCore;
using PhoneBook.Database.Model;

namespace PhoneBook.Database
{
    public class PhoneBookDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Environment.CurrentDirectory + "\\Database\\PhoneBook.mdf";
            optionsBuilder.UseSqlServer($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""{dbPath}"";Integrated Security=True;Connect Timeout=30");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var contactModelBuilder = modelBuilder.Entity<Contact>();
            contactModelBuilder.ToTable("Contact");
            contactModelBuilder.HasKey(x => x.Id);
            contactModelBuilder.Property(x => x.Id).ValueGeneratedNever();
            contactModelBuilder.Property(x => x.CreatedTime).HasDefaultValueSql("GetUtcDate()");
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
