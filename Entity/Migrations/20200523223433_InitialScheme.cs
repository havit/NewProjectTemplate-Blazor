using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Havit.GoranG3.Entity.Migrations
{
	public partial class InitialScheme : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateSequence<int>(
				name: "ContactSequence");

			migrationBuilder.CreateSequence<int>(
				name: "ProjectSequence");

			migrationBuilder.CreateSequence<int>(
				name: "TeamSequence");

			migrationBuilder.CreateTable(
				name: "__DataSeed",
				columns: table => new
				{
					ProfileName = table.Column<string>(maxLength: 250, nullable: false),
					Version = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DataSeed", x => x.ProfileName);
				});

			migrationBuilder.CreateTable(
				name: "AbsenceType",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbsenceType", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ApplicationSettings",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ApplicationSettings", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AttridaObject",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AttridaObject", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "BankAccount",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(maxLength: 50, nullable: false),
					BankName = table.Column<string>(maxLength: 50, nullable: false),
					AccountNumber = table.Column<string>(maxLength: 50, nullable: false),
					Iban = table.Column<string>(maxLength: 50, nullable: true),
					SwiftBic = table.Column<string>(maxLength: 50, nullable: true),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_BankAccount", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Country",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					IsoCode = table.Column<string>(maxLength: 2, nullable: false),
					IsoCode3 = table.Column<string>(maxLength: 3, nullable: false),
					PhoneCountryCode = table.Column<string>(maxLength: 6, nullable: true),
					UiOrder = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Country", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "DateInfo",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Date = table.Column<DateTime>(nullable: false),
					IsHoliday = table.Column<bool>(nullable: false),
					Description = table.Column<string>(maxLength: 50, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DateInfo", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "EmploymentTerms",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RateType = table.Column<int>(nullable: false),
					HourPerDay = table.Column<decimal>(type: "decimal(9, 2)", nullable: false),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EmploymentTerms", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "EmploymentType",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(maxLength: 50, nullable: false),
					EmployerContributionsRate = table.Column<decimal>(type: "decimal(9, 5)", nullable: false),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EmploymentType", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Language",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false),
					Name = table.Column<string>(maxLength: 200, nullable: true),
					Culture = table.Column<string>(maxLength: 10, nullable: true),
					UiCulture = table.Column<string>(maxLength: 10, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Language", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "NumberSequence",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(maxLength: 50, nullable: false),
					Prefix = table.Column<string>(maxLength: 10, nullable: true),
					Suffix = table.Column<string>(maxLength: 10, nullable: true),
					DigitCount = table.Column<int>(nullable: true),
					InitialValue = table.Column<int>(nullable: false),
					LastValue = table.Column<int>(nullable: true),
					IsActive = table.Column<bool>(nullable: false),
					StartDate = table.Column<DateTime>(nullable: true),
					EndDate = table.Column<DateTime>(nullable: true),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_NumberSequence", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "OverheadToPersonalCostsRatio",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					StartDate = table.Column<DateTime>(nullable: false),
					Ratio = table.Column<decimal>(type: "decimal(9, 4)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_OverheadToPersonalCostsRatio", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ProjectPhase",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProjectPhase", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Role",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false),
					Name = table.Column<string>(maxLength: 255, nullable: true),
					NormalizedName = table.Column<string>(maxLength: 255, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Role", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Team",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR TeamSequence"),
					Name = table.Column<string>(maxLength: 50, nullable: false),
					IsPrivateTeam = table.Column<bool>(nullable: false),
					IsSystemTeam = table.Column<bool>(nullable: false),
					IsActive = table.Column<bool>(nullable: false),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Team", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "TimesheetItemCategory",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(maxLength: 50, nullable: false),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TimesheetItemCategory", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "TransactionDocumentTemplate",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TransactionDocumentTemplate", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "TransactionItemType",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TransactionItemType", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "User",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Username = table.Column<string>(type: "nvarchar(50) COLLATE Latin1_General_CI_AI", maxLength: 50, nullable: true),
					NormalizedUsername = table.Column<string>(maxLength: 50, nullable: true),
					Email = table.Column<string>(type: "nvarchar(255) COLLATE Latin1_General_CI_AI", maxLength: 255, nullable: true),
					NormalizedEmail = table.Column<string>(maxLength: 255, nullable: true),
					EmailConfirmed = table.Column<bool>(nullable: false),
					PasswordHash = table.Column<string>(maxLength: 2147483647, nullable: true),
					LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
					LockoutEnabled = table.Column<bool>(nullable: false),
					AccessFailedCount = table.Column<int>(nullable: false),
					SecurityStamp = table.Column<string>(maxLength: 255, nullable: true),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_User", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AttridaDocument",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					AttridaObjectId = table.Column<int>(nullable: false),
					OriginalFilename = table.Column<string>(maxLength: 200, nullable: false),
					StorageFilename = table.Column<string>(maxLength: 200, nullable: false),
					FileType = table.Column<int>(nullable: false),
					Description = table.Column<string>(maxLength: 100, nullable: true),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AttridaDocument", x => x.Id);
					table.ForeignKey(
						name: "FK_AttridaDocument_AttridaObject_AttridaObjectId",
						column: x => x.AttridaObjectId,
						principalTable: "AttridaObject",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "AttridaTag",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					AttridaObjectId = table.Column<int>(nullable: false),
					Name = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AttridaTag", x => x.Id);
					table.ForeignKey(
						name: "FK_AttridaTag_AttridaObject_AttridaObjectId",
						column: x => x.AttridaObjectId,
						principalTable: "AttridaObject",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Currency",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					DefaultBankAccountId = table.Column<int>(nullable: true),
					Code = table.Column<string>(maxLength: 50, nullable: false),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Currency", x => x.Id);
					table.ForeignKey(
						name: "FK_Currency_BankAccount_DefaultBankAccountId",
						column: x => x.DefaultBankAccountId,
						principalTable: "BankAccount",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Address",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Line1 = table.Column<string>(maxLength: 50, nullable: true),
					Line2 = table.Column<string>(maxLength: 200, nullable: false),
					City = table.Column<string>(maxLength: 200, nullable: false),
					Zip = table.Column<string>(maxLength: 20, nullable: false),
					CountryId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Address", x => x.Id);
					table.ForeignKey(
						name: "FK_Address_Country_CountryId",
						column: x => x.CountryId,
						principalTable: "Country",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "CountryLocalization",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					ParentId = table.Column<int>(nullable: false),
					LanguageId = table.Column<int>(nullable: false),
					Name = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CountryLocalization", x => x.Id);
					table.ForeignKey(
						name: "FK_CountryLocalization_Language_LanguageId",
						column: x => x.LanguageId,
						principalTable: "Language",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_CountryLocalization_Country_ParentId",
						column: x => x.ParentId,
						principalTable: "Country",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "NumberSequenceUnusedNumber",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					NumberSequenceId = table.Column<int>(nullable: false),
					Value = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_NumberSequenceUnusedNumber", x => x.Id);
					table.ForeignKey(
						name: "FK_NumberSequenceUnusedNumber_NumberSequence_NumberSequenceId",
						column: x => x.NumberSequenceId,
						principalTable: "NumberSequence",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "AttridaComment",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					AttridaObjectId = table.Column<int>(nullable: false),
					Text = table.Column<string>(nullable: false),
					CreatedById = table.Column<int>(nullable: false),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AttridaComment", x => x.Id);
					table.ForeignKey(
						name: "FK_AttridaComment_AttridaObject_AttridaObjectId",
						column: x => x.AttridaObjectId,
						principalTable: "AttridaObject",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AttridaComment_User_CreatedById",
						column: x => x.CreatedById,
						principalTable: "User",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "UserRole",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					RoleId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_UserRole_Role_RoleId",
						column: x => x.RoleId,
						principalTable: "Role",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_UserRole_User_UserId",
						column: x => x.UserId,
						principalTable: "User",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "ExchangeRate",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CurrencyId = table.Column<int>(nullable: false),
					DateFrom = table.Column<DateTime>(nullable: false),
					Rate = table.Column<decimal>(type: "Money", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ExchangeRate", x => x.Id);
					table.ForeignKey(
						name: "FK_ExchangeRate_Currency_CurrencyId",
						column: x => x.CurrencyId,
						principalTable: "Currency",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Contact",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR ContactSequence"),
					Name = table.Column<string>(maxLength: 200, nullable: false),
					RegisteredAddressId = table.Column<int>(nullable: true),
					ContactAddressId = table.Column<int>(nullable: true),
					Phone = table.Column<string>(maxLength: 20, nullable: true),
					Mobile = table.Column<string>(maxLength: 20, nullable: true),
					Email = table.Column<string>(maxLength: 200, nullable: true),
					Web = table.Column<string>(maxLength: 200, nullable: true),
					CompanyRegistrationNumber = table.Column<string>(maxLength: 15, nullable: true),
					TaxRegistrationNumber = table.Column<string>(maxLength: 15, nullable: true),
					IsVatPayer = table.Column<bool>(nullable: false),
					CertificateOfIncorporation = table.Column<string>(maxLength: 100, nullable: true),
					BankName = table.Column<string>(maxLength: 400, nullable: true),
					BankAccountNumber = table.Column<string>(maxLength: 50, nullable: true),
					BankAccountIban = table.Column<string>(maxLength: 40, nullable: true),
					BankAccountSwiftBic = table.Column<string>(maxLength: 11, nullable: true),
					Note = table.Column<string>(nullable: true),
					IsArchived = table.Column<bool>(nullable: false),
					IsBasicContact = table.Column<bool>(nullable: false),
					ExternalCode = table.Column<string>(maxLength: 50, nullable: true),
					AttridaObjectId = table.Column<int>(nullable: true),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true),
					HasNoVatForInvoicesIssued = table.Column<bool>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Contact", x => x.Id);
					table.ForeignKey(
						name: "FK_Contact_AttridaObject_AttridaObjectId",
						column: x => x.AttridaObjectId,
						principalTable: "AttridaObject",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Contact_Address_ContactAddressId",
						column: x => x.ContactAddressId,
						principalTable: "Address",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Contact_Address_RegisteredAddressId",
						column: x => x.RegisteredAddressId,
						principalTable: "Address",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "ContactRelationship",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RelationshipType = table.Column<int>(nullable: false),
					ParentContactId = table.Column<int>(nullable: false),
					DetailContactId = table.Column<int>(nullable: false),
					Description = table.Column<string>(maxLength: 100, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ContactRelationship", x => x.Id);
					table.ForeignKey(
						name: "FK_ContactRelationship_Contact_DetailContactId",
						column: x => x.DetailContactId,
						principalTable: "Contact",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_ContactRelationship_Contact_ParentContactId",
						column: x => x.ParentContactId,
						principalTable: "Contact",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Employee",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(nullable: true),
					PrivateTeamId = table.Column<int>(nullable: false),
					TitlePrefix = table.Column<string>(maxLength: 20, nullable: true),
					FirstName = table.Column<string>(maxLength: 50, nullable: false),
					LastName = table.Column<string>(maxLength: 50, nullable: false),
					TitleSuffix = table.Column<string>(maxLength: 20, nullable: true),
					ContactId = table.Column<int>(nullable: true),
					BossId = table.Column<int>(nullable: true),
					BirthNumber = table.Column<string>(maxLength: 15, nullable: true),
					BirthDate = table.Column<DateTime>(nullable: true),
					CooperationStartDate = table.Column<DateTime>(nullable: true),
					CooperationEndDate = table.Column<DateTime>(nullable: true),
					Note = table.Column<string>(nullable: true),
					AbsencesAutoApproved = table.Column<bool>(nullable: false),
					TimesheetsAutoApproved = table.Column<bool>(nullable: false),
					TimesheetNotificationsEnabled = table.Column<bool>(nullable: false),
					IsActive = table.Column<bool>(nullable: false),
					AttridaObjectId = table.Column<int>(nullable: true),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Employee", x => x.Id);
					table.ForeignKey(
						name: "FK_Employee_AttridaObject_AttridaObjectId",
						column: x => x.AttridaObjectId,
						principalTable: "AttridaObject",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Employee_Employee_BossId",
						column: x => x.BossId,
						principalTable: "Employee",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Employee_Contact_ContactId",
						column: x => x.ContactId,
						principalTable: "Contact",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Employee_Team_PrivateTeamId",
						column: x => x.PrivateTeamId,
						principalTable: "Team",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Employee_User_UserId",
						column: x => x.UserId,
						principalTable: "User",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Project",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR ProjectSequence"),
					ParentId = table.Column<int>(nullable: true),
					Depth = table.Column<int>(nullable: false),
					ProjectCode = table.Column<string>(maxLength: 20, nullable: false),
					Name = table.Column<string>(maxLength: 100, nullable: false),
					ProjectManagerId = table.Column<int>(nullable: true),
					ProjectManagerEffectiveId = table.Column<int>(nullable: true),
					BusinessPartnerId = table.Column<int>(nullable: true),
					BusinessPartnerEffectiveId = table.Column<int>(nullable: true),
					PaymentDueDaysDefault = table.Column<int>(nullable: true),
					OverheadToPersonalCostsRatio = table.Column<decimal>(type: "decimal(9, 4)", nullable: true),
					OverheadToPersonalCostsRatioEffective = table.Column<decimal>(type: "decimal(9, 4)", nullable: true),
					IsActive = table.Column<bool>(nullable: true),
					IsActiveEffective = table.Column<bool>(nullable: false),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true),
					AttridaObjectId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Project", x => x.Id);
					table.ForeignKey(
						name: "FK_Project_AttridaObject_AttridaObjectId",
						column: x => x.AttridaObjectId,
						principalTable: "AttridaObject",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Project_Contact_BusinessPartnerEffectiveId",
						column: x => x.BusinessPartnerEffectiveId,
						principalTable: "Contact",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Project_Contact_BusinessPartnerId",
						column: x => x.BusinessPartnerId,
						principalTable: "Contact",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Project_Project_ParentId",
						column: x => x.ParentId,
						principalTable: "Project",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Project_User_ProjectManagerEffectiveId",
						column: x => x.ProjectManagerEffectiveId,
						principalTable: "User",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Project_User_ProjectManagerId",
						column: x => x.ProjectManagerId,
						principalTable: "User",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Transaction",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Description = table.Column<string>(maxLength: 200, nullable: true),
					TransactionType = table.Column<int>(nullable: false),
					TransactionStatus = table.Column<int>(nullable: false),
					OurReference = table.Column<string>(maxLength: 30, nullable: true),
					OurReferenceNumberSequenceId = table.Column<int>(nullable: true),
					OurReferenceNumberSequenceValue = table.Column<int>(nullable: true),
					BusinessPartnerReference = table.Column<string>(maxLength: 30, nullable: true),
					BusinessPartnerId = table.Column<int>(nullable: false),
					TotalAmountWithoutVat = table.Column<decimal>(type: "Money", nullable: false),
					TotalAmountIncludingVat = table.Column<decimal>(type: "Money", nullable: false),
					CurrencyId = table.Column<int>(nullable: true),
					TotalAmountWithoutVatInForeignCurrency = table.Column<decimal>(type: "Money", nullable: true),
					TotalAmountIncludingVatInForeignCurrency = table.Column<decimal>(type: "Money", nullable: true),
					RegistrationDate = table.Column<DateTime>(type: "Date", nullable: true),
					EffectiveDate = table.Column<DateTime>(type: "Date", nullable: true),
					PaymentDueDate = table.Column<DateTime>(type: "Date", nullable: true),
					PaymentDueDays = table.Column<int>(nullable: true),
					PaymentOrderDate = table.Column<DateTime>(type: "Date", nullable: true),
					TotalAmountPaid = table.Column<decimal>(type: "Money", nullable: false),
					TotalAmountPaidInForeignCurrency = table.Column<decimal>(type: "Money", nullable: true),
					PaidDate = table.Column<DateTime>(type: "Date", nullable: true),
					Note = table.Column<string>(nullable: true),
					DocumentTemplateId = table.Column<int>(nullable: true),
					RelatedTransactionId = table.Column<int>(nullable: false),
					BankAccountId = table.Column<int>(nullable: true),
					AttridaObjectId = table.Column<int>(nullable: true),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Transaction", x => x.Id);
					table.ForeignKey(
						name: "FK_Transaction_AttridaObject_AttridaObjectId",
						column: x => x.AttridaObjectId,
						principalTable: "AttridaObject",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Transaction_BankAccount_BankAccountId",
						column: x => x.BankAccountId,
						principalTable: "BankAccount",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Transaction_Contact_BusinessPartnerId",
						column: x => x.BusinessPartnerId,
						principalTable: "Contact",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Transaction_Currency_CurrencyId",
						column: x => x.CurrencyId,
						principalTable: "Currency",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Transaction_TransactionDocumentTemplate_DocumentTemplateId",
						column: x => x.DocumentTemplateId,
						principalTable: "TransactionDocumentTemplate",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Transaction_NumberSequence_OurReferenceNumberSequenceId",
						column: x => x.OurReferenceNumberSequenceId,
						principalTable: "NumberSequence",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Transaction_Transaction_RelatedTransactionId",
						column: x => x.RelatedTransactionId,
						principalTable: "Transaction",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Absence",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					EmployeeId = table.Column<int>(nullable: false),
					StartDate = table.Column<DateTime>(nullable: false),
					EndDate = table.Column<DateTime>(nullable: true),
					Days = table.Column<decimal>(nullable: false),
					AbsenceTypeId = table.Column<int>(nullable: false),
					Description = table.Column<string>(maxLength: 100, nullable: true),
					ApprovedById = table.Column<int>(nullable: true),
					ApprovedAt = table.Column<DateTime>(nullable: true),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Absence", x => x.Id);
					table.ForeignKey(
						name: "FK_Absence_AbsenceType_AbsenceTypeId",
						column: x => x.AbsenceTypeId,
						principalTable: "AbsenceType",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Absence_User_ApprovedById",
						column: x => x.ApprovedById,
						principalTable: "User",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Absence_Employee_EmployeeId",
						column: x => x.EmployeeId,
						principalTable: "Employee",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "EmployeeHistory",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					EmployeeId = table.Column<int>(nullable: false),
					StartDate = table.Column<DateTime>(nullable: false),
					JobPosition = table.Column<string>(maxLength: 50, nullable: true),
					EmploymentTypeId = table.Column<int>(nullable: false),
					BasicRate = table.Column<decimal>(type: "Money", nullable: false),
					EmploymentTermsId = table.Column<int>(nullable: false),
					HourlyCost = table.Column<decimal>(type: "Money", nullable: false),
					OverheadToPersonalCostsRatio = table.Column<decimal>(type: "decimal(9, 4)", nullable: true),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EmployeeHistory", x => x.Id);
					table.ForeignKey(
						name: "FK_EmployeeHistory_Employee_EmployeeId",
						column: x => x.EmployeeId,
						principalTable: "Employee",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_EmployeeHistory_EmploymentTerms_EmploymentTermsId",
						column: x => x.EmploymentTermsId,
						principalTable: "EmploymentTerms",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_EmployeeHistory_EmploymentType_EmploymentTypeId",
						column: x => x.EmploymentTypeId,
						principalTable: "EmploymentType",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "TeamMembership",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					TeamId = table.Column<int>(nullable: false),
					EmployeeId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TeamMembership", x => x.Id);
					table.ForeignKey(
						name: "FK_TeamMembership_Employee_EmployeeId",
						column: x => x.EmployeeId,
						principalTable: "Employee",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_TeamMembership_Team_TeamId",
						column: x => x.TeamId,
						principalTable: "Team",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "BusinessCase",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					BusinessCaseType = table.Column<int>(nullable: false),
					BusinessPartnerId = table.Column<int>(nullable: true),
					ProjectId = table.Column<int>(nullable: true),
					Name = table.Column<string>(maxLength: 200, nullable: false),
					Probability = table.Column<decimal>(type: "decimal(5, 3)", nullable: false),
					FinancialValue = table.Column<decimal>(type: "Money", nullable: true),
					ReminderDate = table.Column<DateTime>(type: "Date", nullable: true),
					AssignedToId = table.Column<int>(nullable: false),
					Description = table.Column<string>(nullable: true),
					State = table.Column<int>(nullable: false),
					AttridaObjectId = table.Column<int>(nullable: true),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true),
					ModifiedById = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_BusinessCase", x => x.Id);
					table.ForeignKey(
						name: "FK_BusinessCase_User_AssignedToId",
						column: x => x.AssignedToId,
						principalTable: "User",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_BusinessCase_AttridaObject_AttridaObjectId",
						column: x => x.AttridaObjectId,
						principalTable: "AttridaObject",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_BusinessCase_Contact_BusinessPartnerId",
						column: x => x.BusinessPartnerId,
						principalTable: "Contact",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_BusinessCase_User_ModifiedById",
						column: x => x.ModifiedById,
						principalTable: "User",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_BusinessCase_Project_ProjectId",
						column: x => x.ProjectId,
						principalTable: "Project",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "ProjectRelation",
				columns: table => new
				{
					HigherProjectId = table.Column<int>(nullable: false),
					LowerProjectId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProjectRelation", x => new { x.HigherProjectId, x.LowerProjectId });
					table.ForeignKey(
						name: "FK_ProjectRelation_Project_HigherProjectId",
						column: x => x.HigherProjectId,
						principalTable: "Project",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_ProjectRelation_Project_LowerProjectId",
						column: x => x.LowerProjectId,
						principalTable: "Project",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "TimesheetItem",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					EmployeeId = table.Column<int>(nullable: false),
					Date = table.Column<DateTime>(nullable: false),
					ProjectId = table.Column<int>(nullable: false),
					ProjectPhaseId = table.Column<int>(nullable: true),
					DurationHours = table.Column<decimal>(type: "decimal(9, 5)", nullable: false),
					PersonalCosts = table.Column<decimal>(type: "Money", nullable: true),
					OverheadCosts = table.Column<decimal>(type: "Money", nullable: true),
					TimesheetItemCategoryId = table.Column<int>(nullable: true),
					Text = table.Column<string>(maxLength: 100, nullable: true),
					ApprovedById = table.Column<int>(nullable: true),
					ApprovedAt = table.Column<DateTime>(nullable: true),
					ExternalId = table.Column<int>(nullable: true),
					ExternalUpdatePending = table.Column<bool>(nullable: false),
					Created = table.Column<DateTime>(nullable: false),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TimesheetItem", x => x.Id);
					table.ForeignKey(
						name: "FK_TimesheetItem_User_ApprovedById",
						column: x => x.ApprovedById,
						principalTable: "User",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_TimesheetItem_Employee_EmployeeId",
						column: x => x.EmployeeId,
						principalTable: "Employee",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_TimesheetItem_Project_ProjectId",
						column: x => x.ProjectId,
						principalTable: "Project",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_TimesheetItem_ProjectPhase_ProjectPhaseId",
						column: x => x.ProjectPhaseId,
						principalTable: "ProjectPhase",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_TimesheetItem_TimesheetItemCategory_TimesheetItemCategoryId",
						column: x => x.TimesheetItemCategoryId,
						principalTable: "TimesheetItemCategory",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Payment",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					TransactionId = table.Column<int>(nullable: false),
					PaymentDate = table.Column<DateTime>(nullable: false),
					Amount = table.Column<decimal>(type: "Money", nullable: false),
					CurrencyId = table.Column<int>(nullable: true),
					AmountInForeignCurrency = table.Column<decimal>(type: "Money", nullable: true),
					Description = table.Column<string>(maxLength: 50, nullable: true),
					Created = table.Column<DateTime>(nullable: false),
					CreatedById = table.Column<int>(nullable: true),
					Deleted = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Payment", x => x.Id);
					table.ForeignKey(
						name: "FK_Payment_User_CreatedById",
						column: x => x.CreatedById,
						principalTable: "User",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Payment_Currency_CurrencyId",
						column: x => x.CurrencyId,
						principalTable: "Currency",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Payment_Transaction_TransactionId",
						column: x => x.TransactionId,
						principalTable: "Transaction",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "TransactionItem",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					TransactionId = table.Column<int>(nullable: false),
					ItemTypeId = table.Column<int>(nullable: false),
					Description = table.Column<string>(nullable: true),
					AmountWithoutVat = table.Column<decimal>(type: "Money", nullable: false),
					AmountWithoutVatInForeignCurrency = table.Column<decimal>(type: "Money", nullable: true),
					VatRate = table.Column<decimal>(type: "decimal(9, 5)", nullable: false),
					VatAmount = table.Column<decimal>(type: "Money", nullable: false),
					VatAmountInForeignCurrency = table.Column<decimal>(type: "Money", nullable: true),
					AmountIncludingVat = table.Column<decimal>(type: "Money", nullable: false),
					AmountIncludingVatInForeignCurrency = table.Column<decimal>(type: "Money", nullable: true),
					ProjectId = table.Column<int>(nullable: false),
					ProjectPhaseId = table.Column<int>(nullable: true),
					ApprovedById = table.Column<int>(nullable: true),
					ApprovedAt = table.Column<DateTime>(nullable: true),
					ItemOrder = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TransactionItem", x => x.Id);
					table.ForeignKey(
						name: "FK_TransactionItem_User_ApprovedById",
						column: x => x.ApprovedById,
						principalTable: "User",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_TransactionItem_TransactionItemType_ItemTypeId",
						column: x => x.ItemTypeId,
						principalTable: "TransactionItemType",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_TransactionItem_Project_ProjectId",
						column: x => x.ProjectId,
						principalTable: "Project",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_TransactionItem_ProjectPhase_ProjectPhaseId",
						column: x => x.ProjectPhaseId,
						principalTable: "ProjectPhase",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_TransactionItem_Transaction_TransactionId",
						column: x => x.TransactionId,
						principalTable: "Transaction",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Absence_AbsenceTypeId",
				table: "Absence",
				column: "AbsenceTypeId");

			migrationBuilder.CreateIndex(
				name: "IX_Absence_ApprovedById",
				table: "Absence",
				column: "ApprovedById");

			migrationBuilder.CreateIndex(
				name: "IX_Absence_EmployeeId",
				table: "Absence",
				column: "EmployeeId");

			migrationBuilder.CreateIndex(
				name: "IX_Address_CountryId",
				table: "Address",
				column: "CountryId");

			migrationBuilder.CreateIndex(
				name: "IX_AttridaComment_AttridaObjectId",
				table: "AttridaComment",
				column: "AttridaObjectId");

			migrationBuilder.CreateIndex(
				name: "IX_AttridaComment_CreatedById",
				table: "AttridaComment",
				column: "CreatedById");

			migrationBuilder.CreateIndex(
				name: "IX_AttridaDocument_AttridaObjectId",
				table: "AttridaDocument",
				column: "AttridaObjectId");

			migrationBuilder.CreateIndex(
				name: "IX_AttridaTag_AttridaObjectId",
				table: "AttridaTag",
				column: "AttridaObjectId");

			migrationBuilder.CreateIndex(
				name: "IX_BusinessCase_AssignedToId",
				table: "BusinessCase",
				column: "AssignedToId");

			migrationBuilder.CreateIndex(
				name: "IX_BusinessCase_AttridaObjectId",
				table: "BusinessCase",
				column: "AttridaObjectId");

			migrationBuilder.CreateIndex(
				name: "IX_BusinessCase_BusinessPartnerId",
				table: "BusinessCase",
				column: "BusinessPartnerId");

			migrationBuilder.CreateIndex(
				name: "IX_BusinessCase_ModifiedById",
				table: "BusinessCase",
				column: "ModifiedById");

			migrationBuilder.CreateIndex(
				name: "IX_BusinessCase_ProjectId",
				table: "BusinessCase",
				column: "ProjectId");

			migrationBuilder.CreateIndex(
				name: "IX_Contact_AttridaObjectId",
				table: "Contact",
				column: "AttridaObjectId");

			migrationBuilder.CreateIndex(
				name: "IX_Contact_ContactAddressId",
				table: "Contact",
				column: "ContactAddressId");

			migrationBuilder.CreateIndex(
				name: "IX_Contact_RegisteredAddressId",
				table: "Contact",
				column: "RegisteredAddressId");

			migrationBuilder.CreateIndex(
				name: "IX_ContactRelationship_DetailContactId",
				table: "ContactRelationship",
				column: "DetailContactId");

			migrationBuilder.CreateIndex(
				name: "IX_ContactRelationship_ParentContactId",
				table: "ContactRelationship",
				column: "ParentContactId");

			migrationBuilder.CreateIndex(
				name: "IX_CountryLocalization_LanguageId",
				table: "CountryLocalization",
				column: "LanguageId");

			migrationBuilder.CreateIndex(
				name: "IX_CountryLocalization_ParentId",
				table: "CountryLocalization",
				column: "ParentId");

			migrationBuilder.CreateIndex(
				name: "IX_Currency_DefaultBankAccountId",
				table: "Currency",
				column: "DefaultBankAccountId");

			migrationBuilder.CreateIndex(
				name: "IX_Employee_AttridaObjectId",
				table: "Employee",
				column: "AttridaObjectId");

			migrationBuilder.CreateIndex(
				name: "IX_Employee_BossId",
				table: "Employee",
				column: "BossId");

			migrationBuilder.CreateIndex(
				name: "IX_Employee_ContactId",
				table: "Employee",
				column: "ContactId");

			migrationBuilder.CreateIndex(
				name: "IX_Employee_PrivateTeamId",
				table: "Employee",
				column: "PrivateTeamId");

			migrationBuilder.CreateIndex(
				name: "IX_Employee_UserId",
				table: "Employee",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_EmployeeHistory_EmployeeId",
				table: "EmployeeHistory",
				column: "EmployeeId");

			migrationBuilder.CreateIndex(
				name: "IX_EmployeeHistory_EmploymentTermsId",
				table: "EmployeeHistory",
				column: "EmploymentTermsId");

			migrationBuilder.CreateIndex(
				name: "IX_EmployeeHistory_EmploymentTypeId",
				table: "EmployeeHistory",
				column: "EmploymentTypeId");

			migrationBuilder.CreateIndex(
				name: "IX_ExchangeRate_CurrencyId",
				table: "ExchangeRate",
				column: "CurrencyId");

			migrationBuilder.CreateIndex(
				name: "IX_NumberSequenceUnusedNumber_NumberSequenceId",
				table: "NumberSequenceUnusedNumber",
				column: "NumberSequenceId");

			migrationBuilder.CreateIndex(
				name: "IX_Payment_CreatedById",
				table: "Payment",
				column: "CreatedById");

			migrationBuilder.CreateIndex(
				name: "IX_Payment_CurrencyId",
				table: "Payment",
				column: "CurrencyId");

			migrationBuilder.CreateIndex(
				name: "IX_Payment_TransactionId",
				table: "Payment",
				column: "TransactionId");

			migrationBuilder.CreateIndex(
				name: "IX_Project_AttridaObjectId",
				table: "Project",
				column: "AttridaObjectId");

			migrationBuilder.CreateIndex(
				name: "IX_Project_BusinessPartnerEffectiveId",
				table: "Project",
				column: "BusinessPartnerEffectiveId");

			migrationBuilder.CreateIndex(
				name: "IX_Project_BusinessPartnerId",
				table: "Project",
				column: "BusinessPartnerId");

			migrationBuilder.CreateIndex(
				name: "IX_Project_ParentId",
				table: "Project",
				column: "ParentId");

			migrationBuilder.CreateIndex(
				name: "IX_Project_ProjectManagerEffectiveId",
				table: "Project",
				column: "ProjectManagerEffectiveId");

			migrationBuilder.CreateIndex(
				name: "IX_Project_ProjectManagerId",
				table: "Project",
				column: "ProjectManagerId");

			migrationBuilder.CreateIndex(
				name: "IX_ProjectRelation_LowerProjectId",
				table: "ProjectRelation",
				column: "LowerProjectId");

			migrationBuilder.CreateIndex(
				name: "IX_TeamMembership_EmployeeId",
				table: "TeamMembership",
				column: "EmployeeId");

			migrationBuilder.CreateIndex(
				name: "IX_TeamMembership_TeamId",
				table: "TeamMembership",
				column: "TeamId");

			migrationBuilder.CreateIndex(
				name: "IX_TimesheetItem_ApprovedById",
				table: "TimesheetItem",
				column: "ApprovedById");

			migrationBuilder.CreateIndex(
				name: "IX_TimesheetItem_EmployeeId",
				table: "TimesheetItem",
				column: "EmployeeId");

			migrationBuilder.CreateIndex(
				name: "IX_TimesheetItem_ProjectId",
				table: "TimesheetItem",
				column: "ProjectId");

			migrationBuilder.CreateIndex(
				name: "IX_TimesheetItem_ProjectPhaseId",
				table: "TimesheetItem",
				column: "ProjectPhaseId");

			migrationBuilder.CreateIndex(
				name: "IX_TimesheetItem_TimesheetItemCategoryId",
				table: "TimesheetItem",
				column: "TimesheetItemCategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_Transaction_AttridaObjectId",
				table: "Transaction",
				column: "AttridaObjectId");

			migrationBuilder.CreateIndex(
				name: "IX_Transaction_BankAccountId",
				table: "Transaction",
				column: "BankAccountId");

			migrationBuilder.CreateIndex(
				name: "IX_Transaction_BusinessPartnerId",
				table: "Transaction",
				column: "BusinessPartnerId");

			migrationBuilder.CreateIndex(
				name: "IX_Transaction_CurrencyId",
				table: "Transaction",
				column: "CurrencyId");

			migrationBuilder.CreateIndex(
				name: "IX_Transaction_DocumentTemplateId",
				table: "Transaction",
				column: "DocumentTemplateId");

			migrationBuilder.CreateIndex(
				name: "IX_Transaction_OurReferenceNumberSequenceId",
				table: "Transaction",
				column: "OurReferenceNumberSequenceId");

			migrationBuilder.CreateIndex(
				name: "IX_Transaction_RelatedTransactionId",
				table: "Transaction",
				column: "RelatedTransactionId");

			migrationBuilder.CreateIndex(
				name: "IX_TransactionItem_ApprovedById",
				table: "TransactionItem",
				column: "ApprovedById");

			migrationBuilder.CreateIndex(
				name: "IX_TransactionItem_ItemTypeId",
				table: "TransactionItem",
				column: "ItemTypeId");

			migrationBuilder.CreateIndex(
				name: "IX_TransactionItem_ProjectId",
				table: "TransactionItem",
				column: "ProjectId");

			migrationBuilder.CreateIndex(
				name: "IX_TransactionItem_ProjectPhaseId",
				table: "TransactionItem",
				column: "ProjectPhaseId");

			migrationBuilder.CreateIndex(
				name: "IX_TransactionItem_TransactionId",
				table: "TransactionItem",
				column: "TransactionId");

			migrationBuilder.CreateIndex(
				name: "IX_UserRole_RoleId",
				table: "UserRole",
				column: "RoleId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "__DataSeed");

			migrationBuilder.DropTable(
				name: "Absence");

			migrationBuilder.DropTable(
				name: "ApplicationSettings");

			migrationBuilder.DropTable(
				name: "AttridaComment");

			migrationBuilder.DropTable(
				name: "AttridaDocument");

			migrationBuilder.DropTable(
				name: "AttridaTag");

			migrationBuilder.DropTable(
				name: "BusinessCase");

			migrationBuilder.DropTable(
				name: "ContactRelationship");

			migrationBuilder.DropTable(
				name: "CountryLocalization");

			migrationBuilder.DropTable(
				name: "DateInfo");

			migrationBuilder.DropTable(
				name: "EmployeeHistory");

			migrationBuilder.DropTable(
				name: "ExchangeRate");

			migrationBuilder.DropTable(
				name: "NumberSequenceUnusedNumber");

			migrationBuilder.DropTable(
				name: "OverheadToPersonalCostsRatio");

			migrationBuilder.DropTable(
				name: "Payment");

			migrationBuilder.DropTable(
				name: "ProjectRelation");

			migrationBuilder.DropTable(
				name: "TeamMembership");

			migrationBuilder.DropTable(
				name: "TimesheetItem");

			migrationBuilder.DropTable(
				name: "TransactionItem");

			migrationBuilder.DropTable(
				name: "UserRole");

			migrationBuilder.DropTable(
				name: "AbsenceType");

			migrationBuilder.DropTable(
				name: "Language");

			migrationBuilder.DropTable(
				name: "EmploymentTerms");

			migrationBuilder.DropTable(
				name: "EmploymentType");

			migrationBuilder.DropTable(
				name: "Employee");

			migrationBuilder.DropTable(
				name: "TimesheetItemCategory");

			migrationBuilder.DropTable(
				name: "TransactionItemType");

			migrationBuilder.DropTable(
				name: "Project");

			migrationBuilder.DropTable(
				name: "ProjectPhase");

			migrationBuilder.DropTable(
				name: "Transaction");

			migrationBuilder.DropTable(
				name: "Role");

			migrationBuilder.DropTable(
				name: "Team");

			migrationBuilder.DropTable(
				name: "User");

			migrationBuilder.DropTable(
				name: "Contact");

			migrationBuilder.DropTable(
				name: "Currency");

			migrationBuilder.DropTable(
				name: "TransactionDocumentTemplate");

			migrationBuilder.DropTable(
				name: "NumberSequence");

			migrationBuilder.DropTable(
				name: "AttridaObject");

			migrationBuilder.DropTable(
				name: "Address");

			migrationBuilder.DropTable(
				name: "BankAccount");

			migrationBuilder.DropTable(
				name: "Country");

			migrationBuilder.DropSequence(
				name: "ContactSequence");

			migrationBuilder.DropSequence(
				name: "ProjectSequence");

			migrationBuilder.DropSequence(
				name: "TeamSequence");
		}
	}
}
