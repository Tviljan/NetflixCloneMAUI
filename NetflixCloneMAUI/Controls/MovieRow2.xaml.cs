using NetflixCloneMAUI.Models;
using NetflixCloneMAUI.ViewModels;
using System.Windows.Input;

namespace NetflixCloneMAUI.Controls;

public partial class MovieRow2 : ContentView
{
	public static readonly BindableProperty HeadingProperty =
			BindableProperty.Create(nameof(Heading), typeof(string), typeof(MovieRow2), string.Empty);

	public static readonly BindableProperty MoviesProperty =
			BindableProperty.Create(nameof(Movies), typeof(IEnumerable<Media>), typeof(MovieRow2), Enumerable.Empty<Media>());

	public static readonly BindableProperty IsLargeProperty =
			BindableProperty.Create(nameof(IsLarge), typeof(bool), typeof(MovieRow2), false);
    
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MovieRow2), default(ICommand));
    
    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(Object), typeof(MovieRow2), default(Object));

    public MovieRow2()
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
		get => (string)GetValue(MovieRow2.HeadingProperty);
		set => SetValue(MovieRow2.HeadingProperty, value);
	}
	public IEnumerable<Media> Movies
    {
		get => (IEnumerable<Media>)GetValue(MovieRow2.MoviesProperty);
		set => SetValue(MovieRow2.MoviesProperty, value);
	}
	public bool IsLarge
    {
		get => (bool)GetValue(MovieRow2.IsLargeProperty);
		set => SetValue(MovieRow2.IsLargeProperty, value);
	}

	public bool IsNotLarge => !IsLarge;

    public ICommand MediaDetailsCommand { get; private set; }
    private void ExecuteMediaDetailsCommand(object parameter)
    {
        CommandParameter = parameter;
        Command?.Execute(CommandParameter);
    }
}