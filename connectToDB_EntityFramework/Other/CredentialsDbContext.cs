using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace connectToBD_EntityFramework.Other
{
    //DbContext e o clasa generica din .Net pe care o definim mai jos + apoi o vom folosi sa citim datele din DB cu ajutorul pachetului Pomelo.EntityFrameworkCore.MySql
    class CredentialsDbContext : DbContext
    {
        //definim variabila credentials de tip DbSet (DbSet e o clasa generica pt o colectii de date) care va mapa tabelul credential din baza de date tema la clasa curenta
        //de ce avem nevoie de colectie de date: pt ca in DB avem mai multe linii de tip: id, username, password
        public DbSet<Other.DataModels.EFModels.Credential> credential { get; set; }

        //definesc variabila connectionString cu care urmeaza sa ma conectez la baza de date
        private string connectionString;




        // constructor este mostenit din clasa parinte
        // constructorul are parametrul options
        // options este de tipul clasei generice DbContextOptions - mai multe detalii in ultima ametoda din clasa prezenta
        public CredentialsDbContext(DbContextOptions<CredentialsDbContext> options) : base(options)
        {

        }



        // al 2-lea constructor are ca parametru variabila connectionString 
        public CredentialsDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }




        //metoda OnConfiguring va face override la metoda OnConfiguring din DbContext
        //metoda va face conectarea la MySql - cum? folosind connectionString din fisierul appsettings.json 
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            //indic 2 parametrii pt conectare la baza de date: connectionString si clasa ServerVersion
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }


    }
}
