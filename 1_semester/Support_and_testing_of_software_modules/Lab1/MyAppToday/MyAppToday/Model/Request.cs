namespace MyAppToday.Model
{
    public class Request
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public string ProblemDescription { get; set; }
        public string Status { get; set; } = "Создано";
    }
}
