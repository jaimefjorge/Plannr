using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plannr.DAL
{
    interface IBatimentsRepository : IDisposable
    {
        Batiment Get(int id);
        void Insert(Batiment e);
        void Delete(int id);
        void Save();
    }
}
