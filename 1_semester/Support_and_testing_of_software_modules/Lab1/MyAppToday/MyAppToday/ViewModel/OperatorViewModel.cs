using MyAppToday.Model;
using MyAppToday.Service;
using System.Collections.ObjectModel;

namespace MyAppToday.ViewModel
{
    public class OperatorViewModel
    {
        private IRequestService _requestService;
        public List<Model.Client> Clients { get; set; }
        public Client SelectedClient { get; set; }
        public ObservableCollection<Equipment> Equipment { get; set; }
        public Equipment SelectedEquipment { get; set; }
        public string ProblemDescription { get; set; }
        public OperatorViewModel(IRequestService requestService)
        {
            _requestService = requestService;
            Clients = _requestService.GetClients();
            Equipment = _requestService.GetEquipment();
        }
    }
}
