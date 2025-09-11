using Volo.Abp.Application.Services;

namespace AvaloniaDemo.Application;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class NugetSearchAppService : ApplicationService
{
	public async Task<IEnumerable<NugetPackageInfoDto>> SearchAsync(NugetSearchRequestDto request, CancellationToken cancellationToken = default)
	{
		SourceRepository repository = LazyServiceProvider.GetRequiredService<SourceRepository>();

		PackageSearchResource resource = await repository.GetResourceAsync<PackageSearchResource>(cancellationToken);

		IEnumerable<IPackageSearchMetadata> metadata = await resource.SearchAsync(request.SearchTerm, new SearchFilter(false), request.SkipCount, request.MaxResultCount, NuGet.Common.NullLogger.Instance, cancellationToken);

		return metadata.Select(x => new NugetPackageInfoDto
		{
			Title = x.Title,
			Description = x.Description,
			IconUrl = x.IconUrl,
			ProjectUrl = x.ProjectUrl
		});
	}

	public async Task<byte[]> DownloadIconAsync(Uri? uri, CancellationToken cancellationToken = default)
	{
		HttpClient httpClient = LazyServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient();

		return await httpClient.GetByteArrayAsync(uri, cancellationToken);
	}
}
