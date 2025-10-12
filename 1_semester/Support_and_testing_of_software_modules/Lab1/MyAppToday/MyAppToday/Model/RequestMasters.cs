
namespace MyAppToday.Model
{
    public class RequestMasters
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public int MasterId { get; set; }
        public Master Master { get; set; }
        public int RequestId {  get; set; }
        public Request Request { set; get; }
    }
}
