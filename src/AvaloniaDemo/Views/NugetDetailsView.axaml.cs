using Avalonia.Media.Imaging;
using System.Reactive.Disposables;

namespace AvaloniaDemo.Views;

public partial class NugetDetailsView : ReactiveUserControl<NugetDetailsViewModel>, ITransientDependency
{
	public NugetDetailsView()
	{
		InitializeComponent();

		this.WhenActivated(d =>
		{
			this.OneWayBind(ViewModel,
					vm => vm.Icon,
					v => v.IconImage.Source,
					bytes =>
					{
						if (bytes is null)
						{
							return default;
						}

						using MemoryStream ms = new(bytes);
						return new Bitmap(ms);
					})
				.DisposeWith(d);
		});
	}
}
