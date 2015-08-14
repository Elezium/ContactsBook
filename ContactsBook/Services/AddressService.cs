using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactsBook.Models;
using ContactsBook.DataAccess;

namespace ContactsBook.Services
{
	public class AddressService : IAddressService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository _repository;

		public AddressService(IUnitOfWork unitOfWork, IRepository repository)
		{
			_unitOfWork = unitOfWork;
			_repository = repository;
		}

		public void AddOrUpdate(Address address)
		{			
			if (address.Id == 0) {
				_repository.Add<Address>(address);
				_unitOfWork.Commit();
			}
			else
			{				
				_repository.Update<Address>(address);				
				_unitOfWork.Commit();
			}
		}

		public IEnumerable<Address> GetAll()
		{
			var address = _repository.GetAll<Address>();
			return address;
		}

		public Address GetOneById(int id)
		{
			var address = _repository.FindOne<Address>(criteria: i => i.Id == id);
			return address;
		}

		public IEnumerable<Address> GetByPersonId(int id)
		{
			var addresses = _repository.FindInclude<Address> ("Type",criteria: i => i.PersonId == id);
			return addresses;
		}

		public void Delete(int id)
		{
			var address = _repository.FindOne<Address>(criteria: i => i.Id == id);
			_repository.Delete<Address>(address);
			_unitOfWork.Commit();
		}


	}
}