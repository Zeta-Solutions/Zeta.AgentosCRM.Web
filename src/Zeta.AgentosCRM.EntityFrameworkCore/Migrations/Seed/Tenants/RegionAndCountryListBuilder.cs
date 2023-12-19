using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.CRMSetup.Regions;
using Zeta.AgentosCRM.EntityFrameworkCore;

namespace Zeta.AgentosCRM.Migrations.Seed.Tenants
{
    public class RegionAndCountryListBuilder
    {
        private readonly AgentosCRMDbContext _context;
        private readonly int _tenantId;

        public RegionAndCountryListBuilder(AgentosCRMDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRegionsAndCountries();
        }

        private void CreateRegionsAndCountries() 
        {
            var africa = _context.Regions.FirstOrDefault(p => p.Name == "Africa");
            if (africa == null)
            {
                _context.Regions.Add(
                    new Region
                    {
                        TenantId = _tenantId,
                        Name = "Africa",
                        Abbrivation = "AF"

                    });
            }

            var asia = _context.Regions.FirstOrDefault(p => p.Name == "Asia");
            if (asia == null)
            {
                _context.Regions.Add(
                    new Region
                    {
                        TenantId = _tenantId,
                        Name = "Asia",
                        Abbrivation = "AS"
                    });
            }

            var europe = _context.Regions.FirstOrDefault(p => p.Name == "Europe");
            if (europe == null)
            {
                _context.Regions.Add(
                    new Region
                    {
                        TenantId = _tenantId,
                        Name = "Europe",
                        Abbrivation = "EU"
                    });
            }

            var europe_asia = _context.Regions.FirstOrDefault(p => p.Name == "Europe/Asia");
            if (europe_asia == null)
            {
                _context.Regions.Add(
                    new Region
                    {
                        TenantId = _tenantId,
                        Name = "Europe/Asia",
                        Abbrivation = "EU/AS"
                    });
            }

            var middleEast = _context.Regions.FirstOrDefault(p => p.Name == "Middle East");
            if (middleEast == null)
            {
                _context.Regions.Add(
                    new Region
                    {
                        TenantId = _tenantId,
                        Name = "Middle East",
                        Abbrivation = "ME"
                    });
            }

            var northAmerica = _context.Regions.FirstOrDefault(p => p.Name == "North America");
            if (northAmerica == null)
            {
                _context.Regions.Add(
                    new Region
                    {
                        TenantId = _tenantId,
                        Name = "North America",
                        Abbrivation = "NA"
                    });
            }

            var oceania = _context.Regions.FirstOrDefault(p => p.Name == "Oceania");
            if (oceania == null)
            {
                _context.Regions.Add(
                    new Region
                    {
                        TenantId = _tenantId,
                        Name = "Oceania",
                        Abbrivation = "OC"
                    });
            }

            var southAmerica = _context.Regions.FirstOrDefault(p => p.Name == "South America");
            if (southAmerica == null)
            {
                _context.Regions.Add(
                    new Region
                    {
                        TenantId = _tenantId,
                        Name = "South America",
                        Abbrivation = "SA"
                    });
            }

            #region "Africa"

            var southAfrica = _context.Countries.FirstOrDefault(p => p.Name == "South Africa");
            if (southAfrica == null)
            {
                _context.Countries.Add(
                new Country
                {
                    Name = "South Africa",
                    Abbrivation = "ZAF",
                    Code = "27",
                    RegionId = africa.Id
                });
            }

            var egypt = _context.Countries.FirstOrDefault(p => p.Name == "Egypt");
            if (egypt == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Egypt",
                        Abbrivation = "EGY",
                        Code = "20",
                        RegionId = africa.Id
                    });
            }

