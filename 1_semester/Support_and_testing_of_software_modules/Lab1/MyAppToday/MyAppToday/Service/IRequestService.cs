using MyAppToday.Model;
using System.Collections.ObjectModel;

namespace MyAppToday.Service
{
    public interface IRequestService
    {
        public ObservableCollection<Model.Equipment> GetEquipment();
        public void CreateRequest(Model.Request request);
        public List<Client> GetClients();
        public ObservableCollection<Model.Request> GetRequests();
        public ObservableCollection<Master> GetMasters();
        public void AssignMaster(RequestMasters requestMasters);
        public bool CanAssignMaster(Model.Request request, Model.Master master);
        public List<Master> GetAssignMasters(Model.Request selectedRequest);
    }
}
