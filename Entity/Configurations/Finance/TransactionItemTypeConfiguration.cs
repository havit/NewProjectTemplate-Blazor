using Havit.GoranG3.Model.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Entity.Configurations.Finance
{
	public class TransactionItemTypeConfiguration : IEntityTypeConfiguration<TransactionItemType>
	{
		public void Configure(EntityTypeBuilder<TransactionItemType> builder)
		{
			builder.Property(it => it.Id).ValueGeneratedNever();
		}
	}
}