            var nigeria = _context.Countries.FirstOrDefault(p => p.Name == "Nigeria");
            if (egypt == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Nigeria",
                        Abbrivation = "NGA",
                        Code = "234",
                        RegionId = africa.Id
                    });
            }

            var kenya = _context.Countries.FirstOrDefault(p => p.Name == "Kenya");
            if (kenya == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Kenya",
                        Abbrivation = "KEN",
                        Code = "254",
                        RegionId = africa.Id
                    });
            }

            var ghana = _context.Countries.FirstOrDefault(p => p.Name == "Ghana");
            if (ghana == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Ghana",
                        Abbrivation = "GHA",
                        Code = "233",
                        RegionId = africa.Id
                    });
            }

            var morocco = _context.Countries.FirstOrDefault(p => p.Name == "Morocco");
            if (morocco == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Morocco",
                        Abbrivation = "MAR",
                        Code = "212",
                        RegionId = africa.Id
                    });
            }

            var tunisia = _context.Countries.FirstOrDefault(p => p.Name == "Tunisia");
            if (tunisia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Tunisia",
                        Abbrivation = "TUN",
                        Code = "216",
                        RegionId = africa.Id
                    });
            }
            #endregion

            #region "Asia"
            /*-------------------Aisa--------------------------------*/


            var singapore = _context.Countries.FirstOrDefault(p => p.Name == "Singapore");
            if (singapore == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Singapore",
                        Abbrivation = "SGP",
                        Code = "65",
                        RegionId = asia.Id
                    });
            }

            var hongKong = _context.Countries.FirstOrDefault(p => p.Name == "Hong Kong");
            if (hongKong == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Hong Kong",
                        Abbrivation = "HKG",
                        Code = "852",
                        RegionId = asia.Id
                    });
            }

            var japan = _context.Countries.FirstOrDefault(p => p.Name == "Japan");
            if (japan == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Japan",
                        Abbrivation = "JPN",
                        Code = "81",
                        RegionId = asia.Id
                    });
            }

            var china = _context.Countries.FirstOrDefault(p => p.Name == "China");
            if (china == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "China",
                        Abbrivation = "CHN",
                        Code = "86",
                        RegionId = asia.Id
                    });
            }

            var southKorea = _context.Countries.FirstOrDefault(p => p.Name == "South Korea");
            if (southKorea == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "South Korea",
                        Abbrivation = "KOR",
                        Code = "82",
                        RegionId = asia.Id
                    });
            }

            var taiwan = _context.Countries.FirstOrDefault(p => p.Name == "Taiwan");
            if (taiwan == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Taiwan",
                        Abbrivation = "TWN",
                        Code = "886",
                        RegionId = asia.Id
                    });
            }

            var india = _context.Countries.FirstOrDefault(p => p.Name == "India");
            if (india == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "India",
                        Abbrivation = "IND",
                        Code = "91",
                        RegionId = asia.Id
                    });
            }

            var malaysia = _context.Countries.FirstOrDefault(p => p.Name == "Malaysia");
            if (malaysia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Malaysia",
                        Abbrivation = "MYS",
                        Code = "60",
                        RegionId = asia.Id
                    });
            }

            var thailand = _context.Countries.FirstOrDefault(p => p.Name == "Thailand");
            if (thailand == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Thailand",
                        Abbrivation = "THA",
                        Code = "66",
                        RegionId = asia.Id
                    });
            }

            var indonesia = _context.Countries.FirstOrDefault(p => p.Name == "Indonesia");
            if (indonesia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Indonesia",
                        Abbrivation = "IDN",
                        Code = "62",
                        RegionId = asia.Id
                    });
            }

            var philippines = _context.Countries.FirstOrDefault(p => p.Name == "Philippines");
            if (philippines == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Philippines",
                        Abbrivation = "PHL",
                        Code = "63",
                        RegionId = asia.Id
                    });
            }

            var vietnam = _context.Countries.FirstOrDefault(p => p.Name == "Vietnam");
            if (vietnam == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Vietnam",
                        Abbrivation = "VNM",
                        Code = "84",
                        RegionId = asia.Id
                    });
            }

            var bangladesh = _context.Countries.FirstOrDefault(p => p.Name == "Bangladesh");
            if (bangladesh == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Bangladesh",
                        Abbrivation = "BGD",
                        Code = "880",
                        RegionId = asia.Id
                    });
            }

            var pakistan = _context.Countries.FirstOrDefault(p => p.Name == "Pakistan");
            if (pakistan == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Pakistan",
                        Abbrivation = "PAK",
                        Code = "92",
                        RegionId = asia.Id
                    });
            }
            #endregion

            #region "Europe"
            /*-------------------Europe--------------------------------*/


            var uk = _context.Countries.FirstOrDefault(p => p.Name == "United Kingdom");
            if (uk == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "United Kingdom",
                        Abbrivation = "UK",
                        Code = "44",
                        RegionId = europe.Id
                    });
            }

            var germany = _context.Countries.FirstOrDefault(p => p.Name == "Germany");
            if (germany == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Germany",
                        Abbrivation = "GER",
                        Code = "49",
                        RegionId = europe.Id
                    });
            }

            var switzerland = _context.Countries.FirstOrDefault(p => p.Name == "Switzerland");
            if (switzerland == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Switzerland",
                        Abbrivation = "CHE",
                        Code = "41",
                        RegionId = europe.Id
                    });
            }

            var sweden = _context.Countries.FirstOrDefault(p => p.Name == "Sweden");
            if (sweden == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Sweden",
                        Abbrivation = "SWE",
                        Code = "46",
                        RegionId = europe.Id
                    });
            }

            var france = _context.Countries.FirstOrDefault(p => p.Name == "France");
            if (france == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "France",
                        Abbrivation = "FRA",
                        Code = "33",
                        RegionId = europe.Id
                    });
            }

            var netherlands = _context.Countries.FirstOrDefault(p => p.Name == "Netherlands");
            if (netherlands == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Netherlands",
                        Abbrivation = "NLD",
                        Code = "31",
                        RegionId = europe.Id
                    });
            }

            var finland = _context.Countries.FirstOrDefault(p => p.Name == "Finland");
            if (finland == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Finland",
                        Abbrivation = "FIN",
                        Code = "358",
                        RegionId = europe.Id
                    });
            }

            var denmark = _context.Countries.FirstOrDefault(p => p.Name == "Denmark");
            if (denmark == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Denmark",
                        Abbrivation = "DNK",
                        Code = "45",
                        RegionId = europe.Id
                    });
            }

            var belgium = _context.Countries.FirstOrDefault(p => p.Name == "Belgium");
            if (belgium == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Belgium",
                        Abbrivation = "BEL",
                        Code = "32",
                        RegionId = europe.Id
                    });
            }

            var norway = _context.Countries.FirstOrDefault(p => p.Name == "Norway");
            if (norway == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Norway",
                        Abbrivation = "NOR",
                        Code = "47",
                        RegionId = europe.Id
                    });
            }

            var ireland = _context.Countries.FirstOrDefault(p => p.Name == "Ireland");
            if (ireland == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Ireland",
                        Abbrivation = "IRL",
                        Code = "353",
                        RegionId = europe.Id
                    });
            }

            var austria = _context.Countries.FirstOrDefault(p => p.Name == "Austria");
            if (austria == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Austria",
                        Abbrivation = "AUT",
                        Code = "43",
                        RegionId = europe.Id
                    });
            }

            var italy = _context.Countries.FirstOrDefault(p => p.Name == "Italy");
            if (italy == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Italy",
                        Abbrivation = "ITA",
                        Code = "39",
                        RegionId = europe.Id
                    });
            }

            var spain = _context.Countries.FirstOrDefault(p => p.Name == "Spain");
            if (spain == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Spain",
                        Abbrivation = "ESP",
                        Code = "34",
                        RegionId = europe.Id
                    });
            }

            var luxembourg = _context.Countries.FirstOrDefault(p => p.Name == "Luxembourg");
            if (luxembourg == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Luxembourg",
                        Abbrivation = "LUX",
                        Code = "352",
                        RegionId = europe.Id
                    });
            }

            var greece = _context.Countries.FirstOrDefault(p => p.Name == "Greece");
            if (greece == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Greece",
                        Abbrivation = "GRC",
                        Code = "30",
                        RegionId = europe.Id
                    });
            }

            var portugal = _context.Countries.FirstOrDefault(p => p.Name == "Portugal");
            if (portugal == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Portugal",
                        Abbrivation = "PRT",
                        Code = "351",
                        RegionId = europe.Id
                    });
            }

            var czechRepublic = _context.Countries.FirstOrDefault(p => p.Name == "Czech Republic");
            if (czechRepublic == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Czech Republic",
                        Abbrivation = "CZE",
                        Code = "420",
                        RegionId = europe.Id
                    });
            }

            var poland = _context.Countries.FirstOrDefault(p => p.Name == "Poland");
            if (poland == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Poland",
                        Abbrivation = "POL",
                        Code = "48",
                        RegionId = europe.Id
                    });
            }

            var hungary = _context.Countries.FirstOrDefault(p => p.Name == "Hungary");
            if (hungary == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Hungary",
                        Abbrivation = "HUN",
                        Code = "36",
                        RegionId = europe.Id
                    });
            }

            var ukraine = _context.Countries.FirstOrDefault(p => p.Name == "Ukraine");
            if (ukraine == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Ukraine",
                        Abbrivation = "UKR",
                        Code = "380",
                        RegionId = europe.Id
                    });
            }

            var croatia = _context.Countries.FirstOrDefault(p => p.Name == "Croatia");
            if (croatia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Croatia",
                        Abbrivation = "HRV",
                        Code = "385",
                        RegionId = europe.Id
                    });
            }

            var slovenia = _context.Countries.FirstOrDefault(p => p.Name == "Slovenia");
            if (slovenia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Slovenia",
                        Abbrivation = "SVN",
                        Code = "386",
                        RegionId = europe.Id
                    });
            }

            var bulgaria = _context.Countries.FirstOrDefault(p => p.Name == "Bulgaria");
            if (bulgaria == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Bulgaria",
                        Abbrivation = "BGR",
                        Code = "359",
                        RegionId = europe.Id
                    });
            }

            var romania = _context.Countries.FirstOrDefault(p => p.Name == "Romania");
            if (romania == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Romania",
                        Abbrivation = "ROU",
                        Code = "40",
                        RegionId = europe.Id
                    });
            }

            var slovakia = _context.Countries.FirstOrDefault(p => p.Name == "Slovakia");
            if (slovakia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Slovakia",
                        Abbrivation = "SVK",
                        Code = "421",
                        RegionId = europe.Id
                    });
            }

            var serbia = _context.Countries.FirstOrDefault(p => p.Name == "Serbia");
            if (serbia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Serbia",
                        Abbrivation = "SRB",
                        Code = "381",
                        RegionId = europe.Id
                    });
            }

            var bosniaAndHerzegovina = _context.Countries.FirstOrDefault(p => p.Name == "Bosnia and Herzegovina");
            if (bosniaAndHerzegovina == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Bosnia and Herzegovina",
                        Abbrivation = "BIH",
                        Code = "387",
                        RegionId = europe.Id
                    });
            }

            var montenegro = _context.Countries.FirstOrDefault(p => p.Name == "Montenegro");
            if (montenegro == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Montenegro",
                        Abbrivation = "MNE",
                        Code = "382",
                        RegionId = europe.Id
                    });
            }

            var northMacedonia = _context.Countries.FirstOrDefault(p => p.Name == "North Macedonia");
            if (northMacedonia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "North Macedonia",
                        Abbrivation = "MKD",
                        Code = "389",
                        RegionId = europe.Id
                    });
            }

            var albania = _context.Countries.FirstOrDefault(p => p.Name == "Albania");
            if (albania == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Albania",
                        Abbrivation = "ALB",
                        Code = "355",
                        RegionId = europe.Id
                    });
            }

            var kosovo = _context.Countries.FirstOrDefault(p => p.Name == "Kosovo");
            if (kosovo == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Kosovo",
                        Abbrivation = "XK",
                        Code = "383",
                        RegionId = europe.Id
                    });
            }

            var moldova = _context.Countries.FirstOrDefault(p => p.Name == "Moldova");
            if (moldova == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Moldova",
                        Abbrivation = "MDA",
                        Code = "373",
                        RegionId = europe.Id
                    });
            }

            var belarus = _context.Countries.FirstOrDefault(p => p.Name == "Belarus");
            if (belarus == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Belarus",
                        Abbrivation = "BLR",
                        Code = "375",
                        RegionId = europe.Id
                    });
            }

            var latvia = _context.Countries.FirstOrDefault(p => p.Name == "Latvia");
            if (latvia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Latvia",
                        Abbrivation = "LVA",
                        Code = "371",
                        RegionId = europe.Id
                    });
            }

            var lithuania = _context.Countries.FirstOrDefault(p => p.Name == "Lithuania");
            if (lithuania == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Lithuania",
                        Abbrivation = "LTU",
                        Code = "370",
                        RegionId = europe.Id
                    });
            }

            var estonia = _context.Countries.FirstOrDefault(p => p.Name == "Estonia");
            if (estonia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Estonia",
                        Abbrivation = "EST",
                        Code = "372",
                        RegionId = europe.Id
                    });
            }

            var cyprus = _context.Countries.FirstOrDefault(p => p.Name == "Cyprus");
            if (cyprus == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Cyprus",
                        Abbrivation = "CYP",
                        Code = "357",
                        RegionId = europe.Id
                    });
            }

            var malta = _context.Countries.FirstOrDefault(p => p.Name == "Malta");
            if (malta == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Malta",
                        Abbrivation = "MLT",
                        Code = "356",
                        RegionId = europe.Id
                    });
            }

            var iceland = _context.Countries.FirstOrDefault(p => p.Name == "Iceland");
            if (iceland == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Iceland",
                        Abbrivation = "ISL",
                        Code = "354",
                        RegionId = europe.Id
                    });
            }

            var faroeIslands = _context.Countries.FirstOrDefault(p => p.Name == "Faroe Islands");
            if (faroeIslands == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Faroe Islands",
                        Abbrivation = "FRO",
                        Code = "298",
                        RegionId = europe.Id
                    });
            }
            #endregion

            #region "europe_asia"

            var russia = _context.Countries.FirstOrDefault(p => p.Name == "Russia");
            if (russia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Russia",
                        Abbrivation = "RUS",
                        Code = "7",
                        RegionId = europe_asia.Id
                    });
            }

            var turkey = _context.Countries.FirstOrDefault(p => p.Name == "Turkey");
            if (turkey == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Turkey",
                        Abbrivation = "TUR",
                        Code = "90",
                        RegionId = europe_asia.Id
                    });
            }

            #endregion

            #region "MiddleEast"

            var israel = _context.Countries.FirstOrDefault(p => p.Name == "Israel");
            if (israel == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Israel",
                        Abbrivation = "ISR",
                        Code = "972",
                        RegionId = middleEast.Id
                    });
            }

            var uae = _context.Countries.FirstOrDefault(p => p.Name == "United Arab Emirates");
            if (uae == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "United Arab Emirates",
                        Abbrivation = "UAE",
                        Code = "971",
                        RegionId = middleEast.Id
                    });
            }

            var saudiArabia = _context.Countries.FirstOrDefault(p => p.Name == "Saudi Arabia");
            if (saudiArabia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Saudi Arabia",
                        Abbrivation = "SAU",
                        Code = "966",
                        RegionId = middleEast.Id
                    });
            }

            var qatar = _context.Countries.FirstOrDefault(p => p.Name == "Qatar");
            if (qatar == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Qatar",
                        Abbrivation = "QAT",
                        Code = "974",
                        RegionId = middleEast.Id
                    });
            }
            #endregion

            #region "NorthAmerica"

            var usa = _context.Countries.FirstOrDefault(p => p.Name == "United States");
            if (usa == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "United States",
                        Abbrivation = "USA",
                        Code = "1",
                        RegionId = northAmerica.Id
                    });
            }

            var canada = _context.Countries.FirstOrDefault(p => p.Name == "Canada");
            if (canada == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Canada",
                        Abbrivation = "CAN",
                        Code = "1",
                        RegionId = northAmerica.Id
                    });
            }

            var mexico = _context.Countries.FirstOrDefault(p => p.Name == "Mexico");
            if (mexico == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Mexico",
                        Abbrivation = "MEX",
                        Code = "52",
                        RegionId = northAmerica.Id
                    });
            }

            var costaRica = _context.Countries.FirstOrDefault(p => p.Name == "Costa Rica");
            if (costaRica == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Costa Rica",
                        Abbrivation = "CRI",
                        Code = "506",
                        RegionId = northAmerica.Id
                    });
            }

            var panama = _context.Countries.FirstOrDefault(p => p.Name == "Panama");
            if (panama == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Panama",
                        Abbrivation = "PAN",
                        Code = "507",
                        RegionId = northAmerica.Id
                    });
            }

            #endregion

            #region "Oceania"

            var australia = _context.Countries.FirstOrDefault(p => p.Name == "Australia");
            if (australia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Australia",
                        Abbrivation = "AUS",
                        Code = "61",
                        RegionId = oceania.Id
                    });
            }

            var newZealand = _context.Countries.FirstOrDefault(p => p.Name == "New Zealand");
            if (newZealand == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "New Zealand",
                        Abbrivation = "NZL",
                        Code = "64",
                        RegionId = oceania.Id
                    });
            }

            var fiji = _context.Countries.FirstOrDefault(p => p.Name == "Fiji");
            if (fiji == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Fiji",
                        Abbrivation = "FJI",
                        Code = "679",
                        RegionId = oceania.Id
                    });
            }

            #endregion

            #region "South America"

            var brazil = _context.Countries.FirstOrDefault(p => p.Name == "Brazil");
            if (brazil == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Brazil",
                        Abbrivation = "BRA",
                        Code = "55",
                        RegionId = southAmerica.Id
                    });
            }

            var chile = _context.Countries.FirstOrDefault(p => p.Name == "Chile");
            if (chile == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Chile",
                        Abbrivation = "CHL",
                        Code = "56",
                        RegionId = southAmerica.Id
                    });
            }

            var argentina = _context.Countries.FirstOrDefault(p => p.Name == "Argentina");
            if (argentina == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Argentina",
                        Abbrivation = "ARG",
                        Code = "54",
                        RegionId = southAmerica.Id
                    });
            }

            var colombia = _context.Countries.FirstOrDefault(p => p.Name == "Colombia");
            if (colombia == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Colombia",
                        Abbrivation = "COL",
                        Code = "57",
                        RegionId = southAmerica.Id
                    });
            }

            var peru = _context.Countries.FirstOrDefault(p => p.Name == "Peru");
            if (peru == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Peru",
                        Abbrivation = "PER",
                        Code = "51",
                        RegionId = southAmerica.Id
                    });
            }

            var ecuador = _context.Countries.FirstOrDefault(p => p.Name == "Ecuador");
            if (ecuador == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Ecuador",
                        Abbrivation = "ECU",
                        Code = "593",
                        RegionId = southAmerica.Id
                    });
                _context.SaveChanges();
            }

            var venezuela = _context.Countries.FirstOrDefault(p => p.Name == "Venezuela");
            if (venezuela == null)
            {
                _context.Countries.Add(
                    new Country
                    {
                        TenantId = _tenantId,
                        Name = "Venezuela",
                        Abbrivation = "VEN",
                        Code = "58",
                        RegionId = southAmerica.Id
                    });
            }


            #endregion
        }
    }
}
