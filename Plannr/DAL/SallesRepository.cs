﻿using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plannr.DAL
{
    public class SallesRepository : ISallesRepository
    {

        private PlannrContext context;

        public SallesRepository(PlannrContext context)
        {
            this.context = context;
        }

        public Salle Get(int id)
        {
            return this.context.Salles.Find(id);
        }

        public IEnumerable<Salle> GetSallesCriteres(int capacite, bool projo)
        {
  
          return this.context.Salles.Where(x => x.Capacite >= capacite && x.AProjecteur == projo).ToList();
       
        }

        public void Insert(Models.Salle e)
        {
            this.context.Salles.Add(e);
        }

        public void Delete(int id)
        {
            var e = this.context.Salles.Find(id);
            this.context.Salles.Remove(e);
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

        public IEnumerable<Salle> GetList() {

         return this.context.Salles.ToList();
        }

        public void Entry(Salle e) {
            this.context.Entry(e).State = System.Data.EntityState.Modified;
        }

    }

}