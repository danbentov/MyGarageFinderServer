using System.Text.Json;

namespace MyGarageFinderServer.GarageAPIReaderService
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MosahRecord>> GetApiData(string apiUrl, string resourceId)
        {
            var queryParams = $"?resource_id={resourceId}&limit=1000"; // אפשר לשנות את ה-limit
            var response = await _httpClient.GetStringAsync(apiUrl + queryParams);

            // שימוש ב-System.Text.Json להמיר את ה-JSON
            try
            {
                var jsonResponse = JsonSerializer.Deserialize<ApiResponse>(response);
                return jsonResponse?.Result?.Records ?? new List<MosahRecord>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return new List<MosahRecord>();
            }
        }
    }

    public class ApiResponse
    {
        public ApiResult Result { get; set; }
    }

    public class ApiResult
    {
        public List<MosahRecord> Records { get; set; }
    }

    public class MosahRecord
    {
        public int MisparMosah { get; set; }
        public string ShemMosah { get; set; }
        public int CodSugMosah { get; set; }
        public string SugMosah { get; set; }
        public string Ktovet { get; set; }
        public string Yishuv { get; set; }
        public string Telephone { get; set; }
        public int Mikud { get; set; }
        public int CodMiktzoa { get; set; }
        public string Miktzoa { get; set; }
        public string MenahelMiktzoa { get; set; }
        public int RashamHavarot { get; set; }
        public string Testime { get; set; } // שים לב אם מדובר בתאריך, יש לעדכן ל-DATE בטבלה
    }
}
