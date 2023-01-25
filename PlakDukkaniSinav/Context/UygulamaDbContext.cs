using Microsoft.EntityFrameworkCore;
using PlakDukkaniSinav.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlakDukkaniSinav.Context
{
    public class UygulamaDbContext:DbContext
    {
        public DbSet<Admin> Adminler { get; set; }
        public DbSet<Album> Albumler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=PlakDukkaniDb;Integrated Security=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().HasData(
                new Album() { Id=1,AlbumAdi="Back In Black",Sanatci="AC/DC",CikisTarihi=1980,Fiyat=600,IndirimOrani=0,SatisDevamMi=true},
                new Album() { Id=2,AlbumAdi="Hurt",Sanatci="Johhny Cash",CikisTarihi=2002,Fiyat=400,IndirimOrani=0.12m,SatisDevamMi=false},
                new Album() { Id=3,AlbumAdi="Hotel California",Sanatci="Eagles",CikisTarihi=1976,Fiyat=599,IndirimOrani=0.1m,SatisDevamMi=true},
                new Album() { Id=4,AlbumAdi="Zeytin Yağlı Yaprak Dolması",Sanatci="Grup Vitamin",CikisTarihi=1995,Fiyat=200,IndirimOrani=0,SatisDevamMi=false},
                new Album() { Id=5,AlbumAdi="Neden Saçların Beyazlamış Arkadaş",Sanatci="Adnan Şenses",CikisTarihi=2005,Fiyat=250,IndirimOrani=0.2m,SatisDevamMi=false},
                new Album() { Id=6,AlbumAdi="Bombabomba.com",Sanatci="İsmail YK",CikisTarihi=2006,Fiyat=100,IndirimOrani=0.25m,SatisDevamMi=true}
                );
        }
    }
}
