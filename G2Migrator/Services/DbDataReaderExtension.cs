using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.G2Migrator.Services
{
	public static class DbDataReaderExtension
	{
		public static T GetValue<T>(this DbDataReader reader, string columnName)
		{
			if (reader[columnName] == DBNull.Value)
			{
				return default(T);
			}
			return (T)reader[columnName];
		}

	}
}
