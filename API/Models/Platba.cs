using System.Text.Json;

namespace API.Models
{
    public class Platba
    {
        public float castka { get; set; }
        public string mena { get; set; }
        public DateTime datum { get; set; }
        public string typ_platby { get; set; }
        public string[] seznam_polozek { get; set; }

        public static Platba parseJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json) || json == "{}") throw new JsonException();
            Platba? p = JsonSerializer.Deserialize<Platba>(json);
            if (p is null) throw new JsonException();
            return p;
        }
    }
}
