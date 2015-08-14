using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Models;

namespace ContactsBook.Services
{
	public interface IGenericService
	{
		IEnumerable<TEntity> GetAll<TEntity>() where TEntity : BaseDomain;

		TEntity GetById<TEntity>(int id) where TEntity : BaseDomain;


		void AddOrUpdate<TEntity>(TEntity entity)
			where TEntity : BaseDomain;


		void Delete<TEntity>(int id) where TEntity : BaseDomain;

		bool Exists<TEntity>(int id) where TEntity : BaseDomain;

	}
}
