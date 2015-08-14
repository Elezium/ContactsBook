using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Models;

namespace ContactsBook.Services
{
	public interface IAddressService
	{
		void AddOrUpdate(Address address);  

        IEnumerable<Address> GetAll();

		Address GetOneById(int id);
  
        IEnumerable<Address> GetByPersonId(int id);

		void Delete(int it);

	}
}
