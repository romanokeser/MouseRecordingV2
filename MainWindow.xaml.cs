using MouseTrackingV2.Utils;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using MessageBox = System.Windows.MessageBox;
using Point = System.Drawing.Point;

namespace MouseTrackingV2
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer recordingTimer;

        public MainWindow()
        {
            InitializeComponent();

            recordingTimer = new DispatcherTimer();
            recordingTimer.Interval = TimeSpan.FromSeconds(0.2f); // Record every second
            recordingTimer.Tick += RecordingTimer_Tick;
        }

        private void RecordingTimer_Tick(object sender, EventArgs e)
        {
            // Retrieve global mouse coordinates
            System.Drawing.Point screenCoordinates = System.Windows.Forms.Cursor.Position;

            // Convert to WPF Point
            System.Windows.Point currentPosition = new System.Windows.Point(screenCoordinates.X, screenCoordinates.Y);

            // Insert mouse coordinates into the database
            DatabaseHelper.InsertMouseCoordinates((int)currentPosition.X, (int)currentPosition.Y);
        }

        private void startRecordButton_Click(object sender, RoutedEventArgs e)
        {
            recordingTimer.Start();
        }

        private void PrintCoordsBtn_Click(object sender, RoutedEventArgs e)
        {
            List<System.Drawing.Point> coordinates = new List<System.Drawing.Point>
            {
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0),
                new Point(337, 158),
                new Point(268, 227),
                new Point(266, 181),
                new Point(69, 89),
                new Point(691, 233),
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0),
                new Point(930, 434),
                new Point(724, 306),
                new Point(1099, 239),
                new Point(1136, 210),
                new Point(1136, 210),
                new Point(485, 413),
                new Point(426, 927),
                new Point(613, 965),
                new Point(998, 952),
                new Point(955, 492),
                new Point(636, 36),
                new Point(644, 0),
                new Point(644, 0),
                new Point(644, 0),
                new Point(284, 0),
                new Point(10, 0),
                new Point(10, 0),
                new Point(10, 0),
                new Point(10, 0)
            };

            int width = 1920; // Set the width of the image
            int height = 1080; // Set the height of the image

            Bitmap heatmapImage = GenerateHeatmapHelper.GenerateHeatmap(coordinates, width, height);

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "heatmapImage.jpg");
            heatmapImage.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            MessageBox.Show($"Heatmap image saved to desktop:\n{filePath}", "Heatmap Saved", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void stopRecBtn_Click(object sender, RoutedEventArgs e)
        {
            recordingTimer.Stop();
            MouseHook.StopTracking();
        }
    }
}
