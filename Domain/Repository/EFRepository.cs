﻿using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace Domain.Repository
{
    public class EFRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        private EFDBContext db = new EFDBContext();

        public T Alterar(T item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return item;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public T Excluir(int id)
        {
            var item = db.Set<T>().Find(id);

            if (item != null)
            {
                try
                {
                    db.Set<T>().Remove(item);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }

            return item;
        }

        public T Find(int id)
        {
            throw new NotImplementedException();
        }

        public T Incluir(T item)
        {
            try
            {
                db.Set<T>().Add(item);
                db.SaveChanges();
                return item;
            }
            catch (DbEntityValidationException dbEx)
            {
                var se = new System.Text.StringBuilder();
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        se.Append(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
                throw new InvalidOperationException(se.ToString());
            }
        }

        public IQueryable<T> Listar()
        {
            return db.Set<T>();
        }
    }
}
