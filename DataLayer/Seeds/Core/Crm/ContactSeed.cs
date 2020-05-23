using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.Model.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.DataLayer.Seeds.Core.Crm
{
	public class ContactSeed : DataSeed<CoreProfile>
	{
		public override void SeedData()
		{
			var contacts = new[]
			{
				new Contact()
				{
					Id = (int)Contact.Entry.Self,
					Name = "<SELF CONTACT>",
				}
			};

			Seed(For(contacts).PairBy(c => c.Id).WithoutUpdate());
		}
	}
}
