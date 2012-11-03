using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plannr.DAL
{
    public class BatimentsRepository : IBatimentsRepository
    {

        private PlannrContext context;

        public BatimentsRepository(PlannrContext context)
        {
            this.context = context;
        }

        public Batiment Get(int id)
        {
            return this.context.Batiments.Find(id);
        }


        public void Insert(Models.Batiment e)
        {
            this.context.Batiments.Add(e);
        }

        public void Delete(int id)
        {
            var e = this.context.Batiments.Find(id);
            this.context.Batiments.Remove(e);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        public IEnumerable<Batiment> GetAll()
        {
            return this.context.Batiments.AsEnumerable();
        }

        // DIspose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}