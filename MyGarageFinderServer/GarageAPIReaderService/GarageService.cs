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
                    GarageNumber = record.MisparMosah,
                    GarageName = record.ShemMosah,
                    TypeCode = record.CodSugMosah.ToString(),
                    GarageType = record.SugMosah,
                    GarageAddress = record.Ktovet,
                    City = record.Yishuv,
                    Phone = record.Telephone,
                    ZipCode = record.Mikud,
                    SpecializationCode = record.CodMiktzoa,
                    Specialization = record.Miktzoa,
                    GarageManager = record.MenahelMiktzoa,
                    GarageLicense = record.RashamHavarot,
                    WorkingHours = string.Empty // אם אין נתון לשעות עבודה
                };

                _context.Garages.Add(garage);
            }

            await _context.SaveChangesAsync(); // שמירת הנתונים בבסיס הנתונים
        }
    }
}
