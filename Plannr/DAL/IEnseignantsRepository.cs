using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plannr.DAL
{
    interface IEnseignantsRepository : IDisposable
    {
        Enseignant Get(int id);
        void Insert(Enseignant e);
        void Delete(int id);
        void Save();
    }
}
