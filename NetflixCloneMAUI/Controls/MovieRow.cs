﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls.Shapes;
using NetflixCloneMAUI.Models;

namespace NetflixCloneMAUI.Controls
{
	public class MovieRow : ContentView
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
            BindingContext = this;
            InitializeComponent();
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

        private void InitializeComponent()
        {
            VerticalStackLayout stackLayout = new VerticalStackLayout
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
                    StrokeShape = new RoundRectangle(),
                    Stroke = Colors.Black,
                    StrokeThickness = 1
                };

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
            
            stackLayout.Add(headingLabel);
            stackLayout.Add(collectionView);

            Content = stackLayout;
        }
    }
}