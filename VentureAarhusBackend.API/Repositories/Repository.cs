using System;
using System.Linq;
using System.Linq.Expressions;
using VentureAarhusBackend.API.Entities;

namespace VentureAarhusBackend.API.Repositories
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

        public virtual void Delete(T t)
        {
            _context.Remove(t);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
