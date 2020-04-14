using System.Runtime.Serialization;

namespace Havit.GoranG3.Contracts.GrpcTests
{
	[DataContract]
	public class DoSomethingResult
	{
		[DataMember(Order = 1)]
		public string Message { get; set; }

		[DataMember(Order = 2)]
		public int Value { get; set; }
	}
}