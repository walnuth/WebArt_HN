using System.Linq;
using Microsoft.EntityFrameworkCore;
using Signo.Business.Models;

namespace Signo.Data.Context
{
   public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Integrante> Integrantes { get; set; }
        //mapear de forma correta suando fluent API, sem poluir com data notations



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);
            //chamada para criar tabelas como descrito nos MAPPINGS



            foreach (var relationship in modelBuilder
                    .Model.GetEntityTypes()
                    .SelectMany(e =>
                        e.GetForeignKeys()))
                // pega todas relações dentro do MODELBUILDER que tem chaves

                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            // e coloca a relação delas para nulas, isto é, não apagam, os filhos quando os pais forem apagados



            //foreach (var property in modelBuilder.Model.GetEntityTypes()
            //    .SelectMany(e => e.GetProperties()
            //        .Where(p => p.ClrType == typeof(string))))
            //    property.SetColumnType("varchar(200)");
            // se esquecer de mapear tabela pega esta por padrão



            base.OnModelCreating(modelBuilder);
        }
    }
}
