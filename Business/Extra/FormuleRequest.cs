namespace API.Business.Extra
{
    public class FormuleRequest
    {
        public int id { get; set; } 
        public string module_id { get; set; }
        public int duree { get; set; }
        public int montant { get; set; }
        public string description { get; set; }
        public string type { get; set; }
    }
}
