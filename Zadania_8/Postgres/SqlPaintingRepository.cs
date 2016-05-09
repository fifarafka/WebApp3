﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zadania_8.Interfaces;
using Zadania_8.Models;
using Zadania_8.DAL;

namespace Zadania_8.Postgres
{
    public class SqlPaintingRepository : IPaintingRepository
    {
        private MuseumContext db = new MuseumContext();

        public int Add(Painting painting)
        {
            db.Paintings.Add(painting);
            db.SaveChanges();

            return painting.Id;
        }

        public bool Delete(int id)
        {
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return false;
            }
            db.Paintings.Remove(painting);
            db.SaveChanges();
            return true;
        }

        public Painting Get(int id)
        {
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return null;
            }
            return painting;
        }

        public List<Painting> GetAll()
        {
            List<Painting> paintings = db.Paintings.ToList();
            if (!paintings.Any())
                return null;
            return paintings;
        }

        public Painting Update(Painting painting)
        {
            Painting p = db.Paintings.Find(painting.Id);
            if (p == null)
                return null;
            p.Title = painting.Title;
            p.Year = painting.Year;
            db.SaveChanges();
            return painting;
        }
    }
}