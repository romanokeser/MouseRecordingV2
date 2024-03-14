using System.Drawing;
using System.Runtime.Versioning;


public class HeatmapBuilder
{
    const float circleDistanceFactor = 0.95f;
    ulong[,] frequencyMatrix;
    uint width, height;
    Color minColor;
    Color maxColor;
    float blendDrop;
    uint pixelWidth;
    public HeatmapBuilder(List<Coordinate> coordinates, uint screenWidth, uint screenHeight, uint heatmapWidth, uint heatmapHeight)
    {
        width = heatmapWidth;
        height = heatmapHeight;
        frequencyMatrix = new ulong[width, height];
        coordinates.ForEach(coord =>
        {
            var newX = (uint)Utils.Remap(coord.x, 0, screenWidth - 1, 0, heatmapWidth);
            var newY = (uint)Utils.Remap(coord.y, 0, screenHeight - 1, 0, heatmapHeight);
            ++frequencyMatrix[newX, newY];
        });
        pixelWidth = 0;
        blendDrop = 1;
    }

    public HeatmapBuilder AddMinAndMaxColors(Color colorFrom, Color colorTo)
    {
        minColor = colorFrom;
        maxColor = colorTo;
        return this;
    }

    public HeatmapBuilder SetPixelWidth(uint n)
    {
        pixelWidth = n;
        return this;
    }
    public HeatmapBuilder SetBlendDrop(float drop)
    {
        blendDrop = drop;
        return this;
    }

    [SupportedOSPlatform("windows")]
    public Bitmap CreateBitmap()
    {
        var colorMatrix = Utils.ColorMatrixCreate(width, height);
        ulong highest = ulong.MinValue;
        ulong lowest = ulong.MaxValue;
        for (var y = 0; y < height; ++y)
        {
            for (var x = 0; x < width; ++x)
            {
                highest = Math.Max(highest, frequencyMatrix[x, y]);
                lowest = Math.Min(lowest, frequencyMatrix[x, y]);
            }
        }
        for (var y = 0; y < height; ++y)
        {
            for (var x = 0; x < width; ++x)
            {
                if (frequencyMatrix[x, y] != lowest)
                {
                    var preferredColor = Utils.Remap(frequencyMatrix[x, y], lowest, highest, minColor, maxColor);
                    colorMatrix[x, y] = preferredColor;
                    for (var k = -pixelWidth; k <= pixelWidth; ++k)
                    {
                        for (var l = -pixelWidth; l <= pixelWidth; ++l)
                        {
                            if (k == 0 && l == 0) continue;
                            if (x + k < 0 || x + k >= width || y + l < 0 || y + l >= height) continue;

                            float distance = Convert.ToSingle(Math.Sqrt(Math.Pow(k, 2) + Math.Pow(l, 2)));//d=sqrt((x_2-x_1)^2+(y_2-y_1)^2)
                            if (distance > (float)pixelWidth * circleDistanceFactor) continue;
                            //float blendFactor = 1f - (distance / pixelWidth * blendDrop);
                            //colorMatrix[x + k, y + l] = Utils.ColorBlend(preferredColor, minColor, blendFactor);
                            colorMatrix[x + k, y + l] = preferredColor;
                        }
                    }
                    
                }
            }
        }

        var rv = new Bitmap((int)width, (int)height);
        for (var y = 0; y < height; ++y)
        {
            for (var x = 0; x < width; ++x)
            {
                rv.SetPixel(x, y, colorMatrix[x, y] ?? minColor);
            }
        }
        return rv;
    }
}
