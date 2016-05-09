using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zadania_8.Models;
using Zadania_8.Interfaces;
using LiteDB;

namespace Zadania_8.LiteDB
{
    public class PaintingRepository : IPaintingRepository
    {
        private readonly string _databaseConnection = DatabaseConnections.PaintingConnection;
        private readonly string _collectionName = "painting";

        public int Add(Painting painting)
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                var repository = db.GetCollection<Painting>(_collectionName);

                if (repository.FindById(painting.Id) != null)
                    repository.Update(painting);
                else
                    repository.Insert(painting);

                return painting.Id;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                var repository = db.GetCollection<Painting>(_collectionName);

                return repository.Delete(id);
            }
        }

        public Painting Get(int id)
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                var repository = db.GetCollection<Painting>(_collectionName);

                return repository.FindById(id);
            }
        }

        public List<Painting> GetAll()
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                return db.GetCollection<Painting>(_collectionName).FindAll().ToList();
            }
        }

        public Painting Update(Painting painting)
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                var repository = db.GetCollection<Painting>(_collectionName);

                if (repository.Update(painting))
                    return painting;
                else
                    return null;
            }
        }
    }
}