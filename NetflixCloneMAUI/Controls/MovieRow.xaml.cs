using NetflixCloneMAUI.Models;
using NetflixCloneMAUI.ViewModels;
using System.Windows.Input;

namespace NetflixCloneMAUI.Controls;

public class MediaSelectEventArgs : EventArgs
{
    public Media Media { get; set; }

    public MediaSelectEventArgs(Media media) => Media = media;
}

public partial class MovieRow : ContentView
{
	public static readonly BindableProperty HeadingProperty =
			BindableProperty.Create(nameof(Heading), typeof(string), typeof(MovieRow), string.Empty);

	public static readonly BindableProperty MoviesProperty =
			BindableProperty.Create(nameof(Movies), typeof(IEnumerable<Media>), typeof(MovieRow), Enumerable.Empty<Media>());

	public static readonly BindableProperty IsLargeProperty =
			BindableProperty.Create(nameof(IsLarge), typeof(bool), typeof(MovieRow), false);
    
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MovieRow), default(ICommand));
    
    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(Object), typeof(MovieRow), default(Object));

    public MovieRow()
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
		get => (string)GetValue(MovieRow.HeadingProperty);
		set => SetValue(MovieRow.HeadingProperty, value);
	}
	public IEnumerable<Media> Movies
    {
		get => (IEnumerable<Media>)GetValue(MovieRow.MoviesProperty);
		set => SetValue(MovieRow.MoviesProperty, value);
	}
	public bool IsLarge
    {
		get => (bool)GetValue(MovieRow.IsLargeProperty);
		set => SetValue(MovieRow.IsLargeProperty, value);
	}

	public bool IsNotLarge => !IsLarge;

    public ICommand MediaDetailsCommand { get; private set; }
    private void ExecuteMediaDetailsCommand(object parameter)
    {
        CommandParameter = parameter;
        Command?.Execute(CommandParameter);
    }
}