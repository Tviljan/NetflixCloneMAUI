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
            //collectionView.SetBinding(CollectionView.ItemsSourceProperty, "Movies", BindingMode.TwoWay);
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

                Label lbl = new Label
                {
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(10, 15, 0, 5)
                };
                lbl.Bind(Label.TextProperty, Binding.SelfPath);
                border.Content = lbl;
                return border;
            });

            /*
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty,
                new Binding("MediaDetailsCommand", source: this));
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, ".");

            border.GestureRecognizers.Add(tapGestureRecognizer);

            Grid grid = new Grid();

            Image smallImage = new Image
            {
                Aspect = Aspect.AspectFill,
                HeightRequest = 150,
                WidthRequest = 120
            };
            smallImage.SetBinding(IsVisibleProperty, new Binding("IsNotLarge", source: this));
            smallImage.Source = new UriImageSource
            {
                Uri = new Uri("ThumbnailSmall")
            };

            Image largeImage = new Image
            {
                Aspect = Aspect.AspectFill,
                HeightRequest = 200,
                WidthRequest = 150
            };
            largeImage.SetBinding(IsVisibleProperty, new Binding("IsLarge", source: this));
            largeImage.Source = new UriImageSource
            {
                Uri = new Uri("ThumbnailSmall")
            };

            grid.Add(smallImage);
            grid.Add(largeImage);

            border.Content = grid;

            return border;
        });
        */
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