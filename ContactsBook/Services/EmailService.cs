using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactsBook.Models;
using ContactsBook.DataAccess;

namespace ContactsBook.Services
{
	public class EmailService : IEmailService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository _repository;

		public EmailService(IUnitOfWork unitOfWork, IRepository repository)
		{
			_unitOfWork = unitOfWork;
			_repository = repository;
		}

		public void AddOrUpdate(Email email)
		{			
			if (email.Id == 0) {
				_repository.Add<Email>(email);
				_unitOfWork.Commit();
			}
			else
			{
				_repository.Update<Email>(email);
				_unitOfWork.Commit();
			}
		}

		public IEnumerable<Email> GetAll()
		{
			var email = _repository.GetAll<Email>();
			return email;
		}

		public Email GetOneById(int id)
		{
			var email = _repository.FindOne<Email>(criteria: i => i.Id == id);
			return email;
		}

		public IEnumerable<Email> GetByPersonId(int id)
		{
			var email = _repository.FindInclude<Email> ("Type",criteria: i => i.PersonId == id);
			return email;
		}

		public void Delete(int id)
		{
			var email = _repository.FindOne<Email>(criteria: i => i.Id == id);
			_repository.Delete<Email>(email);
			_unitOfWork.Commit();
		}


	}
}