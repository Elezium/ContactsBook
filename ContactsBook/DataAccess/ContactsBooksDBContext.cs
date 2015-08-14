using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ContactsBook.Models;

namespace ContactsBook.DataAccess
{
	public class ContactsBookDBContext : DbContext
	{
		public ContactsBookDBContext()
			: base("name=DefaultConnection")
		{ }

		public DbSet<Person> People { get; set; }
		public DbSet<Phone> Phones { get; set; }
		public DbSet<Email> Emails { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<EmailType> EmailTypes { get; set; }
		public DbSet<PhoneType> PhoneTypes { get; set; }
		public DbSet<AddressType> AddressTypes { get; set; }

	}
}