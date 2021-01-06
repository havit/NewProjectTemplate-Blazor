using Havit.GoranG3.Model.Finance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Tests.Finance
{
	[TestClass]
	public class TransactionTests
	{
		[TestMethod]
		public void Transaction_TotalAmounts_NewTransaction_ObjectInitializer()
		{
			// arrange, act
			var transaction = new Transaction()
			{
				Items =
				{
					new TransactionItem()
					{
						AmountWithoutVat = 100,
						VatAmount = 22
					},
					new TransactionItem()
					{
						AmountWithoutVat = 10,
						VatAmount = 2.2m
					}
				}
			};

			// assert
			Assert.AreEqual(110, transaction.TotalAmountWithoutVat);
			Assert.AreEqual(134.2m, transaction.TotalAmountIncludingVat);
		}

		[TestMethod]
		public void Transaction_TotalAmounts_NewTransaction_Standalone()
		{
			// arrange, act
			var transaction = new Transaction();
			var item1 = new TransactionItem();
			var item2 = new TransactionItem();

			// act
			item1.AmountWithoutVat = 100;
			item1.VatAmount = 22;
			transaction.Items.Add(item1);
			item2.AmountWithoutVat = 10;
			item2.VatAmount = 2.2m;
			transaction.Items.Add(item2);

			// assert
			Assert.AreEqual(110, transaction.TotalAmountWithoutVat);
			Assert.AreEqual(134.2m, transaction.TotalAmountIncludingVat);
		}

		[TestMethod]
		public void Transaction_ItemsOrder_SetsOrderForNewItems()
		{
			// arrange, act
			var transaction = new Transaction();
			var item1 = new TransactionItem();
			var item2 = new TransactionItem();

			// act
			transaction.Items.Add(item1);
			transaction.Items.Add(item2);

			// assert
			Assert.AreEqual(1, item1.ItemOrder);
			Assert.AreEqual(2, item2.ItemOrder);
		}

		[TestMethod]
		public void Transaction_PaymentsCalculations_TwoPayments()
		{
			// arrange, act
			var transaction = new Transaction()
			{
				Items =
				{
					new TransactionItem()
					{
						AmountWithoutVat = 100,
					}
				},
				Payments =
				{
					new Payment()
					{
						Amount = 80,
						PaymentDate = new DateTime(2020, 1, 1)
					},
					new Payment()
					{
						Amount = 20,
						PaymentDate = new DateTime(2020, 2, 1)
					}
				}
			};

			// assert
			Assert.AreEqual(new DateTime(2020, 2, 1), transaction.PaidDate);
			Assert.AreEqual(100m, transaction.TotalAmountPaid);
		}

		[TestMethod]
		public void Transaction_PaymentsCalculations_PartialPayment()
		{
			// arrange, act
			var transaction = new Transaction()
			{
				Items =
				{
					new TransactionItem()
					{
						AmountWithoutVat = 100,
					}
				},
				Payments =
				{
					new Payment()
					{
						Amount = 80,
						PaymentDate = new DateTime(2020, 1, 1)
					}
				}
			};

			// assert
			Assert.IsNull(transaction.PaidDate);
			Assert.AreEqual(80m, transaction.TotalAmountPaid);
		}

		[TestMethod]
		public void Transaction_PaymentsCalculations_SinglePayment()
		{
			// arrange, act
			var transaction = new Transaction()
			{
				Items =
				{
					new TransactionItem()
					{
						AmountWithoutVat = 100,
					}
				},
				Payments =
				{
					new Payment()
					{
						Amount = 100,
						PaymentDate = new DateTime(2020, 1, 1)
					}
				}
			};

			// assert
			Assert.AreEqual(new DateTime(2020, 1, 1), transaction.PaidDate);
			Assert.AreEqual(100m, transaction.TotalAmountPaid);
		}

		[TestMethod]
		public void Transaction_PaymentsCalculations_NotPaid()
		{
			// arrange, act
			var transaction = new Transaction()
			{
				Items =
				{
					new TransactionItem()
					{
						AmountWithoutVat = 100,
					}
				},
			};

			// assert
			Assert.IsNull(transaction.PaidDate);
			Assert.AreEqual(0m, transaction.TotalAmountPaid);
		}
	}
}
