using Tut12.DTOs;

namespace Tut12.Services;

public interface ITripService
{
    Task<TripsDto>GetTrip(int page, int pageSize);
    Task AssignClient(AssignClientDto assignClientDto);
}