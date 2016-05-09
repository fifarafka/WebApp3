using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zadania_8.Interfaces;
using Zadania_8.Models;
using LiteDB;

namespace Zadania_8.LiteDB
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly string _databaseConnection = DatabaseConnections.ArtistConnection;
        private readonly string _collectionName = "artist";

        public int Add(Artist artist)
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                var repository = db.GetCollection<Artist>(_collectionName);

                if (repository.FindById(artist.Id) != null)
                    repository.Update(artist);
                else
                    repository.Insert(artist);

                return artist.Id;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                var repository = db.GetCollection<Artist>(_collectionName);
                return repository.Delete(id);
            }
        }

        public Artist Get(int id)
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                var repository = db.GetCollection<Artist>(_collectionName);
                return repository.FindById(id);
            }
        }

        public List<Artist> GetAll()
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                return db.GetCollection<Artist>(_collectionName).FindAll().ToList();
            }
        }

        public Artist Update(Artist artist)
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                var repository = db.GetCollection<Artist>(_collectionName);

                if (repository.Update(artist))
                    return artist;
                else
                    return null;
            }
        }
    }
}