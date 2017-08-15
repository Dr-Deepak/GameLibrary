namespace Library.Migrations.DataContext
{
    using Library.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Library.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations.DataContext";
            ContextKey = "Library.Models.DataContext";
        }

        protected override void Seed(Library.Models.DataContext context)
        {
            List<string> genres = new List<string>();
            genres.Add("Multiplayer");
            genres.Add("First Shooter");
            genres.Add("Guns");
            genres.Add("Cars");
            foreach (var genre in genres)
            {
                Genre newgenre = new Genre();                
                newgenre.Name = genre;

                /*TO CHECK DUPLICATE DATA*/
                Genre checkGenre = context.Genres.SingleOrDefault(x => x.Name == newgenre.Name);
                if (checkGenre == null)
                {
                    context.Genres.Add(newgenre);
                }
            }
            context.SaveChanges();
        }    
    }
}
