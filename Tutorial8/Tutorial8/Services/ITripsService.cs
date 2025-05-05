using Tutorial8.Models.DTOs;

namespace Tutorial8.Services;

public interface ITripsService
{
    Task<IEnumerable<TripDTO>> GetTripsAsync();
    Task<TripDTO?> GetTripByIdAsync(int id);
}