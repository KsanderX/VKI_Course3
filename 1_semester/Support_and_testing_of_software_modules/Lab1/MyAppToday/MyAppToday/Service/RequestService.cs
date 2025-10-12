using MyAppToday.Model;
using System.Collections.ObjectModel;

namespace MyAppToday.Service
{
    public class RequestService : IRequestService
    {
        private MyDbContext _context;
        public RequestService()
        {
            _context = new MyDbContext();
        }
        public void CreateRequest(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();
        }

        public List<Client> GetClients()
        {
            return _context.Clients.ToList();
        }

        public ObservableCollection<Equipment> GetEquipment()
        {
            return new ( _context.Equipment.ToList() );
        }

        public ObservableCollection<Master> GetMasters()
        {
            return new ( _context.Masters.ToList() );
        }

        public ObservableCollection<Request> GetRequests()
        {
            return new ( _context.Requests.ToList() );
        }
        public bool CanAssignMaster(Request request, Master master)
        {
            int count = _context.RequestMasters.Where(r => r.RequestId == request.Id && r.MasterId == master.Id).Count();

            return count == 0;
        }


        public void AssignMaster(RequestMasters requestMasters)
        {
            _context.RequestMasters.Add(requestMasters);
            _context.SaveChanges();
        }

        public List<Master> GetAssignMasters(Request selectedRequest)
        {
            return _context.RequestMasters.Where(rm => rm.RequestId == selectedRequest.Id).Select(rm => rm.Master).ToList();
        }
    }
}
