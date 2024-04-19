using Xamarin.Forms;

namespace PicApp.Pages
{
    public partial class ImageDetailPage : ContentPage
    {
        public ImageDetailPage(string imagePath, string imageName)
        {
            InitializeComponent();

            var grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var imageView = new Image
            {
                Source = ImageSource.FromFile(imagePath),
                Aspect = Aspect.AspectFill
            };

            var imageInfo = new Label
            {
                Text = imageName,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            grid.Children.Add(imageView, 0, 0);
            grid.Children.Add(imageInfo, 0, 1);

            Content = grid;
        }
    }
}