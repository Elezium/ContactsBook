using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsBook.Models
{
	public class Person : BaseDomain
	{
		[Required]
		[Display(Name ="First Name")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		
		public string PictureUrl { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "Birth Day")]
		public DateTime BirthDay { get; set; }

		public  List<Address> Adresses { get; set; }
		public  List<Phone> Phones { get; set; }
		public  List<Email> Emails { get; set; }

    }
}
