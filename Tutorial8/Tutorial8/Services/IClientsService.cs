using Tutorial8.Models.DTOs;

namespace Tutorial8.Services;

public interface IClientsService
{
    Task<int> CreateClientAsync(ClientDTO dto);
    Task<IEnumerable<TripDTO>> GetTripsForClientAsync(int idClient);
    Task<bool> RegisterClientToTripAsync(int idClient, int idTrip);
    Task<bool> UnregisterClientFromTripAsync(int idClient, int idTrip);
}
