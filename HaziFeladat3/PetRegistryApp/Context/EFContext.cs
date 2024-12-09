using Microsoft.EntityFrameworkCore;
using PetRegistryApp.Models;

namespace PetRegistryApp.Context
{
	public class EFContext : DbContext
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Pet> Pets { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=.\\DB\\pets.db");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Pet>() // entitás a külső kulccsal
			.HasOne(p => p.ReferencedCategory) // hivatkozzuk a másik entitást
			.WithMany() // jelezzük, hogy ez 1:N kapcsolat
			.HasForeignKey(p => p.CategoryID) // megadjuk a külső kulcsot
			.OnDelete(DeleteBehavior.Restrict);
		}

	}
}
