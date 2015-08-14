using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactsBook.Models;
using ContactsBook.DataAccess;

namespace ContactsBook.Services
{
	public class PhoneService : IPhoneService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository _repository;

		public PhoneService(IUnitOfWork unitOfWork, IRepository repository)
		{
			_unitOfWork = unitOfWork;
			_repository = repository;
		}

		public void AddOrUpdate(Phone phone)
		{			
			if (phone.Id == 0) {
				_repository.Add<Phone>(phone);
				_unitOfWork.Commit();
			}
			else
			{
				_repository.Update<Phone>(phone);
				_unitOfWork.Commit();
			}
		}

		public IEnumerable<Phone> GetAll()
		{
			var phone = _repository.GetAll<Phone>();
			return phone;
		}

		public Phone GetOneById(int id)
		{
			var phone = _repository.FindOne<Phone>(criteria: i => i.Id == id);
			return phone;
		}

		public IEnumerable<Phone> GetByPersonId(int id)
		{
			var phone = _repository.FindInclude<Phone> ("Type",criteria: i => i.PersonId == id);
			return phone;
		}

		public void Delete(int id)
		{
			var phone = _repository.FindOne<Phone>(criteria: i => i.Id == id);
			_repository.Delete<Phone>(phone);
			_unitOfWork.Commit();
		}


	}
}