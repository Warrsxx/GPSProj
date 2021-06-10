using System;
using GPS.Domain;
using GPS.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace GPS.Repository
{
    public class GpsDBContext : DbContext
    {
        public GpsDBContext(DbContextOptions<GpsDBContext> options) : base(options)
        {

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-S4PRAAC\MSSQLLOCAL;Initial Catalog=GPSPrjDB;User ID=WebApp;Password=123456;Integrated Security=SSPI;Trusted_Connection=false;");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<atividade_principal> atividade_principais { get; set; }
        public DbSet<atividades_secundarias> atividades_secundarias { get; set; }
        public DbSet<billing> billing { get; set; }
        public DbSet<qsa> qsas { get; set; }
    }
}
 