using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;

namespace IChat.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private db_ichatEntities dc = null;
        private DbSet<T> Dbset = null;

        public Repository()
        {
            dc = new db_ichatEntities();
            Dbset = dc.Set<T>();
        }

        public void Create(T _entity)
        {
            Dbset.Add(_entity);
            dc.SaveChanges();
        }

        public void Delete(T _entity)
        {
            Dbset.Remove(_entity);
            dc.SaveChanges();
        }

        public void Update(T _entiy)
        {
            dc.Entry(_entiy).State = EntityState.Modified;
            dc.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return Dbset.ToList();
        }

        public T GetById(int id)
        {
            return Dbset.Find(id);//用find方法一定要傳入pk
        }
    }
}