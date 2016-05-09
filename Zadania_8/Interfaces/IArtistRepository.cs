using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zadania_8.Models;

namespace Zadania_8.Interfaces
{
    public interface IArtistRepository
    {
        int Add(Artist artist);
        bool Delete(int id);
        Artist Get(int id);
        List<Artist> GetAll();
        Artist Update(Artist artist);
    }
}