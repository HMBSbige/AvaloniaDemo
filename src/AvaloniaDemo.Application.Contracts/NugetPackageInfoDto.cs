using Volo.Abp.Application.Dtos;

namespace AvaloniaDemo.Application.Contracts;

public class NugetPackageInfoDto : EntityDto
{
	public string? Title { get; set; }

	public string? Description { get; set; }

	public Uri? IconUrl { get; set; }

	public Uri? ProjectUrl { get; set; }
}
