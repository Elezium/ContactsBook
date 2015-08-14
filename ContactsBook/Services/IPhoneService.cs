using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Models;

namespace ContactsBook.Services
{
	public interface IPhoneService
	{
		void AddOrUpdate(Phone phone);  

        IEnumerable<Phone> GetAll();

		Phone GetOneById(int id);
  
        IEnumerable<Phone> GetByPersonId(int id);

		void Delete(int it);

	}
}
