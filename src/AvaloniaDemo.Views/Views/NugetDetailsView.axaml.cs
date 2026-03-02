namespace AvaloniaDemo.Views.Views;

public partial class NugetDetailsView : ReactiveUserControl<NugetDetailsViewModel>, ITransientDependency
{
	public NugetDetailsView()
	{
		InitializeComponent();

		this.WhenActivated(d =>
		{
			OpenProjectWeb.Bind(ContentProperty, new ObservableStringLocalizer(nameof(OpenProjectWeb))).DisposeWith(d);
		});
	}
}
