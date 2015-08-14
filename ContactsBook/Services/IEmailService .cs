using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Models;

namespace ContactsBook.Services
{
	public interface IEmailService
	{
		void AddOrUpdate(Email email);  

        IEnumerable<Email> GetAll();

		Email GetOneById(int id);
  
        IEnumerable<Email> GetByPersonId(int id);

		void Delete(int it);

	}
}
