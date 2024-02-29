using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MouseTrackingV2.Utils
{
    public class GenerateHeatmap
    {
        private List<Point> coordinates = new List<Point>
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

        private int width = 1200; // Set the width of the image
        private int height = 1000; // Set the height of the image

        public void GenerateHeatmapImage()
        {
            using (Bitmap heatmap = GenerateHeatmapAction(coordinates, width, height))
            {
                heatmap.Save("heatmap.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private Bitmap GenerateHeatmapAction(List<Point> coordinates, int width, int height)
        {
            Bitmap heatmap = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(heatmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                foreach (Point point in coordinates)
                {
                    if (point.X != 0 || point.Y != 0) // Ignore coordinates (0, 0)
                    {
                        int intensity = CalculateIntensity(point);
                        Color color = GetColor(intensity);

                        using (SolidBrush brush = new SolidBrush(color))
                        {
                            g.FillRectangle(brush, point.X, point.Y, 10, 10); // Adjust the rectangle size as needed
                        }
                    }
                }
            }

            return heatmap;
        }

        private int CalculateIntensity(Point point)
        {
            // You can implement your own logic to calculate intensity based on the coordinates.
            // For simplicity, this example uses the sum of X and Y coordinates.
            return point.X + point.Y;
        }

        private Color GetColor(int intensity)
        {
            // You can adjust this method to map intensity to a color gradient of your choice.
            // For simplicity, this example uses a grayscale gradient.
            int colorValue = Math.Min(255, intensity); // Cap the value at 255
            return Color.FromArgb(colorValue, colorValue, colorValue);
        }
    }
}
