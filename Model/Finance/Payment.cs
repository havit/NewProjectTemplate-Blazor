using Havit.Diagnostics.Contracts;
using Havit.GoranG3.Model.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Finance
{
	/// <summary>
	/// G2: Uhrada
	/// </summary>
	public class Payment : IValidatableObject
	{
		public int Id { get; set; }

		public Transaction Transaction { get; set; }
		public int TransactionId { get; set; }

		/// <summary>
		/// G2: Datum
		/// </summary>
		public DateTime PaymentDate
		{
			get => _paymentDate;
			set
			{
				bool changed = (_paymentDate != value);
				_paymentDate = value;
				if ((this.Transaction != null) && changed)
				{
					this.Transaction.CalculatePayment();
				}
			}
		}
		private DateTime _paymentDate;

		/// <summary>
		/// G2: Castka
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal Amount
		{
			get => _amount;
			set
			{
				bool changed = (_amount != value);
				_amount = value;
				if ((this.Transaction != null) && changed)
				{
					this.Transaction.CalculatePayment();
				}
			}
		}
		private decimal _amount;
		private decimal? _amountInForeignCurrency;

		public Currency Currency { get; set; }
		public int? CurrencyId { get; set; }

		/// <summary>
		/// G2: CastkaVCiziMene
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal? AmountInForeignCurrency
		{
			get => _amountInForeignCurrency;
			set
			{
				bool changed = (_amountInForeignCurrency != value);
				_amountInForeignCurrency = value;
				if ((this.Transaction != null) && changed)
				{
					this.Transaction.CalculatePayment();
				}
			}
		}

		/// <summary>
		/// G2: Popis
		/// </summary>
		[MaxLength(50)]
		public string Description { get; set; }

		public DateTime Created { get; set; }

		public User CreatedBy { get; set; }
		public int? CreatedById { get; set; }

		public DateTime? Deleted { get; set; }

		public int? MigrationId { get; set; }

		public bool IsForeignCurrency => this.Currency != null;

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			Contract.Assert<InvalidOperationException>(this.Transaction != null, nameof(this.Transaction));

			if (this.IsForeignCurrency && (this.AmountInForeignCurrency == null))
			{
				yield return new ValidationResult("Payment in foreign currency has to have amount in foreign currency.");
			}

			if ((this.Transaction.Currency != this.Currency))
			{
				yield return new ValidationResult("Payment has to have same currency as corresponding transaction.");
			}
		}
	}
}
