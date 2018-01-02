using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetTripsByUserName(string username);
        Trip GetTripByName(string tripName);

        Trip GetUserTripByName(string tripName, string username);

        void AddTrip(Trip trip);
        void AddStop(string tripName, Stop newStop,string username);

        Task<bool> SaveChangesAsync();

        

    }
}