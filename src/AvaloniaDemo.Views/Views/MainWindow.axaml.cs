namespace AvaloniaDemo.Views.Views;

[UsedImplicitly]
public partial class MainWindow : ReactiveWindow<AppViewModel>, ISingletonDependency
{
	public MainWindow()
	{
		InitializeComponent();
	}
}
