using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsBook.Models
{
    public class Email : BaseDomain
    {
		public int PersonId { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public string EmailAddress { get; set; }

		public int EmailTypeId { get; set; }
		public  EmailType Type { get; set; }
    }
}
