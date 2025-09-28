using AvaloniaDemo.Domain.Shared.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reactive;
using System.Reactive.Subjects;

namespace AvaloniaDemo.Extensions;

public class ObservableStringLocalizer(string key) : IObservable<string>
{
	private static readonly IStringLocalizer L = Locator.Current.GetService<IStringLocalizer<AvaloniaDemoResource>>()!;

	private static IObservable<Unit> CultureChanged => ObservableCultureChanged;

	private static readonly BehaviorSubject<Unit> ObservableCultureChanged = new(default);

	public static void ChangeCulture(CultureInfo culture)
	{
		CultureInfo.CurrentCulture = culture;
		CultureInfo.CurrentUICulture = culture;
		ObservableCultureChanged.OnNext(default);
	}

	public IDisposable Subscribe(IObserver<string> observer)
	{
		return CultureChanged.Subscribe(_ => observer.OnNext(L[key]));
	}
}
