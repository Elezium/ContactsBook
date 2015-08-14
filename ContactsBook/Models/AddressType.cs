using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactsBook.Models
{
	public class AddressType : BaseDomain
	{
		[Display(Name = "Address Type")]
		public string Label { get; set; }
	}
}