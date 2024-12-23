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
            var queryParams = $"?resource_id={resourceId}&limit=250"; // אפשר לשנות את ה-limit
            var response = await _httpClient.GetStringAsync(apiUrl + queryParams);

            // שימוש ב-System.Text.Json להמיר את ה-JSON
            try
            {
                var jsonResponse = JsonSerializer.Deserialize<ApiResponse>(response);
                return jsonResponse?.result?.records.ToList();
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
        public string help { get; set; }
        public bool success { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public bool include_total { get; set; }
        public int limit { get; set; }
        public string records_format { get; set; }
        public string resource_id { get; set; }
        public object total_estimation_threshold { get; set; }
        public MosahRecord[] records { get; set; }
        public Field[] fields { get; set; }
        public _Links _links { get; set; }
        public int total { get; set; }
        public bool total_was_estimated { get; set; }
    }

    public class _Links
    {
        public string start { get; set; }
        public string next { get; set; }
    }

    public class MosahRecord
    {
        public int _id { get; set; }
        public int mispar_mosah { get; set; }
        public string shem_mosah { get; set; }
        public int cod_sug_mosah { get; set; }
        public string sug_mosah { get; set; }
        public string ktovet { get; set; }
        public string yishuv { get; set; }
        public string telephone { get; set; }
        public int mikud { get; set; }
        public int cod_miktzoa { get; set; }
        public string miktzoa { get; set; }
        public string menahel_miktzoa { get; set; }
        public int rasham_havarot { get; set; }
        public string TESTIME { get; set; }
    }

    public class Field
    {
        public string id { get; set; }
        public string type { get; set; }
    }

}
