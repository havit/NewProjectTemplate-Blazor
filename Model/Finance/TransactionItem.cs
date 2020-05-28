using Havit.Diagnostics.Contracts;
using Havit.GoranG3.Model.Projects;
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
	/// G2: FakturaItem
	/// </summary>
	public class TransactionItem : IValidatableObject
	{
		public int Id { get; set; }

		public Transaction Transaction { get; set; }
		public int TransactionId { get; set; }

		/// <summary>
		/// G2: FakturaItem.TypFakturaItemId
		/// </summary>
		public TransactionItemType ItemType { get; set; }
		public int ItemTypeId { get; set; }

		/// <summary>
		/// G2: FakturaItem.Popis, PlatbaHistorie.Popis (2/2)
		/// </summary>
		[MaxLength]
		public string Description { get; set; }

		/// <summary>
		/// G2: FakturaItem.CastkaBezDph, PlatbaHistorie.Castka
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal AmountWithoutVat
		{
			get => _amountWithoutVat;
			set
			{
				bool changed = (_amountWithoutVat != value);
				_amountWithoutVat = value;
				if (changed)
				{
					RecalculateAmounts();
					if (this.Transaction != null)
					{
						this.Transaction.RecalculateTotals();
					}
				}
			}
		}
		private decimal _amountWithoutVat;

		/// <summary>
		/// G2: FakturaItem.CastkaVCiziMeneBezDph, PlatbaHistorie.CastkaVCiziMene
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal? AmountWithoutVatInForeignCurrency
		{
			get => _amountWithoutVatInForeignCurrency;
			set
			{
				bool changed = (_amountWithoutVatInForeignCurrency != value);
				_amountWithoutVatInForeignCurrency = value;
				if (changed)
				{
					RecalculateAmounts();
					if (this.Transaction != null)
					{
						this.Transaction.RecalculateTotals();
					}
				}
			}
		}
		private decimal? _amountWithoutVatInForeignCurrency;

		/// <summary>
		/// Normalized VAT rate [19% -&gt; 0.19].
		/// G2: SazbaDph
		/// </summary>
		[Column(TypeName = "decimal(9, 5)")]
		public decimal VatRate { get; set; }

		/// <summary>
		/// G2: FakturaItem.CastkaDph
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal VatAmount
		{
			get => _vatAmount;
			set
			{
				bool changed = (_vatAmount != value);
				_vatAmount = value;
				if (changed)
				{
					RecalculateAmounts();
					if (this.Transaction != null)
					{
						this.Transaction.RecalculateTotals();
					}
				}
			}
		}
		private decimal _vatAmount;

		/// <summary>
		/// G2: FakturaItem.CastkaVCiziMeneDph
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal? VatAmountInForeignCurrency
		{
			get => _vatAmountInForeignCurrency;
			set
			{
				bool changed = (_vatAmountInForeignCurrency != value);
				_vatAmountInForeignCurrency = value;
				if (changed)
				{
					RecalculateAmounts();
					if (this.Transaction != null)
					{
						this.Transaction.RecalculateTotals();
					}
				}
			}
		}
		private decimal? _vatAmountInForeignCurrency;

		/// <summary>
		/// G2: FakturaItem.CastkaVcetneDph
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal AmountIncludingVat
		{
			get => _amountIncludingVat;
			private set
			{
				bool changed = (_amountIncludingVat != value);
				_amountIncludingVat = value;
				if ((this.Transaction != null) && changed)
				{
					this.Transaction.RecalculateTotals();
				}
			}
		}
		private decimal _amountIncludingVat;

		/// <summary>
		/// G2: FakturaItem.CastkaVCiziMeneVcetneDph
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal? AmountIncludingVatInForeignCurrency
		{
			get => _amountIncludingVatInForeignCurrency;
			private set
			{
				bool changed = (_amountIncludingVatInForeignCurrency != value);
				_amountIncludingVatInForeignCurrency = value;
				if ((this.Transaction != null) && changed)
				{
					this.Transaction.RecalculateTotals();
				}
			}
		}
		private decimal? _amountIncludingVatInForeignCurrency;

		/// <summary>
		/// G2: FakturaItem.Projekt, PlanProjektu.Projekt
		/// </summary>
		public Project Project { get; set; }
		public int ProjectId { get; set; }

		/// <summary>
		/// G2: FakturaItem.Faze, PlanProjektu.Faze
		/// </summary>
		public ProjectPhase ProjectPhase { get; set; }
		public int? ProjectPhaseId { get; set; }

		/// <summary>
		/// User, who approved the item. If null, the approval might have been automatic (see ApprovedAt to check if the item is approved).
		/// G2: FakturaItem.SchvalilId (Pracovnik=>User!)
		/// </summary>
		public User ApprovedBy { get; set; }
		public int? ApprovedById { get; set; }

		/// <summary>
		/// Indicates approval.
		/// G2: FakturaItem.SchvalenoKdy
		/// </summary>
		public DateTime? ApprovedAt { get; set; }

		public int? MigrationId { get; set; }

		/// <summary>
		/// Order (position) of the item within transaction.
		/// </summary>
		public int ItemOrder { get; set; }

		public decimal AmountWithoutVatInTransactionCurrency => this.Transaction.IsForeignCurrency ? this.AmountWithoutVatInForeignCurrency.GetValueOrDefault() : this.AmountWithoutVat;
		public decimal AmountIncludingVatInTransactionCurrency => this.Transaction.IsForeignCurrency ? this.AmountIncludingVatInForeignCurrency.GetValueOrDefault() : this.AmountIncludingVat;
		public decimal VatAmountInTransactionCurrency => this.Transaction.IsForeignCurrency ? this.VatAmountInForeignCurrency.GetValueOrDefault() : this.VatAmount;

		internal void RecalculateAmounts()
		{
			this.AmountIncludingVat = this.AmountWithoutVat + this.VatAmount;
			if (this.Transaction?.IsForeignCurrency == true)
			{
				// TODO G2: pro změny měn již hotových faktur?
				//CastkaVCiziMeneBezDph = CastkaVCiziMeneBezDph.GetValueOrDefault();
				//CastkaVCiziMeneDph = CastkaVCiziMeneDph.GetValueOrDefault();

				this.AmountIncludingVatInForeignCurrency = this.AmountWithoutVatInForeignCurrency + this.VatAmountInForeignCurrency;
			}
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			Contract.Assert<InvalidOperationException>(this.Transaction != null, nameof(this.Transaction));

			if (this.AmountIncludingVat != (this.AmountWithoutVat + this.VatAmount))
			{
				yield return new ValidationResult($"{this.AmountIncludingVat} does not correspond to {this.AmountWithoutVat} + {this.VatAmount}");
			}

			if (this.Transaction.IsForeignCurrency && ((this.AmountWithoutVatInForeignCurrency == null) || (this.VatAmountInForeignCurrency == null) || (this.AmountIncludingVatInForeignCurrency == null)))
			{
				yield return new ValidationResult("Transaction in foreign currency has to have amounts in foreign currency.");
			}
		}
	}
}
