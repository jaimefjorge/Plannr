using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plannr.DAL
{
    interface ISallesRepository : IDisposable
    {
        Salle Get(int id);
        void Insert(Salle e);
        void Delete(int id);
        IEnumerable<Salle> GetList();
        void Entry(Salle e);
        void Save();
    }
}
