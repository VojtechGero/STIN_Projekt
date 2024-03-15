namespace API
{
    public class Platba
    {
        public float castka { get; set; }
        public string mena { get; set; }
        public DateTime datu { get; set; }
        public string typ_platby { get; set; }
        public string[] seznam_polozek { get; set; }
    }
}
