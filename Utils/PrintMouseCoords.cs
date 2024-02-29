using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MouseTrackingV2.Utils
{
    public static class PrintMouseCoords
    {
        public static void GenerateImageWithMouseCoords(int[] xCoords, int[] yCoords, string outputPath)
        {
            if (xCoords.Length != yCoords.Length)
            {
                throw new ArgumentException("xCoords and yCoords arrays must have the same length.");
            }

            // Set up image size and other parameters
            int imageWidth = 800; // Adjust as needed
            int imageHeight = 600; // Adjust as needed
            Color dotColor = Color.Red; // Color of the dots
            int dotRadius = 5; // Radius of the dots

            // Create a bitmap with a white background
            using (Bitmap bitmap = new Bitmap(imageWidth, imageHeight))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                // Draw circles at each mouse coordinate
                for (int i = 0; i < xCoords.Length; i++)
                {
                    int xCoord = xCoords[i];
                    int yCoord = yCoords[i];

                    // Calculate the position to draw the circle
                    int xPos = Math.Max(0, Math.Min(imageWidth - 1, xCoord - dotRadius));
                    int yPos = Math.Max(0, Math.Min(imageHeight - 1, yCoord - dotRadius));

                    // Draw a filled circle
                    g.FillEllipse(new SolidBrush(dotColor), xPos, yPos, 2 * dotRadius, 2 * dotRadius);
                }

                // Save the bitmap to the specified file path
                bitmap.Save(outputPath, ImageFormat.Jpeg);
            }
        }
    }
}
