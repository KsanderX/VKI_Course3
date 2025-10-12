using MyAppToday.Model;
using MyAppToday.Service;
using System.Collections.ObjectModel;

namespace MyAppToday.ViewModel
{
    public class RequestViewModel
    {
        private IRequestService _requestService;
        public ObservableCollection<Request> Requests { get; set; }
        public Request SelectedRequest { get; set; }
        public RequestViewModel(IRequestService requestService)
        {
            _requestService = requestService;
            Requests = _requestService.GetRequests();
        }
    }
}
