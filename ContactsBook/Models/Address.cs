using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsBook.Models
{
    public class Address : BaseDomain 
    {
		public int PersonId { get; set; }

		[Required]		
		[Display(Name ="Street Address")]
		public string StreetAddress { get; set; }

		[Required]
		public string City { get; set; }

		[Required]
		public string State { get; set; }

		[Required]
		public string Country { get; set; }

		[Required]
		public string ZipCode { get; set; }

		
		public int AddressTypeId { get; set; }
		
		public  AddressType Type { get; set; }
	}
}
