using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.DataAccess;
using ContactsBook.Models;

namespace ContactsBook.Services
{
	public class GenericService : IGenericService
	{

		protected readonly IUnitOfWork _unitOfWork;
		protected readonly IRepository _repository;

		public GenericService(IUnitOfWork unitOfWork, IRepository repository)
		{
			_unitOfWork = unitOfWork;
			_repository = repository;
		}

		public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : BaseDomain
		{
			var model = _repository.GetAll<TEntity>();
			return model;
		}

		public TEntity GetById<TEntity>(int id) where TEntity : BaseDomain
		{
			var model = _repository.FindOne<TEntity>(criteria: i => i.Id == id);			
			return model;
		}



		public void AddOrUpdate<TEntity>(TEntity entity) where TEntity : BaseDomain
		{
			if (entity.Id == 0)
			{
				_repository.Add<TEntity>(entity);
				_unitOfWork.Commit();
			}
			else
			{
				_repository.Update<TEntity>(entity);
				_unitOfWork.Commit();
			}
		}

		public void Delete<TEntity>(int id) where TEntity : BaseDomain
		{
			var model = _repository.FindOne<TEntity>(criteria: i => i.Id == id);
			_repository.Delete<TEntity>(model);
			_unitOfWork.Commit();
		}

		public bool Exists<TEntity>(int id) where TEntity : BaseDomain
		{

			var t = _repository.FindOne<TEntity>(criteria: i => i.Id == id);					
			if (t == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}


	}
}
