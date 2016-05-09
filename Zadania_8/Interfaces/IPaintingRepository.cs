using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadania_8.Models;

namespace Zadania_8.Interfaces
{
    public interface IPaintingRepository
    {
        int Add(Painting painting);
        bool Delete(int id);
        Painting Get(int id);
        List<Painting> GetAll();
        Painting Update(Painting painting);
    }
}
