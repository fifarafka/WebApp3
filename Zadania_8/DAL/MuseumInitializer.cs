using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zadania_8.Models;

namespace Zadania_8.DAL
{
    public class MuseumInitializer : System.Data.Entity.DropCreateDatabaseAlways<MuseumContext>
    {
        protected override void Seed(MuseumContext context)
        {
            var paintings = new List<Painting>
            {
                new Painting() { Id = 1, Title = "Portret Marii Teresy Walter", Year = 1937 },
                new Painting() { Id = 2, Title = "Ship with butterfly sails", Year = 1956 },
                new Painting() { Id =3, Title = "Poznań", Year = 2004 }
            };
            context.Paintings.AddRange(paintings);
            context.SaveChanges();

            var artists = new List<Artist>
            {
                new Artist() { Id = 1, ArtistName = "Pablo", ArtistSurname = "Picasso" },
                new Artist() { Id = 2, ArtistName = "Salvador", ArtistSurname = "Dali" },
                new Artist() { Id = 3, ArtistName ="Edward", ArtistSurname="Dwurnik" }

            };
            context.Artists.AddRange(artists);
            context.SaveChanges();
        }
    }
}