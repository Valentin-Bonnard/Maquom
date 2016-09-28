using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Maquom.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Maquom.Data
{
    public class MaquomContext : DbContext
    {
        #region l'ensembles des entités
    public DbSet<Company> CompanySet { get; set; }
    public DbSet<Contact> ContactSet { get; set; }
    public DbSet<Client> ClientSet { get; set; }
    public DbSet<Application> ApplicationSet { get; set; }
    public DbSet<Error> ErrorSet { get; set; }

    #endregion

    public MaquomContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relation.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // Configurations

        #region Company

        modelBuilder.Entity<Company>()
            .ToTable("Company");

        modelBuilder.Entity<Company>()
            .Property(c => c.ID)
            .IsRequired();
        #endregion

        #region Contact

        modelBuilder.Entity<Contact>()
            .ToTable("Contact");

        modelBuilder.Entity<Contact>()
            .Property(c => c.Name)
            .IsRequired();

        modelBuilder.Entity<Contact>()
           .Property(c => c.FirstName)
           .IsRequired();

        modelBuilder.Entity<Contact>()
           .Property(c => c.Telephone)
           .IsRequired();

        modelBuilder.Entity<Contact>()
            .Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Contact>()
            .Property(c => c.Project)
            .HasMaxLength(400)
            .IsRequired();

        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Company)
            .WithMany(e => e.Contacts)
            .HasForeignKey(c => c.CompanyId);
        #endregion

        #region Client

        modelBuilder.Entity<Client>()
            .ToTable("Client");

        modelBuilder.Entity<Client>()
            .Property(c => c.Name)
            .IsRequired();

        modelBuilder.Entity<Client>()
            .HasOne(c => c.Company)
            .WithMany(e => e.Clients)
            .HasForeignKey(c => c.CompanyId);
        #endregion

        #region Application

        modelBuilder.Entity<Application>()
            .ToTable("Applications");

        modelBuilder.Entity<Application>()
            .Property(a => a.Description)
            .HasMaxLength(400)
            .IsRequired();

        modelBuilder.Entity<Application>()
            .HasOne(a => a.Client)
            .WithMany(c => c.Applications)
            .HasForeignKey(a => a.ClientId);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.Company)
            .WithMany(c => c.Applications)
            .HasForeignKey(a => a.CompanµId);

        #endregion

    }
}
}