using System.Windows.Input;
using Microsoft.Maui.Controls.Shapes;
using NetflixCloneMAUI.Models;
using CommunityToolkit.Maui.Markup;

namespace NetflixCloneMAUI.Controls
{
    public class MovieRow : ContentView
    {

        public static readonly BindableProperty HeadingProperty =
            BindableProperty.Create(nameof(Heading), typeof(string), typeof(MovieRow), string.Empty);

        public static readonly BindableProperty MoviesProperty =
            BindableProperty.Create(nameof(Movies), typeof(IEnumerable<Media>), typeof(MovieRow2), Enumerable.Empty<Media>());

        public static readonly BindableProperty IsLargeProperty =
            BindableProperty.Create(nameof(IsLarge), typeof(bool), typeof(MovieRow), false);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MovieRow), default(ICommand));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(Object), typeof(MovieRow), default(Object));

        static void OnIsExpandedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Property changed implementation goes here
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

        public MovieRow()
        {
            BindingContext = this;

            VerticalStackLayout stackLayout = new VerticalStackLayout
            {
                BackgroundColor = Colors.Black
            };
            stackLayout.BindingContext = this;

            Label headingLabel = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                HorizontalTextAlignment = TextAlignment.Start,
                Margin = new Thickness(10, 15, 0, 5)
            };
            headingLabel.SetBinding(Label.TextProperty, new Binding("Heading"));

            var collectionView = new CollectionView().ItemsSource(Movies);
            collectionView.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
            {
                ItemSpacing = 5
            };

            collectionView.ItemTemplate = new DataTemplate(() =>
            {

                Border border = new Border
                {
                    StrokeShape = new RoundRectangle(),
                    Stroke = Colors.Red,
                    StrokeThickness = 1
                };
                
                Grid grid = new Grid();

                Label lbl = new Label()
                {
                    HeightRequest = 150,
                    WidthRequest = 120
                };
                lbl.SetBinding(Label.TextProperty, new Binding("DisplayTitle", source: this));
              
                grid.Add(lbl);

                border.Content = grid;

                return border;
            });

            stackLayout.Add(headingLabel);
            stackLayout.Add(collectionView);

            Content = stackLayout;

        }
        private void ExecuteMediaDetailsCommand(object parameter)
        {
            CommandParameter = parameter;
            Command?.Execute(CommandParameter);
        }
    }
}