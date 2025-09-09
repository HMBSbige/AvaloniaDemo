namespace AvaloniaDemo.Views;

[UsedImplicitly]
public partial class MainWindow : ReactiveWindow<AppViewModel>, ISingletonDependency
{
	public MainWindow()
	{
		InitializeComponent();
	}
}
