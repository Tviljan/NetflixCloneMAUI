using System.Windows.Input;
using Microsoft.Maui.Controls.Shapes;
using NetflixCloneMAUI.Models;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;

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
            MediaDetailsCommand = new Command(ExecuteMediaDetailsCommand);
            
            VerticalStackLayout layout = new VerticalStackLayout
            {
                BackgroundColor = Colors.Black,
                BindingContext = this
            };

            Label headingLabel = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                HorizontalTextAlignment = TextAlignment.Start,
                Margin = new Thickness(10, 15, 0, 5)
            };

            headingLabel.SetBinding(Label.TextProperty, new Binding("Heading"));
            CollectionView collectionView = new CollectionView();

            collectionView.SetBinding(CollectionView.ItemsSourceProperty, "Movies", BindingMode.TwoWay);
            collectionView.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
            {
                ItemSpacing = 5
            };
            collectionView.ItemTemplate = new DataTemplate(() =>
            {
                Border border = new Border
                {
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = 5
                    },
                    Stroke = Colors.Red,
                    StrokeThickness = 1
                };

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Command = MediaDetailsCommand;
                tapGestureRecognizer.Bind(TapGestureRecognizer.CommandParameterProperty, ".");
            

                border.GestureRecognizers.Add(tapGestureRecognizer);

                Grid grid = new Grid();

                //Instead of binding IsLarge or IsNotLarge (largeImage.Bind(IsVisibleProperty, "IsLarge", source: this);) we can just check if IsLarge
                if (IsLarge)
                {
                    Image largeImage = new Image
                    {
                        Aspect = Aspect.AspectFill,
                        HeightRequest = 200,
                        WidthRequest = 150
                    };
                    //largeImage.Bind(IsVisibleProperty, "IsLarge", source: this);
                    largeImage.Bind(Image.SourceProperty, "ThumbnailSmall");
                    grid.Add(largeImage);
            
                }
                else
                {

                    Image smallImage = new Image
                    {
                        Aspect = Aspect.AspectFill,
                        HeightRequest = 150,
                        WidthRequest = 120
                    };
                    //smallImage.Bind(IsVisibleProperty, "IsNotLarge", source: this);
                    smallImage.Bind(Image.SourceProperty, "ThumbnailSmall");

                    grid.Add(smallImage);
                }
              
                border.Content = grid;

                return border;
            });

            layout.Children.Add(headingLabel);
            layout.Children.Add(collectionView);

            Content = layout;
        }

        private void ExecuteMediaDetailsCommand(object parameter)
        {
            CommandParameter = parameter;
            Command?.Execute(CommandParameter);
        }
    }
}