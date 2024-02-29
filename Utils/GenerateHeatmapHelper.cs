using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MouseTrackingV2.Utils
{
    public class GenerateHeatmapHelper
    {
        public static Bitmap GenerateHeatmap(List<System.Drawing.Point> coordinates, int width, int height)
        {
            Bitmap heatmap = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(heatmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                foreach (System.Drawing.Point point in coordinates)
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


        private static int CalculateIntensity(System.Drawing.Point point)
        {
            // You can implement your own logic to calculate intensity based on the coordinates.
            // For simplicity, this example uses the sum of X and Y coordinates.
            return (int)point.X + (int)point.Y;
        }

        private static Color GetColor(int intensity)
        {
            // You can adjust this method to map intensity to a color gradient of your choice.
            // For simplicity, this example uses a grayscale gradient.
            int colorValue = Math.Min(255, intensity); // Cap the value at 255
            return Color.FromArgb(colorValue, colorValue, colorValue);
        }
    }
}
