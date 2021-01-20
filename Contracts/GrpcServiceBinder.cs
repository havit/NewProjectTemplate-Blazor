using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf.Grpc.Configuration;

namespace Havit.GoranG3.Contracts
{
	// TODO: Kam s ním? Přinesl též závislost Contracts na nuget balíčku proto-buf.Grpc.

	public class GrpcServiceBinder : ServiceBinder
	{
		// voláno jen pro zaregistrované služby
		public override bool IsServiceContract(Type contractType, out string name)
		{
			// name - zde použito bez namespace
			string resultName = (contractType.IsInterface && contractType.Name.StartsWith("I"))
				? contractType.Name.Substring(1)
				: contractType.Name;

			/*if (resultName.EndsWith("Facade"))
			{
				resultName = resultName.Substring(0, resultName.Length - "Facade".Length);
			}*/

			name = $"{contractType.Namespace}.{resultName}";
			return true;
		}
	}
}
