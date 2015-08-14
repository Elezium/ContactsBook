using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsBook.Models
{
	public class Phone : BaseDomain
	{
		public int PersonId { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		public int PhoneTypeId { get; set; }
		public  PhoneType Type { get; set; }
    }
}
