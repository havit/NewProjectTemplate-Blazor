using Havit.Data.Patterns.DataSeeds;
using Havit.NewProjectTemplate.Model.Common;
using Havit.NewProjectTemplate.Model.Localizations;

namespace Havit.NewProjectTemplate.DataLayer.Seeds.Core.Common;

public class CountrySeed : DataSeed<CoreProfile>
{
	public override void SeedData()
	{
		var countries = new[]
		{
				new Country()
				{
					IsoCode = "CZ",
					IsoCode3 = "CZE",
					PhoneCountryCode = "+420",
					UiOrder = 1,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Česká republika" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Czech Republic" }
					}
				},
				new Country()
				{
					IsoCode = "SK",
					IsoCode3 = "SVK",
					PhoneCountryCode = "+421",
					UiOrder = 2,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Slovensko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Slovakia" }
					}
				},
				new Country()
				{
					IsoCode = "AF",
					IsoCode3 = "AFG",
					PhoneCountryCode = "+93",
					UiOrder = 3,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Afghánistán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Afghanistan" }
					}
				},
				new Country()
				{
					IsoCode = "AL",
					IsoCode3 = "ALB",
					PhoneCountryCode = "+355",
					UiOrder = 4,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Albánie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Albania" }
					}
				},
				new Country()
				{
					IsoCode = "DZ",
					IsoCode3 = "DZA",
					PhoneCountryCode = "+213",
					UiOrder = 5,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Alžírsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Algeria" }
					}
				},
				new Country()
				{
					IsoCode = "AS",
					IsoCode3 = "ASM",
					PhoneCountryCode = "+1-684",
					UiOrder = 6,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Americká Samoa" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "American Samoa" }
					}
				},
				new Country()
				{
					IsoCode = "AD",
					IsoCode3 = "AND",
					PhoneCountryCode = "+376",
					UiOrder = 7,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Andorra" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Andorra" }
					}
				},
				new Country()
				{
					IsoCode = "AO",
					IsoCode3 = "AGO",
					PhoneCountryCode = "+244",
					UiOrder = 8,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Angola" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Angola" }
					}
				},
				new Country()
				{
					IsoCode = "AI",
					IsoCode3 = "AIA",
					PhoneCountryCode = "+1-264",
					UiOrder = 9,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Anguilla" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Anguilla" }
					}
				},
				new Country()
				{
					IsoCode = "AG",
					IsoCode3 = "ATG",
					PhoneCountryCode = "+1-268",
					UiOrder = 10,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Antigua a Barbuda" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Antigua and Barbuda" }
					}
				},
				new Country()
				{
					IsoCode = "AR",
					IsoCode3 = "ARG",
					PhoneCountryCode = "+54",
					UiOrder = 11,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Argentina" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Argentina" }
					}
				},
				new Country()
				{
					IsoCode = "AM",
					IsoCode3 = "ARM",
					PhoneCountryCode = "+374",
					UiOrder = 12,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Arménie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Armenia" }
					}
				},
				new Country()
				{
					IsoCode = "AW",
					IsoCode3 = "ABW",
					PhoneCountryCode = "+297",
					UiOrder = 13,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Aruba" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Aruba" }
					}
				},
				new Country()
				{
					IsoCode = "AU",
					IsoCode3 = "AUS",
					PhoneCountryCode = "+61",
					UiOrder = 14,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Austrálie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Australie" }
					}
				},
				new Country()
				{
					IsoCode = "AZ",
					IsoCode3 = "AZE",
					PhoneCountryCode = "+994",
					UiOrder = 15,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Azerbajdžán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Azerbaijan" }
					}
				},
				new Country()
				{
					IsoCode = "BS",
					IsoCode3 = "BHS",
					PhoneCountryCode = "+1-242",
					UiOrder = 16,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Bahamy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Bahamas" }
					}
				},
				new Country()
				{
					IsoCode = "BH",
					IsoCode3 = "BHR",
					PhoneCountryCode = "+973",
					UiOrder = 17,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Bahrajn" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Bahrain" }
					}
				},
				new Country()
				{
					IsoCode = "BD",
					IsoCode3 = "BGD",
					PhoneCountryCode = "+880",
					UiOrder = 18,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Bangladéš" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Bangladesh" }
					}
				},
				new Country()
				{
					IsoCode = "BB",
					IsoCode3 = "BRB",
					PhoneCountryCode = "+1-246",
					UiOrder = 19,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Barbados" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Barbados" }
					}
				},
				new Country()
				{
					IsoCode = "BE",
					IsoCode3 = "BEL",
					PhoneCountryCode = "+32",
					UiOrder = 20,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Belgie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Belgium" }
					}
				},
				new Country()
				{
					IsoCode = "BZ",
					IsoCode3 = "BLZ",
					PhoneCountryCode = "+501",
					UiOrder = 21,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Belize" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Belize" }
					}
				},
				new Country()
				{
					IsoCode = "BY",
					IsoCode3 = "BLR",
					PhoneCountryCode = "+375",
					UiOrder = 22,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Bělorusko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Belarus" }
					}
				},
				new Country()
				{
					IsoCode = "BJ",
					IsoCode3 = "BEN",
					PhoneCountryCode = "+229",
					UiOrder = 23,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Benin" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Benin" }
					}
				},
				new Country()
				{
					IsoCode = "BM",
					IsoCode3 = "BMU",
					PhoneCountryCode = "+1-441",
					UiOrder = 24,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Bermudy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Bermuda" }
					}
				},
				new Country()
				{
					IsoCode = "BT",
					IsoCode3 = "BTN",
					PhoneCountryCode = "+975",
					UiOrder = 25,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Bhútán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Bhutan" }
					}
				},
				new Country()
				{
					IsoCode = "BO",
					IsoCode3 = "BOL",
					PhoneCountryCode = "+591",
					UiOrder = 26,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Bolívie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Bolivia" }
					}
				},
				new Country()
				{
					IsoCode = "BA",
					IsoCode3 = "BIH",
					PhoneCountryCode = "+387",
					UiOrder = 27,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Bosna a Hercegovina" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Bosnia and Herzegovina" }
					}
				},
				new Country()
				{
					IsoCode = "BW",
					IsoCode3 = "BWA",
					PhoneCountryCode = "+267",
					UiOrder = 28,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Botswana" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Botswana" }
					}
				},
				new Country()
				{
					IsoCode = "BV",
					IsoCode3 = "BVT",
					PhoneCountryCode = "",
					UiOrder = 29,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Bouvetův ostrov" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Bouvet Island" }
					}
				},
				new Country()
				{
					IsoCode = "BR",
					IsoCode3 = "BRA",
					PhoneCountryCode = "+55",
					UiOrder = 30,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Brazílie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Brazil" }
					}
				},
				new Country()
				{
					IsoCode = "IO",
					IsoCode3 = "IOT",
					PhoneCountryCode = "+246",
					UiOrder = 31,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Britské indickooceánské úz." },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "British Indian Ocean Terr." }
					}
				},
				new Country()
				{
					IsoCode = "BN",
					IsoCode3 = "BRN",
					PhoneCountryCode = "+673",
					UiOrder = 32,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Brunej Darussalam" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Brunei Darussalam" }
					}
				},
				new Country()
				{
					IsoCode = "BG",
					IsoCode3 = "BGR",
					PhoneCountryCode = "+359",
					UiOrder = 33,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Bulharsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Bulgaria" }
					}
				},
				new Country()
				{
					IsoCode = "BI",
					IsoCode3 = "BDI",
					PhoneCountryCode = "+257",
					UiOrder = 34,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Burundi" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Burundi" }
					}
				},
				new Country()
				{
					IsoCode = "CL",
					IsoCode3 = "CHL",
					PhoneCountryCode = "+56",
					UiOrder = 35,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Chile" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Chile" }
					}
				},
				new Country()
				{
					IsoCode = "HR",
					IsoCode3 = "HRV",
					PhoneCountryCode = "+385",
					UiOrder = 36,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Chorvatsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Croatia" }
					}
				},
				new Country()
				{
					IsoCode = "CK",
					IsoCode3 = "COK",
					PhoneCountryCode = "+682",
					UiOrder = 37,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Cookovy ostrovy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Cook Islands" }
					}
				},
				new Country()
				{
					IsoCode = "TD",
					IsoCode3 = "TCD",
					PhoneCountryCode = "+235",
					UiOrder = 38,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Čad" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Chad" }
					}
				},
				new Country()
				{
					IsoCode = "ME",
					IsoCode3 = "MNE",
					PhoneCountryCode = "+248",
					UiOrder = 39,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Černá Hora" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Montenegro" }
					}
				},
				new Country()
				{
					IsoCode = "CN",
					IsoCode3 = "CHN",
					PhoneCountryCode = "+86",
					UiOrder = 40,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Čína" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "China" }
					}
				},
				new Country()
				{
					IsoCode = "DK",
					IsoCode3 = "DNK",
					PhoneCountryCode = "+45",
					UiOrder = 41,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Dánsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Denmark" }
					}
				},
				new Country()
				{
					IsoCode = "DO",
					IsoCode3 = "DOM",
					PhoneCountryCode = "+1-767",
					UiOrder = 42,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Dominikánská republika" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Dominican Republic" }
					}
				},
				new Country()
				{
					IsoCode = "DJ",
					IsoCode3 = "DJI",
					PhoneCountryCode = "+253",
					UiOrder = 43,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Džibuti" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Djibouti" }
					}
				},
				new Country()
				{
					IsoCode = "EG",
					IsoCode3 = "EGY",
					PhoneCountryCode = "+20",
					UiOrder = 44,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Egypt" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Egypt" }
					}
				},
				new Country()
				{
					IsoCode = "EC",
					IsoCode3 = "ECU",
					PhoneCountryCode = "+593",
					UiOrder = 45,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Ekvádor" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Ecuador" }
					}
				},
				new Country()
				{
					IsoCode = "ER",
					IsoCode3 = "ERI",
					PhoneCountryCode = "+291",
					UiOrder = 46,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Eritrea" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Eritrea" }
					}
				},
				new Country()
				{
					IsoCode = "EE",
					IsoCode3 = "EST",
					PhoneCountryCode = "+372",
					UiOrder = 47,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Estonsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Estonia" }
					}
				},
				new Country()
				{
					IsoCode = "ET",
					IsoCode3 = "ETH",
					PhoneCountryCode = "+251",
					UiOrder = 48,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Etiopie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Ethiopia" }
					}
				},
				new Country()
				{
					IsoCode = "FO",
					IsoCode3 = "FRO",
					PhoneCountryCode = "+298",
					UiOrder = 49,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Faerské ostrovy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Faroe Islands" }
					}
				},
				new Country()
				{
					IsoCode = "FK",
					IsoCode3 = "FLK",
					PhoneCountryCode = "+500",
					UiOrder = 50,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Falklandské ostrovy (Malvíny)" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Falkland Islands (Malvina)" }
					}
				},
				new Country()
				{
					IsoCode = "FJ",
					IsoCode3 = "FJI",
					PhoneCountryCode = "+679",
					UiOrder = 51,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Fidži" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Fiji" }
					}
				},
				new Country()
				{
					IsoCode = "PH",
					IsoCode3 = "PHL",
					PhoneCountryCode = "+63",
					UiOrder = 52,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Filipíny" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Philippines" }
					}
				},
				new Country()
				{
					IsoCode = "FI",
					IsoCode3 = "FIN",
					PhoneCountryCode = "+358",
					UiOrder = 53,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Finsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Finland" }
					}
				},
				new Country()
				{
					IsoCode = "FR",
					IsoCode3 = "FRA",
					PhoneCountryCode = "+33",
					UiOrder = 54,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Francie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "France" }
					}
				},
				new Country()
				{
					IsoCode = "GF",
					IsoCode3 = "GUF",
					PhoneCountryCode = "+594",
					UiOrder = 55,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Francouzská Guayana" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "French Guiana" }
					}
				},
				new Country()
				{
					IsoCode = "PF",
					IsoCode3 = "PYF",
					PhoneCountryCode = "+689",
					UiOrder = 56,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Francouzská Polynésie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "French Polynesia" }
					}
				},
				new Country()
				{
					IsoCode = "GA",
					IsoCode3 = "GAB",
					PhoneCountryCode = "+241",
					UiOrder = 57,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Gabon" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Gabon" }
					}
				},
				new Country()
				{
					IsoCode = "GM",
					IsoCode3 = "GMB",
					PhoneCountryCode = "+220",
					UiOrder = 58,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Gambie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Gambia" }
					}
				},
				new Country()
				{
					IsoCode = "GH",
					IsoCode3 = "GHA",
					PhoneCountryCode = "+233",
					UiOrder = 59,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Ghana" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Ghana" }
					}
				},
				new Country()
				{
					IsoCode = "GI",
					IsoCode3 = "GIB",
					PhoneCountryCode = "+350",
					UiOrder = 60,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Gibraltar" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Gibraltar" }
					}
				},
				new Country()
				{
					IsoCode = "GD",
					IsoCode3 = "GRD",
					PhoneCountryCode = "+1-473",
					UiOrder = 61,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Grenada" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Grenada" }
					}
				},
				new Country()
				{
					IsoCode = "GL",
					IsoCode3 = "GRL",
					PhoneCountryCode = "+299",
					UiOrder = 62,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Grónsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Greenland" }
					}
				},
				new Country()
				{
					IsoCode = "GE",
					IsoCode3 = "GEO",
					PhoneCountryCode = "+995",
					UiOrder = 63,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Gruzie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Georgia" }
					}
				},
				new Country()
				{
					IsoCode = "GP",
					IsoCode3 = "GLP",
					PhoneCountryCode = "+590",
					UiOrder = 64,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Guadeloupe" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Guadeloupe" }
					}
				},
				new Country()
				{
					IsoCode = "GU",
					IsoCode3 = "GUM",
					PhoneCountryCode = "+1-671",
					UiOrder = 65,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Guam" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Guam" }
					}
				},
				new Country()
				{
					IsoCode = "GT",
					IsoCode3 = "GTM",
					PhoneCountryCode = "+502",
					UiOrder = 66,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Guatemala" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Guatemala" }
					}
				},
				new Country()
				{
					IsoCode = "GN",
					IsoCode3 = "GIN",
					PhoneCountryCode = "+224",
					UiOrder = 67,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Guinea" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Guinea" }
					}
				},
				new Country()
				{
					IsoCode = "GW",
					IsoCode3 = "GNB",
					PhoneCountryCode = "+245",
					UiOrder = 68,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Guinea-Bissau" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Guinea-Bissau" }
					}
				},
				new Country()
				{
					IsoCode = "GY",
					IsoCode3 = "GUY",
					PhoneCountryCode = "+592",
					UiOrder = 69,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Guyana" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Guyana" }
					}
				},
				new Country()
				{
					IsoCode = "HT",
					IsoCode3 = "HTI",
					PhoneCountryCode = "+509",
					UiOrder = 70,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Haiti" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Haiti" }
					}
				},
				new Country()
				{
					IsoCode = "HM",
					IsoCode3 = "HMD",
					PhoneCountryCode = "",
					UiOrder = 71,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Heardův ostrov a McDonaldovy ostrovy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Heard and McDonald Islands" }
					}
				},
				new Country()
				{
					IsoCode = "HN",
					IsoCode3 = "HND",
					PhoneCountryCode = "+504",
					UiOrder = 72,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Honduras" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Honduras" }
					}
				},
				new Country()
				{
					IsoCode = "HK",
					IsoCode3 = "HKG",
					PhoneCountryCode = "+852",
					UiOrder = 73,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Hong Kong" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Hong Kong" }
					}
				},
				new Country()
				{
					IsoCode = "IN",
					IsoCode3 = "IND",
					PhoneCountryCode = "+91",
					UiOrder = 74,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Indie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "India" }
					}
				},
				new Country()
				{
					IsoCode = "ID",
					IsoCode3 = "IDN",
					PhoneCountryCode = "+62",
					UiOrder = 75,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Indonésie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Indonesia" }
					}
				},
				new Country()
				{
					IsoCode = "IQ",
					IsoCode3 = "IRQ",
					PhoneCountryCode = "+964",
					UiOrder = 76,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Irák" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Irak" }
					}
				},
				new Country()
				{
					IsoCode = "IR",
					IsoCode3 = "IRN",
					PhoneCountryCode = "+98",
					UiOrder = 77,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Irán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Iran" }
					}
				},
				new Country()
				{
					IsoCode = "IE",
					IsoCode3 = "IRL",
					PhoneCountryCode = "+353",
					UiOrder = 78,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Irsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Ireland" }
					}
				},
				new Country()
				{
					IsoCode = "IS",
					IsoCode3 = "ISL",
					PhoneCountryCode = "+354",
					UiOrder = 79,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Island" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Iceland" }
					}
				},
				new Country()
				{
					IsoCode = "IT",
					IsoCode3 = "ITA",
					PhoneCountryCode = "+39",
					UiOrder = 80,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Itálie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Italy" }
					}
				},
				new Country()
				{
					IsoCode = "IL",
					IsoCode3 = "ISR",
					PhoneCountryCode = "+972",
					UiOrder = 81,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Izrael" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Israel" }
					}
				},
				new Country()
				{
					IsoCode = "JM",
					IsoCode3 = "JAM",
					PhoneCountryCode = "+1-876",
					UiOrder = 82,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Jamajka" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Jamaica" }
					}
				},
				new Country()
				{
					IsoCode = "JP",
					IsoCode3 = "JPN",
					PhoneCountryCode = "+81",
					UiOrder = 83,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Japonsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Japan" }
					}
				},
				new Country()
				{
					IsoCode = "YE",
					IsoCode3 = "YEM",
					PhoneCountryCode = "+967",
					UiOrder = 84,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Jemen" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Yemen" }
					}
				},
				new Country()
				{
					IsoCode = "ZA",
					IsoCode3 = "ZAF",
					PhoneCountryCode = "+27",
					UiOrder = 85,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Jihoafrická republika" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "South Africa" }
					}
				},
				new Country()
				{
					IsoCode = "GS",
					IsoCode3 = "SGS",
					PhoneCountryCode = "",
					UiOrder = 86,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "J. Geogie a J. Sandwichovy ostrovy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "S. Georgia and S. Sandwitch Islands" }
					}
				},
				new Country()
				{
					IsoCode = "JO",
					IsoCode3 = "JOR",
					PhoneCountryCode = "+962",
					UiOrder = 87,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Jordánsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Jordan" }
					}
				},
				new Country()
				{
					IsoCode = "KY",
					IsoCode3 = "CYM",
					PhoneCountryCode = "+1-345",
					UiOrder = 88,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kajmanské ostrovy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Cayman Islands" }
					}
				},
				new Country()
				{
					IsoCode = "KH",
					IsoCode3 = "KHM",
					PhoneCountryCode = "+855",
					UiOrder = 89,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kambodža" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Cambodia" }
					}
				},
				new Country()
				{
					IsoCode = "CM",
					IsoCode3 = "CMR",
					PhoneCountryCode = "+237",
					UiOrder = 90,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kamerun" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Cameroon" }
					}
				},
				new Country()
				{
					IsoCode = "CA",
					IsoCode3 = "CAN",
					PhoneCountryCode = "+1",
					UiOrder = 91,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kanada" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Canada" }
					}
				},
				new Country()
				{
					IsoCode = "CV",
					IsoCode3 = "CPV",
					PhoneCountryCode = "+238",
					UiOrder = 92,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kapverdy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Cape Verde" }
					}
				},
				new Country()
				{
					IsoCode = "QA",
					IsoCode3 = "QAT",
					PhoneCountryCode = "+974",
					UiOrder = 93,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Katar" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Qatar" }
					}
				},
				new Country()
				{
					IsoCode = "KZ",
					IsoCode3 = "KAZ",
					PhoneCountryCode = "+7",
					UiOrder = 94,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kazachstán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Kazakhstan" }
					}
				},
				new Country()
				{
					IsoCode = "KE",
					IsoCode3 = "KEN",
					PhoneCountryCode = "+254",
					UiOrder = 95,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Keňa" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Kenya" }
					}
				},
				new Country()
				{
					IsoCode = "KI",
					IsoCode3 = "KIR",
					PhoneCountryCode = "+686",
					UiOrder = 96,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kiribati" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Kiribati" }
					}
				},
				new Country()
				{
					IsoCode = "CC",
					IsoCode3 = "CCK",
					PhoneCountryCode = "+61",
					UiOrder = 97,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kokosové (Keelingovy) ostrovy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Cocos (Keeling) Islands" }
					}
				},
				new Country()
				{
					IsoCode = "CO",
					IsoCode3 = "COL",
					PhoneCountryCode = "+57",
					UiOrder = 98,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kolumbie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Colombia" }
					}
				},
				new Country()
				{
					IsoCode = "KM",
					IsoCode3 = "COM",
					PhoneCountryCode = "+269",
					UiOrder = 99,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Komory" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Comoros" }
					}
				},
				new Country()
				{
					IsoCode = "CD",
					IsoCode3 = "COD",
					PhoneCountryCode = "+242",
					UiOrder = 100,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kongo, demokratická republika" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Congo, Democratic People's Rep" }
					}
				},
				new Country()
				{
					IsoCode = "CG",
					IsoCode3 = "COG",
					PhoneCountryCode = "+242",
					UiOrder = 101,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Konžská republika" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Congo, Republic of" }
					}
				},
				new Country()
				{
					IsoCode = "KP",
					IsoCode3 = "PRK",
					PhoneCountryCode = "+850",
					UiOrder = 102,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Korea (KLDR)" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Korea (DPRK)" }
					}
				},
				new Country()
				{
					IsoCode = "KR",
					IsoCode3 = "KOR",
					PhoneCountryCode = "+82",
					UiOrder = 103,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Korejská republika" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Korea, Republic of" }
					}
				},
				new Country()
				{
					IsoCode = "CR",
					IsoCode3 = "CRI",
					PhoneCountryCode = "+506",
					UiOrder = 104,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kostarika" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Costa Rica" }
					}
				},
				new Country()
				{
					IsoCode = "CU",
					IsoCode3 = "CUB",
					PhoneCountryCode = "+53",
					UiOrder = 105,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kuba" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Cuba" }
					}
				},
				new Country()
				{
					IsoCode = "KW",
					IsoCode3 = "KWT",
					PhoneCountryCode = "+965",
					UiOrder = 106,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kuvajt" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Kuwait" }
					}
				},
				new Country()
				{
					IsoCode = "CY",
					IsoCode3 = "CYP",
					PhoneCountryCode = "+357",
					UiOrder = 107,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kypr" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Cyprus" }
					}
				},
				new Country()
				{
					IsoCode = "KG",
					IsoCode3 = "KGZ",
					PhoneCountryCode = "+996",
					UiOrder = 108,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Kyrgyzstán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Kyrgyzstan" },
					}
				},
				new Country()
				{
					IsoCode = "LA",
					IsoCode3 = "LAO",
					PhoneCountryCode = "+856",
					UiOrder = 109,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Laoská lidově demokratická republika" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Lao People's Democratic Rep" }
					}
				},
				new Country()
				{
					IsoCode = "LS",
					IsoCode3 = "LSO",
					PhoneCountryCode = "+266",
					UiOrder = 110,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Lesotho" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Lesotho" }
					}
				},
				new Country()
				{
					IsoCode = "LB",
					IsoCode3 = "LBN",
					PhoneCountryCode = "+961",
					UiOrder = 111,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Libanon" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Lebanon" }
					}
				},
				new Country()
				{
					IsoCode = "LR",
					IsoCode3 = "LBR",
					PhoneCountryCode = "+231",
					UiOrder = 112,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Libérie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Liberia" }
					}
				},
				new Country()
				{
					IsoCode = "LY",
					IsoCode3 = "LBY",
					PhoneCountryCode = "+218",
					UiOrder = 113,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Libyjská arabská džamáhírije" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Libyan Arab Jamahiriya" }
					}
				},
				new Country()
				{
					IsoCode = "LI",
					IsoCode3 = "LIE",
					PhoneCountryCode = "+423",
					UiOrder = 114,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Lichtenštejnsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Liechtenstein" }
					}
				},
				new Country()
				{
					IsoCode = "LT",
					IsoCode3 = "LTU",
					PhoneCountryCode = "+370",
					UiOrder = 115,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Litva" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Lithuania" }
					}
				},
				new Country()
				{
					IsoCode = "LV",
					IsoCode3 = "LVA",
					PhoneCountryCode = "+371",
					UiOrder = 116,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Lotyšsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Latvia" }
					}
				},
				new Country()
				{
					IsoCode = "LU",
					IsoCode3 = "LUX",
					PhoneCountryCode = "+352",
					UiOrder = 117,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Lucembursko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Luxembourg" }
					}
				},
				new Country()
				{
					IsoCode = "MO",
					IsoCode3 = "MAC",
					PhoneCountryCode = "+853",
					UiOrder = 118,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Macao" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Macau" }
					}
				},
				new Country()
				{
					IsoCode = "MG",
					IsoCode3 = "MDG",
					PhoneCountryCode = "+261",
					UiOrder = 119,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Madagaskar" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Madagascar" }
					}
				},
				new Country()
				{
					IsoCode = "HU",
					IsoCode3 = "HUN",
					PhoneCountryCode = "+36",
					UiOrder = 120,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Maďarsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Hungary" }
					}
				},
				new Country()
				{
					IsoCode = "MK",
					IsoCode3 = "MKD",
					PhoneCountryCode = "+382",
					UiOrder = 121,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Makedonie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Macedonia" }
					}
				},
				new Country()
				{
					IsoCode = "MY",
					IsoCode3 = "MYS",
					PhoneCountryCode = "+60",
					UiOrder = 122,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Malajsie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Malaysia" }
					}
				},
				new Country()
				{
					IsoCode = "MW",
					IsoCode3 = "MWI",
					PhoneCountryCode = "+265",
					UiOrder = 123,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Malawi" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Malawi" }
					}
				},
				new Country()
				{
					IsoCode = "MV",
					IsoCode3 = "MDV",
					PhoneCountryCode = "+960",
					UiOrder = 124,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Maledivy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Maldives" }
					}
				},
				new Country()
				{
					IsoCode = "ML",
					IsoCode3 = "MLI",
					PhoneCountryCode = "+223",
					UiOrder = 125,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Mali" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Mali" }
					}
				},
				new Country()
				{
					IsoCode = "MT",
					IsoCode3 = "MLT",
					PhoneCountryCode = "+356",
					UiOrder = 126,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Malta" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Malta" }
					}
				},
				new Country()
				{
					IsoCode = "MA",
					IsoCode3 = "MAR",
					PhoneCountryCode = "+212",
					UiOrder = 127,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Maroko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Morocco" }
					}
				},

				new Country()
				{
					IsoCode = "MH",
					IsoCode3 = "MHL",
					PhoneCountryCode = "+692",
					UiOrder = 128,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Marshallovy ostrovy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Marshall Islands" }
					}
				},
				new Country()
				{
					IsoCode = "MQ",
					IsoCode3 = "MTQ",
					PhoneCountryCode = "+596",
					UiOrder = 129,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Martinik" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Martinique" }
					}
				},
				new Country()
				{
					IsoCode = "MU",
					IsoCode3 = "MUS",
					PhoneCountryCode = "+230",
					UiOrder = 130,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Mauricius" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Mauritius" }
					}
				},
				new Country()
				{
					IsoCode = "MR",
					IsoCode3 = "MRT",
					PhoneCountryCode = "+222",
					UiOrder = 131,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Mauretánie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Mauritania" }
					}
				},
				new Country()
				{
					IsoCode = "YT",
					IsoCode3 = "MYT",
					PhoneCountryCode = "+262",
					UiOrder = 132,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Mayotte" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Mayotte" }
					}
				},
				new Country()
				{
					IsoCode = "MX",
					IsoCode3 = "MEX",
					PhoneCountryCode = "+52",
					UiOrder = 133,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Mexiko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Mexico" }
					}
				},
				new Country()
				{
					IsoCode = "FM",
					IsoCode3 = "FSM",
					PhoneCountryCode = "+691",
					UiOrder = 134,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Mikronésie, federativní státy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Micronesia, Federal State of" }
					}
				},
				new Country()
				{
					IsoCode = "MD",
					IsoCode3 = "MDA",
					PhoneCountryCode = "+373",
					UiOrder = 135,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Moldávie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Moldova" }
					}
				},
				new Country()
				{
					IsoCode = "MC",
					IsoCode3 = "MCO",
					PhoneCountryCode = "+377",
					UiOrder = 136,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Monako" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Monaco" }
					}
				},
				new Country()
				{
					IsoCode = "MN",
					IsoCode3 = "MNG",
					PhoneCountryCode = "+976",
					UiOrder = 137,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Mongolsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Mongolia" }
					}
				},
				new Country()
				{
					IsoCode = "MS",
					IsoCode3 = "MSR",
					PhoneCountryCode = "+1-664",
					UiOrder = 138,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Montserrat" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Montserrat" }
					}
				},
				new Country()
				{
					IsoCode = "MZ",
					IsoCode3 = "MOZ",
					PhoneCountryCode = "+258",
					UiOrder = 139,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Mozambik" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Mozambique" }
					}
				},
				new Country()
				{
					IsoCode = "MM",
					IsoCode3 = "MMR",
					PhoneCountryCode = "+95",
					UiOrder = 140,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Myanmar (Barma)" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Myanmar (Barma)" }
					}
				},
				new Country()
				{
					IsoCode = "NA",
					IsoCode3 = "NAM",
					PhoneCountryCode = "+264",
					UiOrder = 141,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Namibie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Namibia" }
					}
				},
				new Country()
				{
					IsoCode = "NR",
					IsoCode3 = "NRU",
					PhoneCountryCode = "+674",
					UiOrder = 142,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Nauru" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Nauru" }
					}
				},
				new Country()
				{
					IsoCode = "DE",
					IsoCode3 = "DEU",
					PhoneCountryCode = "+49",
					UiOrder = 143,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Německo" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Germany" }
					}
				},
				new Country()
				{
					IsoCode = "NP",
					IsoCode3 = "NPL",
					PhoneCountryCode = "+977",
					UiOrder = 144,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Nepál" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Nepal" }
					}
				},
				new Country()
				{
					IsoCode = "NE",
					IsoCode3 = "NER",
					PhoneCountryCode = "+227",
					UiOrder = 145,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Niger" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Niger" }
					}
				},
				new Country()
				{
					IsoCode = "NG",
					IsoCode3 = "NGA",
					PhoneCountryCode = "+234",
					UiOrder = 146,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Nigérie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Nigeria" }
					}
				},
				new Country()
				{
					IsoCode = "NI",
					IsoCode3 = "NIC",
					PhoneCountryCode = "+505",
					UiOrder = 147,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Nikaragua" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Nicaragua" }
					}
				},
				new Country()
				{
					IsoCode = "NU",
					IsoCode3 = "NIU",
					PhoneCountryCode = "+683",
					UiOrder = 148,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Niue" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Niue" }
					}
				},
				new Country()
				{
					IsoCode = "NL",
					IsoCode3 = "NLD",
					PhoneCountryCode = "+31",
					UiOrder = 149,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Nizozemí" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Netherlands" }
					}
				},
				new Country()
				{
					IsoCode = "NO",
					IsoCode3 = "NOR",
					PhoneCountryCode = "+47",
					UiOrder = 150,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Norsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Norway" }
					}
				},
				new Country()
				{
					IsoCode = "NC",
					IsoCode3 = "NCL",
					PhoneCountryCode = "+687",
					UiOrder = 151,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Nová Kaledonie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "New Caledonia" }
					}
				},
				new Country()
				{
					IsoCode = "NZ",
					IsoCode3 = "NZL",
					PhoneCountryCode = "+64",
					UiOrder = 152,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Nový Zéland" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "New Zealand" }
					}
				},
				new Country()
				{
					IsoCode = "OM",
					IsoCode3 = "OMN",
					PhoneCountryCode = "+968",
					UiOrder = 153,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Omán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Oman" }
					}
				},
				new Country()
				{
					IsoCode = "IM",
					IsoCode3 = "IMN",
					PhoneCountryCode = "+95",
					UiOrder = 154,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Ostrov Man" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Isle of Man" }
					}
				},
				new Country()
				{
					IsoCode = "NF",
					IsoCode3 = "NFK",
					PhoneCountryCode = "+672",
					UiOrder = 155,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Ostrov Norfolk" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Norfolk Island" }
					}
				},
				new Country()
				{
					IsoCode = "MP",
					IsoCode3 = "MNP",
					PhoneCountryCode = "+1-670",
					UiOrder = 156,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Ostrovy Severní Mariany" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Northern Mariana Islands" }
					}
				},
				new Country()
				{
					IsoCode = "TC",
					IsoCode3 = "TCA",
					PhoneCountryCode = "+1-649",
					UiOrder = 157,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Turks a Ciacos ostrovy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Turks and Ciacos Islands" }
					}
				},
				new Country()
				{
					IsoCode = "PK",
					IsoCode3 = "PAK",
					PhoneCountryCode = "+92",
					UiOrder = 158,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Pakistán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Pakistan" }
					}
				},
				new Country()
				{
					IsoCode = "PW",
					IsoCode3 = "PLW",
					PhoneCountryCode = "+680",
					UiOrder = 159,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Palau" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Palau" }
					}
				},
				new Country()
				{
					IsoCode = "PS",
					IsoCode3 = "PSE",
					PhoneCountryCode = "+970",
					UiOrder = 160,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Palestinské území (okupované)" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Palestina" }
					}
				},
				new Country()
				{
					IsoCode = "PA",
					IsoCode3 = "PAN",
					PhoneCountryCode = "+507",
					UiOrder = 161,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Panama" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Panama" }
					}
				},
				new Country()
				{
					IsoCode = "PG",
					IsoCode3 = "PNG",
					PhoneCountryCode = "+675",
					UiOrder = 162,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Papua Nová Guinea" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Papua New Guinea" }
					}
				},
				new Country()
				{
					IsoCode = "PY",
					IsoCode3 = "PRY",
					PhoneCountryCode = "+595",
					UiOrder = 163,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Paraguay" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Paraguay" }
					}
				},
				new Country()
				{
					IsoCode = "PE",
					IsoCode3 = "PER",
					PhoneCountryCode = "+51",
					UiOrder = 164,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Peru" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Peru" }
					}
				},
				new Country()
				{
					IsoCode = "PN",
					IsoCode3 = "PCN",
					PhoneCountryCode = "",
					UiOrder = 165,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Pitcairn" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Pitcairn Island" }
					}
				},
				new Country()
				{
					IsoCode = "CI",
					IsoCode3 = "CIV",
					PhoneCountryCode = "+225",
					UiOrder = 166,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Pobřeží slonoviny" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Cote d'Ivoire" }
					}
				},
				new Country()
				{
					IsoCode = "PL",
					IsoCode3 = "POL",
					PhoneCountryCode = "+48",
					UiOrder = 167,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Polsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Poland" }
					}
				},
				new Country()
				{
					IsoCode = "PR",
					IsoCode3 = "PRI",
					PhoneCountryCode = "+1-787",
					UiOrder = 168,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Portoriko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Puerto Rico" }
					}
				},
				new Country()
				{
					IsoCode = "PT",
					IsoCode3 = "PRT",
					PhoneCountryCode = "+351",
					UiOrder = 169,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Portugalsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Portugal" }
					}
				},
				new Country()
				{
					IsoCode = "AT",
					IsoCode3 = "AUT",
					PhoneCountryCode = "+43",
					UiOrder = 170,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Rakousko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Austria" }
					}
				},
				new Country()
				{
					IsoCode = "RE",
					IsoCode3 = "REU",
					PhoneCountryCode = "+262",
					UiOrder = 171,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Réunion" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Reunion Island" }
					}
				},
				new Country()
				{
					IsoCode = "GQ",
					IsoCode3 = "GNQ",
					PhoneCountryCode = "+240",
					UiOrder = 172,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Rovníková Guinea" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Equatorial Guinea" }
					}
				},
				new Country()
				{
					IsoCode = "RO",
					IsoCode3 = "ROU",
					PhoneCountryCode = "+40",
					UiOrder = 173,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Rumunsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Romania" }
					}
				},
				new Country()
				{
					IsoCode = "RU",
					IsoCode3 = "RUS",
					PhoneCountryCode = "+7",
					UiOrder = 174,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Ruská federace" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Russian Federation" }
					}
				},
				new Country()
				{
					IsoCode = "RW",
					IsoCode3 = "RWA",
					PhoneCountryCode = "+250",
					UiOrder = 175,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Rwanda" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Rwanda" }
					}
				},
				new Country()
				{
					IsoCode = "GR",
					IsoCode3 = "GRC",
					PhoneCountryCode = "+30",
					UiOrder = 176,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Řecko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Greece" }
					}
				},
				new Country()
				{
					IsoCode = "PM",
					IsoCode3 = "SPM",
					PhoneCountryCode = "+508",
					UiOrder = 177,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "St. Pierre a Miquelon" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "St. Pierre and Miquelon" }
					}
				},
				new Country()
				{
					IsoCode = "SV",
					IsoCode3 = "SLV",
					PhoneCountryCode = "+503",
					UiOrder = 178,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Salvador" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "El Salvador" }
					}
				},
				new Country()
				{
					IsoCode = "WS",
					IsoCode3 = "WSM",
					PhoneCountryCode = "+685",
					UiOrder = 179,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Západní Samoa" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Western Samoa" }
					}
				},
				new Country()
				{
					IsoCode = "SM",
					IsoCode3 = "SMR",
					PhoneCountryCode = "+378",
					UiOrder = 180,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "San Marino" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "San Marino" }
					}
				},
				new Country()
				{
					IsoCode = "SA",
					IsoCode3 = "SAU",
					PhoneCountryCode = "+966",
					UiOrder = 181,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Saúdská Arábie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Saudi Arabia" }
					}
				},
				new Country()
				{
					IsoCode = "SN",
					IsoCode3 = "SEN",
					PhoneCountryCode = "+221",
					UiOrder = 182,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Senegal" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Senegal" }
					}
				},
				new Country()
				{
					IsoCode = "SC",
					IsoCode3 = "SYC",
					PhoneCountryCode = "+248",
					UiOrder = 183,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Seychely" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Seychelles" }
					}
				},
				new Country()
				{
					IsoCode = "SL",
					IsoCode3 = "SLE",
					PhoneCountryCode = "+232",
					UiOrder = 184,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Sierra Leone" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Sierra Leone" }
					}
				},
				new Country()
				{
					IsoCode = "SG",
					IsoCode3 = "SGP",
					PhoneCountryCode = "+65",
					UiOrder = 185,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Singapur" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Singapore" }
					}
				},
				new Country()
				{
					IsoCode = "SI",
					IsoCode3 = "SVN",
					PhoneCountryCode = "+386",
					UiOrder = 186,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Slovinsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Slovenia" }
					}
				},
				new Country()
				{
					IsoCode = "SO",
					IsoCode3 = "SOM",
					PhoneCountryCode = "+252",
					UiOrder = 187,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Somálsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Somalia" }
					}
				},
				new Country()
				{
					IsoCode = "AE",
					IsoCode3 = "ARE",
					PhoneCountryCode = "+971",
					UiOrder = 188,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Spojené arabské emiráty" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "United Arab Emirates" }
					}
				},
				new Country()
				{
					IsoCode = "US",
					IsoCode3 = "USA",
					PhoneCountryCode = "+1",
					UiOrder = 189,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Spojené státy americké" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "United States of America" }
					}
				},
				new Country()
				{
					IsoCode = "RS",
					IsoCode3 = "SRB",
					PhoneCountryCode = "+248",
					UiOrder = 190,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Srbsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Serbia" }
					}
				},
				new Country()
				{
					IsoCode = "LK",
					IsoCode3 = "LKA",
					PhoneCountryCode = "+94",
					UiOrder = 191,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Srí Lanka" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Sri Lanka" }
					}
				},
				new Country()
				{
					IsoCode = "CF",
					IsoCode3 = "CAF",
					PhoneCountryCode = "+236",
					UiOrder = 192,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Středoafrická republika" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Central African Republic" }
					}
				},
				new Country()
				{
					IsoCode = "SD",
					IsoCode3 = "SDN",
					PhoneCountryCode = "+249",
					UiOrder = 193,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Súdán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Sudan" }
					}
				},
				new Country()
				{
					IsoCode = "SR",
					IsoCode3 = "SUR",
					PhoneCountryCode = "+597",
					UiOrder = 194,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Surinam" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Suriname" }
					}
				},
				new Country()
				{
					IsoCode = "SJ",
					IsoCode3 = "SJM",
					PhoneCountryCode = "+47",
					UiOrder = 195,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Svalbard a ostrov Jan Mayen" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Svalbard and Jan Mayen Is." }
					}
				},
				new Country()
				{
					IsoCode = "SH",
					IsoCode3 = "SHN",
					PhoneCountryCode = "+290",
					UiOrder = 196,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Svatá Helena, Ascension a Tristan da Cunha" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Saint Helena, Ascension and Tristan da Cunha" }
					}
				},
				new Country()
				{
					IsoCode = "LC",
					IsoCode3 = "LCA",
					PhoneCountryCode = "+1-758",
					UiOrder = 197,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Svatá Lucie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Saint Lucia" }
					}
				},
				new Country()
				{
					IsoCode = "KN",
					IsoCode3 = "KNA",
					PhoneCountryCode = "+1-869",
					UiOrder = 198,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Svatý Kryštof a Nevis" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Saint Kitts and Nevis" }
					}
				},
				new Country()
				{
					IsoCode = "ST",
					IsoCode3 = "STP",
					PhoneCountryCode = "+239",
					UiOrder = 199,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Svatý Tomáš a Princův ostrov" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Sao Tome and Principe" }
					}
				},
				new Country()
				{
					IsoCode = "VC",
					IsoCode3 = "VCT",
					PhoneCountryCode = "+1-784",
					UiOrder = 200,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Svatý Vincenc a Grenadiny" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Saint Vincent and the Grenad." }
					}
				},
				new Country()
				{
					IsoCode = "SZ",
					IsoCode3 = "SWZ",
					PhoneCountryCode = "+268",
					UiOrder = 201,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Svazijsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Swaziland" }
					}
				},
				new Country()
				{
					IsoCode = "SY",
					IsoCode3 = "SYR",
					PhoneCountryCode = "+963",
					UiOrder = 202,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Syrská arabská republika" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Syria" }
					}
				},
				new Country()
				{
					IsoCode = "SB",
					IsoCode3 = "SLB",
					PhoneCountryCode = "+677",
					UiOrder = 203,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Šalamounovy ostrovy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Solomon Islands" }
					}
				},
				new Country()
				{
					IsoCode = "ES",
					IsoCode3 = "ESP",
					PhoneCountryCode = "+34",
					UiOrder = 204,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Španělsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Spain" }
					}
				},
				new Country()
				{
					IsoCode = "SE",
					IsoCode3 = "SWE",
					PhoneCountryCode = "+46",
					UiOrder = 205,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Švédsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Sweden" }
					}
				},
				new Country()
				{
					IsoCode = "CH",
					IsoCode3 = "CHE",
					PhoneCountryCode = "+41",
					UiOrder = 206,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Švýcarsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Switzerland" }
					}
				},
				new Country()
				{
					IsoCode = "TJ",
					IsoCode3 = "TJK",
					PhoneCountryCode = "+992",
					UiOrder = 207,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Tádžikistán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Tajikistan" }
					}
				},
				new Country()
				{
					IsoCode = "TZ",
					IsoCode3 = "TZA",
					PhoneCountryCode = "+255",
					UiOrder = 208,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Tanzánie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Tanzania" }
					}
				},
				new Country()
				{
					IsoCode = "TW",
					IsoCode3 = "TWN",
					PhoneCountryCode = "+886",
					UiOrder = 209,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Tchaj-wan (čínská provincie)" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Taiwan" }
					}
				},
				new Country()
				{
					IsoCode = "TH",
					IsoCode3 = "THA",
					PhoneCountryCode = "+66",
					UiOrder = 210,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Thajsko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Thailand" }
					}
				},
				new Country()
				{
					IsoCode = "TG",
					IsoCode3 = "TGO",
					PhoneCountryCode = "+228",
					UiOrder = 211,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Togo" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Togo" }
					}
				},
				new Country()
				{
					IsoCode = "TK",
					IsoCode3 = "TKL",
					PhoneCountryCode = "+690",
					UiOrder = 212,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Tokelau" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Tokelau" }
					}
				},
				new Country()
				{
					IsoCode = "TO",
					IsoCode3 = "TON",
					PhoneCountryCode = "+676",
					UiOrder = 213,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Tonga" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Tonga" }
					}
				},
				new Country()
				{
					IsoCode = "TT",
					IsoCode3 = "TTO",
					PhoneCountryCode = "+1-868",
					UiOrder = 214,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Trinidad a Tobago" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Trinidad and Tobago" }
					}
				},
				new Country()
				{
					IsoCode = "TN",
					IsoCode3 = "TUN",
					PhoneCountryCode = "+216",
					UiOrder = 215,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Tunisko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Tunisia" }
					}
				},
				new Country()
				{
					IsoCode = "TR",
					IsoCode3 = "TUR",
					PhoneCountryCode = "+90",
					UiOrder = 216,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Turecko" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Turkey" }
					}
				},
				new Country()
				{
					IsoCode = "TM",
					IsoCode3 = "TKM",
					PhoneCountryCode = "+993",
					UiOrder = 217,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Turkmenistán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Turkmenistan" }
					}
				},
				new Country()
				{
					IsoCode = "TV",
					IsoCode3 = "TUV",
					PhoneCountryCode = "+688",
					UiOrder = 218,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Tuvalu" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Tuvalu" }
					}
				},
				new Country()
				{
					IsoCode = "UG",
					IsoCode3 = "UGA",
					PhoneCountryCode = "+256",
					UiOrder = 219,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Uganda" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Uganda" }
					}
				},
				new Country()
				{
					IsoCode = "UA",
					IsoCode3 = "UKR",
					PhoneCountryCode = "+380",
					UiOrder = 220,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Ukrajina" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Ukraine" }
					}
				},
				new Country()
				{
					IsoCode = "UY",
					IsoCode3 = "URY",
					PhoneCountryCode = "+598",
					UiOrder = 221,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Uruguay" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Uruguay" }
					}
				},
				new Country()
				{
					IsoCode = "UZ",
					IsoCode3 = "UZB",
					PhoneCountryCode = "+998",
					UiOrder = 222,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Uzbekistán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Uzbekistan" }
					}
				},
				new Country()
				{
					IsoCode = "CX",
					IsoCode3 = "CXR",
					PhoneCountryCode = "+61",
					UiOrder = 223,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Vánoční ostrov" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Christmas Island" }
					}
				},
				new Country()
				{
					IsoCode = "VU",
					IsoCode3 = "VUT",
					PhoneCountryCode = "+678",
					UiOrder = 224,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Vanuatu" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Vanuatu" }
					}
				},
				new Country()
				{
					IsoCode = "VA",
					IsoCode3 = "VAT",
					PhoneCountryCode = "+379",
					UiOrder = 225,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Vatikán" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Vatican" }
					}
				},
				new Country()
				{
					IsoCode = "GB",
					IsoCode3 = "GBR",
					PhoneCountryCode = "+44",
					UiOrder = 226,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Spojené království" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "United Kingdom" }
					}
				},
				new Country()
				{
					IsoCode = "VE",
					IsoCode3 = "VEN",
					PhoneCountryCode = "+58",
					UiOrder = 227,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Venezuela" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Venezuela" }
					}
				},
				new Country()
				{
					IsoCode = "VN",
					IsoCode3 = "VNM",
					PhoneCountryCode = "+84",
					UiOrder = 228,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Vietnam" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Vietnam" }
					}
				},
				new Country()
				{
					IsoCode = "TL",
					IsoCode3 = "TLS",
					PhoneCountryCode = "+670",
					UiOrder = 229,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Timor-Leste" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Timor-Leste" }
					}
				},
				new Country()
				{
					IsoCode = "WF",
					IsoCode3 = "WLF",
					PhoneCountryCode = "+681",
					UiOrder = 230,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Wallis a Futuna" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Wallis and Futuna Islands" }
					}
				},
				new Country()
				{
					IsoCode = "ZM",
					IsoCode3 = "ZMB",
					PhoneCountryCode = "+260",
					UiOrder = 231,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Zambie" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Zambia" }
					}
				},
				new Country()
				{
					IsoCode = "ZW",
					IsoCode3 = "ZWE",
					PhoneCountryCode = "+263",
					UiOrder = 232,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Zimbabwe" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Zimbabwe" }
					}
				},
				new Country()
				{
					IsoCode = "AQ",
					IsoCode3 = "ATA",
					PhoneCountryCode = "+672",
					UiOrder = 233,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Antarktida" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Antarctica" }
					}
				},
				new Country()
				{
					IsoCode = "AX",
					IsoCode3 = "ALA",
					PhoneCountryCode = "+358",
					UiOrder = 234,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Alandské ostrovy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Aland Islands" }
					}
				},
				new Country()
				{
					IsoCode = "BF",
					IsoCode3 = "BFA",
					PhoneCountryCode = "+226",
					UiOrder = 235,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Burkina Faso" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Burkina Faso" }
					}
				},
				new Country()
				{
					IsoCode = "DM",
					IsoCode3 = "DMA",
					PhoneCountryCode = "+767",
					UiOrder = 236,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Dominica" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Dominica" }
					}
				},
				new Country()
				{
					IsoCode = "EH",
					IsoCode3 = "ESH",
					PhoneCountryCode = "+212",
					UiOrder = 237,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Západní Sahara" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Western Sahara" }
					}
				},
				new Country()
				{
					IsoCode = "GG",
					IsoCode3 = "GGY",
					PhoneCountryCode = "+44",
					UiOrder = 238,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Guernsey" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Guernsey" }
					}
				},
				new Country()
				{
					IsoCode = "JE",
					IsoCode3 = "JEY",
					PhoneCountryCode = "+44",
					UiOrder = 239,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Jersey" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Jersey" }
					}
				},
				new Country()
				{
					IsoCode = "TF",
					IsoCode3 = "ATF",
					PhoneCountryCode = "+262",
					UiOrder = 240,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Francouzská jižní území" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "French Southern Territories" }
					}
				},
				new Country()
				{
					IsoCode = "VG",
					IsoCode3 = "VGB",
					PhoneCountryCode = "+1284",
					UiOrder = 241,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Britské Panenské ostrovy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Virgin Islands (British)" }
					}
				},
				new Country()
				{
					IsoCode = "VI",
					IsoCode3 = "VIR",
					PhoneCountryCode = "+1",
					UiOrder = 242,
					Localizations =
					{
						new CountryLocalization { LanguageId = (int)Language.Entry.Czech, Name = "Americké Panenské ostrovy" },
						new CountryLocalization { LanguageId = (int)Language.Entry.English, Name = "Virgin Islands (USA)" }
					}
				}
			};

		Seed(For(countries).PairBy(country => country.IsoCode3));
	}
}
