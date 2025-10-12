using MyAppToday.Model;
using MyAppToday.Service;
using System.Collections.ObjectModel;

namespace MyAppToday.ViewModel
{
    public class ClientViewModel
    {
        private IRequestService _requestService;
        public ObservableCollection<Model.Equipment> Equipment {  get; set; }
        public Equipment SelectedEquipment { get; set; }
        public string ProblemDescription { get; set; }
        public ClientViewModel(IRequestService requestService)
        {
            _requestService = requestService;
            Equipment = _requestService.GetEquipment();
        }
    }
}
