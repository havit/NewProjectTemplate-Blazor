using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.Contracts.Absences;

[ApiContract]
public interface IAbsenceFacade
{
	Task CreateAbsenceAsync(CancellationToken cancellationToken = default);
}
