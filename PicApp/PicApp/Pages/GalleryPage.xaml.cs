using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PicApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PicApp.Pages
{
    public partial class GalleryPage : ContentPage
    {
        private readonly Version androidVersion = DeviceInfo.Version;
        private readonly string[] imageFiles;
        private readonly List<ImageItem> initialImagesList = new List<ImageItem>();
        private readonly string picDirectory = "/storage/emulated/0/DCIM/Camera";
        private Grid footerGrid;
        private Grid imageGrid;

        private Grid mainGrid;
        private Frame selectedFrame;

        public GalleryPage()
        {
            InitializeComponent();

            if (androidVersion.ToString() == "13.0")
                picDirectory = "/storage/emulated/0/Pictures/";

            imageFiles = Directory.GetFiles(picDirectory, "*.jpg");

            InitializeGrids();

            PopulateImageGrid();

            Content = mainGrid;
        }

        private void InitializeGrids()
        {
            mainGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = GridLength.Auto }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            imageGrid = new Grid
            {
                VerticalOptions = LayoutOptions.Start
            };

            footerGrid = new Grid
            {
                IsVisible = false,
                RowDefinitions = { new RowDefinition { Height = GridLength.Auto } },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star }
                }
            };
        }

        private void PopulateImageGrid()
        {
            var row = 0;
            var column = 0;
            foreach (var imagePath in imageFiles)
            {
                var imageName = Path.GetFileNameWithoutExtension(imagePath);

                var image = new Image { Source = imagePath, Aspect = Aspect.AspectFit };
                var imageId = Guid.NewGuid();

                var stackLayout = new StackLayout
                {
                    Children =
                    {
                        image,
                        new Label
                        {
                            Text = imageName,
                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                            HorizontalTextAlignment = TextAlignment.Center
                        }
                    },
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start
                };

                var frame = new Frame
                {
                    Content = stackLayout,
                    BackgroundColor = Color.LightGray,
                    CornerRadius = 5,
                    HasShadow = true,
                    BindingContext = imageId,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) => ImageTapped(frame);
                image.GestureRecognizers.Add(tapGestureRecognizer);

                imageGrid.Children.Add(frame, column, row);

                column++;
                if (column > 2)
                {
                    column = 0;
                    row++;
                }

                initialImagesList.Add(new ImageItem { Id = imageId, ImagePath = imagePath, ImageName = imageName });
            }

            var openButton = new Button
                { Text = "Открыть", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
            openButton.Clicked += OpenImageButton_Clicked;
            var deleteButton = new Button
                { Text = "Удалить", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
            deleteButton.Clicked += DeleteImageButton_Clicked;

            footerGrid.Children.Add(openButton, 0, 0);
            footerGrid.Children.Add(deleteButton, 1, 0);

            mainGrid.Children.Add(imageGrid);
            mainGrid.Children.Add(footerGrid);
            Grid.SetRow(footerGrid, 1);
        }


        private void ImageTapped(Frame frame)
        {
            if (selectedFrame != null)
                selectedFrame.BackgroundColor = Color.LightGray;

            frame.BackgroundColor = Color.Accent;

            selectedFrame = frame;

            footerGrid.IsVisible = true;
        }

        private void OpenImageButton_Clicked(object sender, EventArgs e)
        {
            if (selectedFrame != null)
            {
                var id = (Guid)selectedFrame.BindingContext;

                var selectedItem = initialImagesList.FirstOrDefault(item => item.Id == id);
                if (selectedItem != null)
                    Navigation.PushAsync(new ImageDetailPage(selectedItem.ImagePath, selectedItem.ImageName));
            }
        }

        private void DeleteImageButton_Clicked(object sender, EventArgs e)
        {
            if (selectedFrame != null)
            {
                var id = (Guid)selectedFrame.BindingContext;
                var selectedItem = initialImagesList.FirstOrDefault(item => item.Id == id);
                if (selectedItem != null)
                {
                    try
                    {
                        if (File.Exists(selectedItem.ImagePath)) File.Delete(selectedItem.ImagePath);
                    }
                    catch (Exception ex)
                    {
                        DisplayAlert("Error", $"Error deleting file: {ex.Message}", "OK");
                    }

                    initialImagesList.Remove(selectedItem);

                    imageGrid.Children.Remove(selectedFrame);

                    footerGrid.IsVisible = false;

                    UpdateGridLayout();
                }
            }
        }

        private void UpdateGridLayout()
        {
            int row = 0, column = 0;

            foreach (var child in imageGrid.Children)
            {
                Grid.SetRow(child, row);
                Grid.SetColumn(child, column);

                column++;

                if (column > 2)
                {
                    column = 0;
                    row++;
                }
            }
        }
    }
}