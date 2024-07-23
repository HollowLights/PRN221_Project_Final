using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Final_Project_PRN221
{
    /// <summary>
    /// Interaction logic for LuckySpinPage.xaml
    /// </summary>
    public partial class LuckySpinPage : Page
    {
        private readonly string[] prizeImages = { "kBeta.png", "kAlpha.png", "milkTea.png", "coca.png" };
        private readonly string[] prizes = { "Giải Đặc Biệt", "Giải Nhì", "Giải Ba", "Giải Khuyến Khích" };
        private readonly Brush[] prizeColors = { Brushes.Gold, Brushes.Coral, Brushes.LightSeaGreen, Brushes.MediumPurple };
        private const double Radius = 250;
        private const double Angle = 360.0;
        private bool hasSpun = false;
        public LuckySpinPage()
        {
            InitializeComponent();
            DrawWheel();
        }
        private void DrawWheel()
        {
            double angleStep = Angle / prizes.Length;
            double currentAngle = 0;

            for (int i = 0; i < prizes.Length; i++)
            {
                var wedge = new Path
                {
                    Fill = prizeColors[i],
                    Data = CreateWedgeGeometry(currentAngle, angleStep),
                    Opacity = 0.8
                };

                WheelCanvas.Children.Add(wedge);

                // Tạo TextBlock cho mỗi ô phần thưởng
                var text = new TextBlock
                {
                    Text = prizes[i],
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White,
                    RenderTransform = new RotateTransform(currentAngle + angleStep / 2, Radius, Radius),
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    Width = 100,
                    Height = 50
                };

                // Tạo Canvas chứa TextBlock và đặt nó vào giữa từng ô
                var textContainer = new Canvas
                {
                    Width = 100,
                    Height = 50,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Background = Brushes.Transparent
                };

                // Tính toán vị trí của Canvas chứa TextBlock
                Canvas.SetLeft(textContainer, Radius - textContainer.Width / 2);
                Canvas.SetTop(textContainer, Radius - textContainer.Height / 2);
                textContainer.Children.Add(text);
                WheelCanvas.Children.Add(textContainer);

                currentAngle += angleStep;
            }
        }
        private Geometry CreateWedgeGeometry(double startAngle, double angleStep)
        {
            double startRadians = ToRadians(startAngle);
            double endRadians = ToRadians(startAngle + angleStep);

            var wedgeGeometry = new PathGeometry();
            var figure = new PathFigure
            {
                StartPoint = new Point(Radius, Radius)
            };

            figure.Segments.Add(new LineSegment(new Point(
                Radius + Radius * Math.Cos(startRadians),
                Radius - Radius * Math.Sin(startRadians)), true));
            figure.Segments.Add(new ArcSegment(new Point(
                Radius + Radius * Math.Cos(endRadians),
                Radius - Radius * Math.Sin(endRadians)),
                new Size(Radius, Radius),
                0,
                false,
                SweepDirection.Clockwise,
                true));
            figure.Segments.Add(new LineSegment(new Point(Radius, Radius), true));

            wedgeGeometry.Figures.Add(figure);
            return wedgeGeometry;
        }
        private void SpinButton_Click(object sender, RoutedEventArgs e)
        {

            if (hasSpun)
            {
                MessageBox.Show("Bạn đã quay rồi!");
                return;
            }

            var random = new Random();
            double spinAngle = random.Next(360, 1800); // Quay từ 1 đến 5 vòng

            var rotateTransform = new RotateTransform();
            WheelCanvas.RenderTransform = rotateTransform;
            WheelCanvas.RenderTransformOrigin = new Point(0.5, 0.5);

            var animation = new DoubleAnimation
            {
                From = 0,
                To = spinAngle,
                Duration = TimeSpan.FromSeconds(3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            animation.Completed += (s, a) =>
            {
                int prizeIndex = (int)((spinAngle % 360) / (360 / prizes.Length));
                ResultImage.Source = new BitmapImage(new Uri($"pack://application:,,,/Images/{prizeImages[prizeIndex]}"));
                ResultText.Text = $"Bạn trúng: {prizes[prizeIndex]}";
                ResultImage.Visibility = Visibility.Visible;
                ResultText.Visibility = Visibility.Visible;
                hasSpun = true; // Đánh dấu đã quay rồi
                SpinButton.IsEnabled = false; // Vô hiệu hóa nút quay sau khi quay xong
            };

            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
        private double ToRadians(double angle)
        {
            return angle * Math.PI / 180.0;
        }
    }
}
