﻿using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plannr.DAL
{
    public class EnseignantsRepository : IEnseignantsRepository
    {

        private PlannrContext context;

        public EnseignantsRepository(PlannrContext context)
        {
            this.context = context;
        }

        public Enseignant Get(int id) {
            return this.context.Enseignants.Find(id);
        }


        public void Insert(Models.Enseignant e)
        {
            this.context.Enseignants.Add(e);
        }

        public void Delete(int id)
        {
            var e = this.context.Enseignants.Find(id);
            this.context.Enseignants.Remove(e);
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
    }

}