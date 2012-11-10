using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plannr.DAL
{
    public class UeRepository : IUeRepository
    {

        private PlannrContext context;

        public UeRepository(PlannrContext context)
        {
            this.context = context;
        }

        public Ue Get(int id)
        {
            return this.context.Ues.Find(id);
        }

        public void Insert(Models.Ue u)
        {
            this.context.Ues.Add(u);
        }

        public void Delete(int id)
        {
            var u = this.context.Ues.Find(id);
            this.context.Ues.Remove(u);
        }

        public void Save()
        {
            this.context.SaveChanges();
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

        public IEnumerable<Ue> GetList()
        {

            return this.context.Ues.ToList();
        }

        public void Entry(Ue u)
        {
            this.context.Entry(u).State = System.Data.EntityState.Modified;
        }

    }

}