using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Diagnostics.Contracts;
using Havit.GoranG3.Contracts;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.DataLayer.Repositories.Finance;
using Havit.GoranG3.Facades.Infrastructure.Security.Authentication;
using Havit.GoranG3.Model.Finance;
using Havit.GoranG3.Services.Finance;

namespace Havit.GoranG3.Facades.Finance
{
	public class CurrencyFacade : ICurrencyFacade
	{
		private readonly ICurrencyRepository currencyRepository;
		private readonly ICurrencyMapper currencyMapper;
		private readonly IApplicationAuthenticationService applicationAuthenticationService;
		private readonly IUnitOfWork unitOfWork;

		public CurrencyFacade(
			ICurrencyRepository currencyRepository,
			ICurrencyMapper currencyMapper,
			IApplicationAuthenticationService applicationAuthenticationService, // TODO XyAuthorizationService
			IUnitOfWork unitOfWork)
		{
			this.currencyRepository = currencyRepository;
			this.currencyMapper = currencyMapper;
			this.applicationAuthenticationService = applicationAuthenticationService;
			this.unitOfWork = unitOfWork;
		}

		public async Task<Dto<int>> CreateCurrencyAsync(CurrencyDto currencyDto, CancellationToken cancellationToken = default)
		{
			Contract.Requires<ArgumentNullException>(currencyDto is not null);

			CheckAuthorization();

			var currency = new Currency();
			currencyMapper.MapFromCurrencyDto(currencyDto, currency);

			unitOfWork.AddForInsert(currency);
			await unitOfWork.CommitAsync(cancellationToken);

			return Dto.FromValue(currency.Id);
		}

		public async Task DeleteCurrencyAsync(Dto<int> currencyId, CancellationToken cancellationToken = default)
		{
			CheckAuthorization();

			var currency = await currencyRepository.GetObjectAsync(currencyId.Value);
			unitOfWork.AddForDelete(currency);

			await unitOfWork.CommitAsync(cancellationToken);
		}

		public async Task<Dto<List<CurrencyDto>>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var data = await currencyRepository.GetAllAsync(cancellationToken);
			return Dto.FromValue(data.Select(currencyMapper.MapToCurrencyDto).ToList());
		}

		public async Task UpdateCurrencyAsync(CurrencyDto currencyDto, CancellationToken cancellationToken = default)
		{
			Contract.Requires<ArgumentNullException>(currencyDto is not null);

			CheckAuthorization();

			var currency = await currencyRepository.GetObjectAsync(currencyDto.Id, cancellationToken);
			currencyMapper.MapFromCurrencyDto(currencyDto, currency);

			unitOfWork.AddForUpdate(currency);
			await unitOfWork.CommitAsync(cancellationToken);
		}

		private void CheckAuthorization()
		{
			var user = applicationAuthenticationService.GetCurrentUser();

			if ((user is null) || !user.IsInRole(roleEntry: Model.Security.Role.Entry.UserSettingsAdministrator))
			{
				throw new SecurityException("Access denied.");
			}
		}
	}
}
