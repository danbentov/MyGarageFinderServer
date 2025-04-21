using MyGarageFinderServer.Models;

namespace MyGarageFinderServer.GarageAPIReaderService
{
    public class GarageService
    {
        private readonly MyGarageFinderDbContext _context;
        private readonly ApiService _apiService;

        public GarageService(MyGarageFinderDbContext context, ApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        public async Task ImportGaragesFromApiAsync()
        {
            string apiUrl = "https://data.gov.il/api/3/action/datastore_search";
            string resourceId = "bb68386a-a331-4bbc-b668-bba2766d517d"; // ה-resource_id שלך

            // קריאת הנתונים מה-API
            var records = await _apiService.GetApiData(apiUrl, resourceId);

            // הוספת הנתונים לבסיס הנתונים
            foreach (var record in records)
            {
                var garage = new Garage
                {
                    GarageNumber = record.mispar_mosah,
                    GarageName = record.shem_mosah,
                    TypeCode = record.cod_sug_mosah.ToString(),
                    GarageType = record.sug_mosah,
                    GarageAddress = record.ktovet,
                    City = record.yishuv,
                    Phone = record.telephone,
                    ZipCode = record.mikud,
                    SpecializationCode = record.cod_miktzoa,
                    Specialization = record.miktzoa,
                    GarageManager = record.menahel_miktzoa,
                    GarageLicense = record.rasham_havarot,
                    WorkingHours = string.Empty // אם אין נתון לשעות עבודה
                };

                _context.Garages.Add(garage);
            }

            await _context.SaveChangesAsync(); // שמירת הנתונים בבסיס הנתונים
        }

        public async Task<Garage> ImportGaragesFromApiAsync(int garageId)
        {
            string apiUrl = "https://data.gov.il/api/3/action/datastore_search";
            string resourceId = "bb68386a-a331-4bbc-b668-bba2766d517d"; // ה-resource_id שלך

            // קריאת הנתונים מה-API
            var records = await _apiService.GetApiData(apiUrl, resourceId);

            List<Garage> garages = new List<Garage>();

            // הוספת הנתונים לבסיס הנתונים
            foreach (var record in records)
            {
                var garage = new Garage
                {
                    GarageNumber = record.mispar_mosah,
                    GarageName = record.shem_mosah,
                    TypeCode = record.cod_sug_mosah.ToString(),
                    GarageType = record.sug_mosah,
                    GarageAddress = record.ktovet,
                    City = record.yishuv,
                    Phone = record.telephone,
                    ZipCode = record.mikud,
                    SpecializationCode = record.cod_miktzoa,
                    Specialization = record.miktzoa,
                    GarageManager = record.menahel_miktzoa,
                    GarageLicense = record.rasham_havarot,
                    WorkingHours = string.Empty // אם אין נתון לשעות עבודה
                };

                garages.Add(garage);
            }

            Garage? g = garages.Where(gg => gg.GarageId == garageId).FirstOrDefault();
            return g;
        }
    }
}
