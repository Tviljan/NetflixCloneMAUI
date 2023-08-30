using NetflixCloneMAUI.Models;
using NetflixCloneMAUI.ViewModels;
using System.Windows.Input;

namespace NetflixCloneMAUI.Controls;

public partial class MovieRowXaml : ContentView
{
	public static readonly BindableProperty HeadingProperty =
			BindableProperty.Create(nameof(Heading), typeof(string), typeof(MovieRowXaml), string.Empty);

	public static readonly BindableProperty MoviesProperty =
			BindableProperty.Create(nameof(Movies), typeof(IEnumerable<Media>), typeof(MovieRowXaml), Enumerable.Empty<Media>());

	public static readonly BindableProperty IsLargeProperty =
			BindableProperty.Create(nameof(IsLarge), typeof(bool), typeof(MovieRowXaml), false);
    
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MovieRowXaml), default(ICommand));
    
    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(Object), typeof(MovieRowXaml), default(Object));

    public MovieRowXaml()
	{
		InitializeComponent();
        MediaDetailsCommand = new Command(ExecuteMediaDetailsCommand);
    }
    public Object CommandParameter
    {
        get => (Object)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public string Heading
    {
		get => (string)GetValue(MovieRowXaml.HeadingProperty);
		set => SetValue(MovieRowXaml.HeadingProperty, value);
	}
	public IEnumerable<Media> Movies
    {
		get => (IEnumerable<Media>)GetValue(MovieRowXaml.MoviesProperty);
		set => SetValue(MovieRowXaml.MoviesProperty, value);
	}
	public bool IsLarge
    {
		get => (bool)GetValue(MovieRowXaml.IsLargeProperty);
		set => SetValue(MovieRowXaml.IsLargeProperty, value);
	}

	public bool IsNotLarge => !IsLarge;

    public ICommand MediaDetailsCommand { get; private set; }
    private void ExecuteMediaDetailsCommand(object parameter)
    {
        CommandParameter = parameter;
        Command?.Execute(CommandParameter);
    }
}