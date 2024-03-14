using System.Drawing;

public static class Utils {
    public static double Remap(double value, double from1, double to1, double from2, double to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    public static Color Remap(ulong frequency, ulong lowest, ulong highest, Color colorFrom, Color colorTo) {
        return Color.FromArgb(
            (int)Remap(frequency, lowest, highest, colorFrom.A, colorTo.A), 
            (int)Remap(frequency, lowest, highest, colorFrom.R, colorTo.R), 
            (int)Remap(frequency, lowest, highest, colorFrom.G, colorTo.G), 
            (int)Remap(frequency, lowest, highest, colorFrom.B, colorTo.B));
    }

    public static Color?[,] ColorMatrixCreate(uint width, uint height) {
        var rv = new Color?[width,height];
        return rv;
    }
    public static Color MedianColor(Color color1, Color? color2) {
        if (color2 == null) return color1;
        else {
            var otherColor = (Color)color2;
            return Color.FromArgb(
                (color1.A + otherColor.A) / 2,
                (color1.R + otherColor.R) / 2,
                (color1.G + otherColor.G) / 2,
                (color1.B + otherColor.B) / 2
            );
        }
    }
    public static Color ColorBlend(Color foreground, Color background, float blendFactor)
    {
        return Color.FromArgb(255,
            (int)(background.R * (1f - blendFactor) + foreground.R * blendFactor),
            (int)(background.G * (1f - blendFactor) + foreground.G * blendFactor),
            (int)(background.B * (1f - blendFactor) + foreground.B * blendFactor));
    }
}