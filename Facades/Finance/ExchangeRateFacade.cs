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
	public class ExchangeRateFacade : IExchangeRateFacade
	{
		private readonly IExchangeRateRepository exchangeRateRepository;
		private readonly IExchangeRateMapper exchangeRateMapper;
		private readonly IApplicationAuthenticationService applicationAuthenticationService;
		private readonly IUnitOfWork unitOfWork;

		public ExchangeRateFacade(
			IExchangeRateRepository exchangeRateRepository,
			IExchangeRateMapper exchangeRateMapper,
			IApplicationAuthenticationService applicationAuthenticationService, // TODO XyAuthorizationService
			IUnitOfWork unitOfWork)
		{
			this.exchangeRateRepository = exchangeRateRepository;
			this.exchangeRateMapper = exchangeRateMapper;
			this.applicationAuthenticationService = applicationAuthenticationService;
			this.unitOfWork = unitOfWork;
		}

		public async Task<Dto<List<ExchangeRateDto>>> GetExchangeRatesAsync(CancellationToken cancellationToken = default)
		{
			var data = await exchangeRateRepository.GetAllAsync(cancellationToken);
			return Dto.FromValue(data.Select(ba => exchangeRateMapper.MapToExchangeRateDto(ba)).ToList());
		}

		public async Task DeleteExchangeRateAsync(Dto<int> exchangeRateId, CancellationToken cancellationToken = default)
		{
			CheckAuthorization();

			var exchangeRate = await exchangeRateRepository.GetObjectAsync(exchangeRateId.Value, cancellationToken);
			unitOfWork.AddForDelete(exchangeRate);
			await unitOfWork.CommitAsync(cancellationToken);
		}

		public async Task<Dto<int>> CreateExchangeRateAsync(ExchangeRateDto exchangeRateDto, CancellationToken cancellationToken = default)
		{
			Contract.Requires<ArgumentNullException>(exchangeRateDto is not null);

			CheckAuthorization();

			var exchangeRate = new ExchangeRate();
			exchangeRateMapper.MapFromExchangeRateDto(exchangeRateDto, exchangeRate);

			unitOfWork.AddForInsert(exchangeRate);
			await unitOfWork.CommitAsync(cancellationToken);

			return Dto.FromValue(exchangeRate.Id);
		}

		public async Task UpdateExchangeRateAsync(ExchangeRateDto exchangeRateDto, CancellationToken cancellationToken = default)
		{
			Contract.Requires<ArgumentNullException>(exchangeRateDto is not null);

			CheckAuthorization();

			var exchangeRate = await exchangeRateRepository.GetObjectAsync(exchangeRateDto.Id, cancellationToken);

			exchangeRateMapper.MapFromExchangeRateDto(exchangeRateDto, exchangeRate);

			unitOfWork.AddForUpdate(exchangeRate);
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
