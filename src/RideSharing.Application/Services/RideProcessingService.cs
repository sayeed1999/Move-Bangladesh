using System.Text;
using System.Text.Json;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.Services;

public class RideProcessingService : IRideProcessingService
{
	private readonly HttpClient httpClient;

	public RideProcessingService(HttpClient httpClient)
  {
		this.httpClient = httpClient;
	}

	public async Task<bool> IsTripRequestTransitionValid(TripRequestStatus fromStatus, TripRequestStatus toStatus)
	{
      var dto = new TransitionCheckerDto((int)fromStatus, (int)toStatus);
      var dtoString = JsonSerializer.Serialize(dto);
      var requestContent = new StringContent(dtoString, Encoding.UTF8, "application/json");

      var response = await httpClient.PostAsync("/transition-checker/trip-request-status", requestContent);
      response.EnsureSuccessStatusCode();
      
      var content = await response.Content.ReadAsStringAsync();
      var responseDto = JsonSerializer.Deserialize<TransitionCheckerResponseDto>(content);
      return responseDto.valid;
	}

	public async Task<bool> IsTripTransitionValid(TripStatus fromStatus, TripStatus toStatus)
	{
      var dto = new TransitionCheckerDto((int)fromStatus, (int)toStatus);
      var dtoString = JsonSerializer.Serialize(dto);
      var requestContent = new StringContent(dtoString, Encoding.UTF8, "application/json");

      var response = await httpClient.PostAsync("/transition-checker/trip-status", requestContent);
      response.EnsureSuccessStatusCode();
      
      var content = await response.Content.ReadAsStringAsync();
      var responseDto = JsonSerializer.Deserialize<TransitionCheckerResponseDto>(content);
      return responseDto.valid;
	}
}

public record struct TransitionCheckerDto(int fromStatus, int toStatus);
public record struct TransitionCheckerResponseDto(bool valid);
