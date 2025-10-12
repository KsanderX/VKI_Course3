using MyAppToday.Model;
using MyAppToday.Service;
using System.Collections.ObjectModel;

namespace MyAppToday.ViewModel
{
    public class AssignMastersViewModel
    {
        private IRequestService _requestService;
        public ObservableCollection<Master> Masters { get; set; }
        public Master SelectedMaster { get; set; }
        public ObservableCollection<Request> Requests { get; set; }
        public Request SelectedRequest { get; set; }
        public string? Comment { get; set; }

        public AssignMastersViewModel(IRequestService requestService)
        {
            _requestService = requestService;
            Masters = _requestService.GetMasters();
            Requests = _requestService.GetRequests();
        }
    }
}
