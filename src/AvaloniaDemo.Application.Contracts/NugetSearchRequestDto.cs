using Volo.Abp.Application.Dtos;

namespace AvaloniaDemo.Application.Contracts;

public class NugetSearchRequestDto : PagedResultRequestDto
{
	public string? SearchTerm { get; set; }
}
