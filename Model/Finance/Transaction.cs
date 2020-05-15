using Havit.GoranG3.Model.Crm;
using Havit.GoranG3.Model.Sequences;
using Havit.Model.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Finance
{
	/// <summary>
	/// Invoice (regular/planned), ...
	/// G2: Faktura
	/// </summary>
	public class Transaction : IValidatableObject
	{
		public int Id { get; set; }

		/// <summary>
		/// G2: Faktura.Popis, PlatbaHistorie.Popis (1/2)
		/// </summary>
		[MaxLength(200)]
		public string Description { get; set; }

		/// <summary>
		/// Invoice Issued / Invoice Received / ...
		/// G2: Faktura.SmerFaktury, PlatbaHistorie.SmerPlatby 
		/// </summary>
		public TransactionType TransactionType { get; set; }

		/// <summary>
		/// G2: Faktura (Regular) vs PlanovanaPlatba (Plan)
		/// </summary>
		public TransactionStatus TransactionStatus { get; set; }

		/// <summary>
		/// Document (invoice) number on our side.
		/// G2: Faktura.CisloNase
		/// </summary>
		[MaxLength(30)]
		public string OurReference { get; set; }

		/// <summary>
		/// NumberSequence used for OurReference (if used).
		/// G2: Faktura.CisloNaseCiselnaRadaId
		/// </summary>
		public NumberSequence OurReferenceNumberSequence { get; set; }

		public int? OurReferenceNumberSequenceId { get; set; }

		/// <summary>
		/// Value in OurReferenceNumberSequence (if used). 
		/// G2: Faktura.CisloNaseCiselnaRadaHodnota
		/// </summary>
		public int? OurReferenceNumberSequenceValue { get; set; }

		/// <summary>
		/// Document (invoice) number on business-partner side. Makes sense for received documents only.
		/// G2: Faktura.CisloProtistrany
		/// </summary>
		[MaxLength(30)]
		public string BusinessPartnerReference { get; set; }

		/// <summary>
		/// G2: Faktura.Protistrana, PlatbaHistorie.ProtistranaPlatby
		/// </summary>
		public Contact BusinessPartner { get; set; }
		public int BusinessPartnerId { get; set; }

		/// <summary>
		/// Total amount without VAT (sum from items), in home currency.
		/// G2: Faktura.CastkaCelkemBezDph
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal TotalAmountWithoutVat { get; private set; } // TODO Kalkulace totals na faktuře

		/// <summary>
		/// Total amount including VAT (sum from items), in home currency.
		/// G2: Faktura.CastkaCelkemVcetneDph
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal TotalAmountIncludingVat { get; private set; }

		/// <summary>
		/// Transaction currency (if not in home currency).
		/// G2: Faktura.CurrencyID
		/// </summary>
		public Currency Currency
		{
			get => _currency;
			set
			{
				_currency = value;
				if (_currency != value)
				{
					_currency = value;
					foreach (var item in this.Items)
					{
						item.RecalculateAmounts();
					}
					RecalculateTotals();
				}
			}
		}
		private Currency _currency;
		private int? _currencyId;
		private DateTime? registrationDate;

		public int? CurrencyId
		{
			get => _currencyId;
			set
			{
				_currencyId = value;
				if (_currencyId != value)
				{
					_currencyId = value;
					foreach (var item in this.Items)
					{
						item.RecalculateAmounts();
					}
					RecalculateTotals();
				}
			}
		}

		/// <summary>
		/// Total amount without VAT if not in home currency (sum from items).
		/// G2: Faktura.CastkaCelkemVCiziMeneBezDph
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal? TotalAmountWithoutVatInForeignCurrency { get; private set; }

		/// <summary>
		/// Total amount including VAT if not in home currency (sum from items).
		/// G2: Faktura.CastkaCelkemVCiziMeneVcetneDph 
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal? TotalAmountIncludingVatInForeignCurrency { get; private set; }

		/// <summary>
		/// Date, when the document (invoice) was issued/received.
		/// G2: Faktura.DatumVystaveniPrijeti, PlatbaHistorie.DatumPlan (1/2)
		/// </summary>
		[Column(TypeName = "Date")]
		public DateTime? RegistrationDate
		{
			get => registrationDate;
			set
			{
				registrationDate = value;
				CalculatePayment();
			}
		}

		/// <summary>
		/// Date when the transaction took business effect (delivery date, ...). Also used as tax date for VAT.
		/// G2: Faktura.DatumDph, PlatbaHistorie.DatumPlan (2/2)
		/// </summary>
		[Column(TypeName = "Date")]
		public DateTime? EffectiveDate { get; set; }

		/// <summary>
		/// If set, the PaymentDueDate has higher priority than PaymentDueDays.
		/// G2: Faktura.DatumSplatnosti
		/// </summary>
		[Column(TypeName = "Date")]
		public DateTime? PaymentDueDate { get; set; }

		/// <summary>
		/// If set, the PaymentDueDate has higher priority than PaymentDueDays.
		/// G2: PlatbaHistorie.Splatnost
		/// </summary>
		public int? PaymentDueDays { get; set; }

		/// <summary>
		/// Date, when the payment was ordered in bank. 
		/// G2: PrikazKUhradeDne
		/// </summary>
		/// <remarks>
		/// Used probably only in CONTRACTIS. Might be configurable in the UI?
		/// </remarks>
		[Column(TypeName = "Date")]
		public DateTime? PaymentOrderDate { get; set; }

		/// <summary>
		/// Total amount paid (sum of payments).
		/// G2: UhrazenoCelkem
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal TotalAmountPaid { get; private set; }

		/// <summary>
		/// Total amount paid when the invoice is in foreign currency (sum of payments).
		/// G2: UhrazenoCelkemVCiziMene
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal? TotalAmountPaidInForeignCurrency { get; private set; }

		/// <summary>
		/// Date when the invoice was fully paid (sum of payments exceeded total amount including VAT).
		/// G2: ZcelaUhrazenoDne
		/// </summary>
		[Column(TypeName = "Date")]
		public DateTime? PaidDate { get; set; }

		/// <summary>
		/// Internal notes.
		/// G2: Poznamka
		/// </summary>
		[MaxLength]
		public string Note { get; set; }

		/// <summary>
		/// Document template to be used for printing the transaction. If null, default template will be used.
		/// G2: SablonaFaktury
		/// </summary>
		public TransactionDocumentTemplate DocumentTemplate { get; set; }
		public int? DocumentTemplateId { get; set; }

		/// <summary>
		/// Link to realization of planned transaction (+ open for simmilar usages)
		/// G2: Platba.FakturaItem
		/// </summary>
		public Transaction RelatedTransaction { get; set; }
		public int RelatedTransactionId { get; set; }

		/// <summary>
		/// G2: BankovniUcet
		/// </summary>
		public BankAccount BankAccount { get; set; }
		public int? BankAccountId { get; set; }

		public DateTime Created { get; set; }

		public DateTime? Deleted { get; set; }

		public ObservableCollection<TransactionItem> Items { get; } = new ObservableCollection<TransactionItem>();

		/// <summary>
		/// G2: Uhrady
		/// </summary>
		public ObservableCollection<Payment> PaymentsIncludingDeleted { get; } = new ObservableCollection<Payment>();

		public FilteringCollection<Payment> Payments { get; }

		public bool IsForeignCurrency => this.Currency != null;
		public decimal TotalAmountIncludingVatInTransactionCurrency => IsForeignCurrency ? TotalAmountIncludingVatInForeignCurrency.GetValueOrDefault() : TotalAmountIncludingVat;
		public decimal TotalAmountWithoutVatInTransactionCurrency => IsForeignCurrency ? TotalAmountWithoutVatInForeignCurrency.GetValueOrDefault() : TotalAmountWithoutVat;
		public decimal? TotalAmountPaidInTransactionCurrency => IsForeignCurrency ? TotalAmountPaidInForeignCurrency.GetValueOrDefault() : TotalAmountPaid;

		public Transaction()
		{
			this.Payments = new FilteringCollection<Payment>(this.PaymentsIncludingDeleted, p => p.Deleted is null);
			this.Items.CollectionChanged += Items_CollectionChanged;
			this.PaymentsIncludingDeleted.CollectionChanged += Payments_CollectionChanged;
		}

		private void Payments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			CalculatePayment();
		}

		private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			RecalculateTotals();
			SetNewItemsOrder();
		}

		private void SetNewItemsOrder()
		{
			if (this.Items.Count > 0)
			{
				int maxOrder = Items.Max(item => item.ItemOrder);

				foreach (var item in this.Items)
				{
					if (item.ItemOrder == 0)
					{
						maxOrder++;
						item.ItemOrder = maxOrder;
					}
				}
			}
		}

		internal void RecalculateTotals() // G2
		{
			decimal totalAmountWithoutVat = 0;
			decimal totalAmountIncludingVat = 0;
			foreach (var item in Items)
			{
				totalAmountWithoutVat += item.AmountWithoutVat;
				totalAmountIncludingVat += item.AmountIncludingVat;
			}
			this.TotalAmountWithoutVat = totalAmountWithoutVat;
			this.TotalAmountIncludingVat = totalAmountIncludingVat;

			if (IsForeignCurrency)
			{
				this.TotalAmountWithoutVatInForeignCurrency = Items.Sum(i => i.AmountWithoutVatInForeignCurrency.GetValueOrDefault());
				this.TotalAmountIncludingVatInForeignCurrency = Items.Sum(i => i.AmountIncludingVatInForeignCurrency.GetValueOrDefault());
			}
			else
			{
				TotalAmountWithoutVatInForeignCurrency = null;
				TotalAmountIncludingVatInForeignCurrency = null;
			}

			CalculatePayment();
		}

		internal void CalculatePayment() // G2
		{
			DateTime? paidDate = null;
			decimal totalAmountPaid = 0;
			decimal? totalAmountPaidInForeignCurrency = null;

			if (this.TotalAmountIncludingVat == 0)
			{
				// pokud je celková částka na faktuře nulová
				if (Items.Any())
				{
					// a jsou položky na faktuře, nastavíme zcela uhrazeno
					paidDate = this.RegistrationDate;
				}
				else
				{
					// a nejsou položky na faktuře, je to paskvil, který není uhrazen
					// NOOP
				}

				// musíme ale spočítat uhrazenou částku
				totalAmountPaid = Payments.Sum(p => p.Amount);
				if (IsForeignCurrency)
				{
					totalAmountPaidInForeignCurrency = Payments.Sum(p => p.AmountInForeignCurrency.GetValueOrDefault());
				}
			}
			else
			{
				// částka na faktuře je nenulová
				totalAmountPaid = Payments.Sum(u => u.Amount);
				decimal totalAmountPaidInTransactionCurrency;
				if (IsForeignCurrency)
				{
					totalAmountPaidInForeignCurrency = Payments.Sum(p => p.AmountInForeignCurrency.GetValueOrDefault());
					totalAmountPaidInTransactionCurrency = totalAmountPaidInForeignCurrency.GetValueOrDefault();
				}
				else
				{
					totalAmountPaidInTransactionCurrency = totalAmountPaid;
				}

				// spočítáme datum úhrady
				decimal paidByPayments = 0;
				foreach (var payment in Payments.OrderBy(p => p.PaymentDate))
				{
					paidByPayments += (!IsForeignCurrency) ? payment.Amount : payment.AmountInForeignCurrency.GetValueOrDefault();
					// pokud jsme dosáhli výši faktury
					if ((Math.Sign(totalAmountPaidInTransactionCurrency) == Math.Sign(this.TotalAmountIncludingVat)) && (Math.Abs(paidByPayments) >= Math.Abs(this.TotalAmountIncludingVatInTransactionCurrency)))
					{
						// zapamatujeme si, kdy byla faktura uhrazena
						paidDate = payment.PaymentDate;
						break; // dál procházet nemusím
					}
				}
			}

			this.TotalAmountPaid = totalAmountPaid;
			this.TotalAmountPaidInForeignCurrency = totalAmountPaidInForeignCurrency;
			this.PaidDate = paidDate;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) // TODO Zapojit IValidatableObject do lifecycle. OnBeforeCommitAction?
		{
			if (this.TransactionStatus == TransactionStatus.Regular)
			{
				const string errorMessage = "Property has to be set for " + nameof(TransactionStatus) + "." + nameof(TransactionStatus.Regular) + ".";
				if (String.IsNullOrWhiteSpace(this.OurReference))
				{
					yield return new ValidationResult(errorMessage, new[] { nameof(this.OurReference) });
				}
				if (this.RegistrationDate is null)
				{
					yield return new ValidationResult(errorMessage, new[] { nameof(this.RegistrationDate) });
				}
				if (this.PaymentDueDate is null)
				{
					yield return new ValidationResult(errorMessage, new[] { nameof(this.PaymentDueDate) });
				}
			}

			if (this.TotalAmountPaid != this.Payments.Sum(p => p.Amount))
			{
				yield return new ValidationResult("Sum of payments not equal to total amount paid.", new[] { nameof(this.TotalAmountPaid) });
			}

			if (!this.IsForeignCurrency)
			{
				const string errorMessage = "Transaction in home currency cannot have amounts in foreign currency.";
				if (this.TotalAmountIncludingVatInForeignCurrency.GetValueOrDefault() != 0)
				{
					yield return new ValidationResult(errorMessage, new[] { nameof(this.TotalAmountIncludingVatInForeignCurrency) });
				}
				if (this.TotalAmountPaidInForeignCurrency.GetValueOrDefault() != 0)
				{
					yield return new ValidationResult(errorMessage, new[] { nameof(this.TotalAmountPaidInForeignCurrency) });
				}
				if (this.TotalAmountWithoutVatInForeignCurrency.GetValueOrDefault() != 0)
				{
					yield return new ValidationResult(errorMessage, new[] { nameof(this.TotalAmountWithoutVatInForeignCurrency) });
				}
			}
			else
			{
				const string errorMessage = "Transaction in foreign currency cannot have null amounts in foreign currency.";
				if (this.TotalAmountIncludingVatInForeignCurrency is null)
				{
					yield return new ValidationResult(errorMessage, new[] { nameof(this.TotalAmountIncludingVatInForeignCurrency) });
				}
				if (this.TotalAmountPaidInForeignCurrency is null)
				{
					yield return new ValidationResult(errorMessage, new[] { nameof(this.TotalAmountPaidInForeignCurrency) });
				}
				if (this.TotalAmountWithoutVatInForeignCurrency is null)
				{
					yield return new ValidationResult(errorMessage, new[] { nameof(this.TotalAmountWithoutVatInForeignCurrency) });
				}
			}
		}
	}
}
