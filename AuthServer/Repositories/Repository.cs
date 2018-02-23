using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AuthServer.Entities;

namespace AuthServer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual T Get(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Single(match);
        }

        public virtual void Add(T t)
        {
            _context.Set<T>().Add(t);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
