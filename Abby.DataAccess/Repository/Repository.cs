using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Abby.DataAccess.Repository.IRepository;
using AbbyWeb.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Abby.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class

	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;

		public Repository(ApplicationDbContext dbContext)
		{
			_db = dbContext;
			//Food,Category
			//_db.MenuItem.Include(e => e.Food).Include(e => e.Category);
			//_db.MenuItem.OrderBy(x => x.Name);
			this.dbSet = dbContext.Set<T>();
		}

		public void Add(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			dbSet.Add(entity);
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, 
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null,
			string ? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null)
				query = query.Where(filter);
			if (includeProperties != null)
			{
				//abc,,xyz -> abc xyz
				foreach (var includeProperty in includeProperties.Split(
					         new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}
			if(orderby != null)
			{
				return orderby(query).ToList();
			}
			return query.ToList();
		}

		public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null)
				query = query.Where(filter);
			if (includeProperties != null)
			{
				//abc,,xyz -> abc xyz
				foreach (var includeProperty in includeProperties.Split(
							 new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}
			return query.FirstOrDefault();
		}
	}
}