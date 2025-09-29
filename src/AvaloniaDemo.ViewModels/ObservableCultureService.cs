using System.Globalization;
using System.Reactive;
using System.Reactive.Subjects;

namespace AvaloniaDemo.ViewModels;

[UsedImplicitly]
public class ObservableCultureService : ISingletonDependency
{
	public required IStringLocalizer<AvaloniaDemoResource> L { get; init; }

	public IObservable<Unit> CultureChanged => _observableCultureChanged;

	private readonly BehaviorSubject<Unit> _observableCultureChanged = new(default);

	public void ChangeCulture(CultureInfo culture)
	{
		CultureInfo.CurrentCulture = culture;
		CultureInfo.CurrentUICulture = culture;
		_observableCultureChanged.OnNext(default);
	}
}
