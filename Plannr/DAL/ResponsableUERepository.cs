using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Plannr.DAL
{
    public class ResponsableUERepository : IResponsableUe
    {
         private PlannrContext context;

        public ResponsableUERepository(PlannrContext context)
        {
            this.context = context;
        }

        public ResponsableUE Get(int id) {
                
    
            return this.context.ResponsablesUE.Find(id);
        }


        public void Insert(Models.ResponsableUE e)
        {
            this.context.ResponsablesUE.Add(e);
        }

        public void Edit(ResponsableUE e)
        {
           this.context.Entry(e).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var e = this.context.ResponsablesUE.Find(id);
            this.context.ResponsablesUE.Remove(e);
        }

        public void Entry(ResponsableUE e)
        {
            this.context.Entry(e).State = System.Data.EntityState.Modified;
        }

        public IEnumerable<ResponsableUE> GetAll()
        {
            return this.context.ResponsablesUE.ToList();
        }
        public IEnumerable<ResponsableUE> GetList()
        {

            return this.context.ResponsablesUE.ToList();
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

        public int Count()
        {
            return context.ResponsablesUE.Count();
        }
    
    }
}