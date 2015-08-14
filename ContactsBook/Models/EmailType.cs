using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactsBook.Models
{
	public class EmailType : BaseDomain
	{
		[Display(Name = "Email Type")]
		public string Label { get; set; }		
	}
}