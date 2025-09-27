using System.Text;

namespace Overkill.UI.Theme;

public enum ThemeMode { Light, Dark, System }
public enum BrandFamily
{
    Red, Orange, Amber, Yellow, Lime, Green, Emerald, Teal, Cyan, Sky, Blue,
    Indigo, Violet, Purple, Fuchsia, Pink, Rose
}
public enum NeutralFamily
{
    None, Neutral, Gray, Zinc, Slate, Stone,
    Graphite, Charcoal, Ink, Sand, Greige, Taupe, Cocoa // extra neutrals
}
//public enum Preset { None, VibrantIndigo, TurboSunset, GlassTeal, PaperTint }

public enum Preset
{
    None, VibrantIndigo, TurboSunset, GlassTeal, PaperTint,
    ProSlate, MidnightGlass, CobaltWave, WarmSand, ExecutiveCharcoal
}
public enum Density { None, _100, _200, _300 }

internal static class ThemeEnumExtensions
{
    public static string ToCssClass(this BrandFamily b)
    {
        return "ok-brand-" + b.ToString().ToLowerInvariant();
    }

    public static string ToCssClass(this NeutralFamily n)
    {
        return n == NeutralFamily.None ? "" : "ok-neutral-" + n.ToString().ToLowerInvariant();
    }

    public static string ToCssClass(this Preset p)
    {
        return p == Preset.None ? "" : "ok-preset-" + ToKebabCase(p.ToString());
    }

    public static string ToCssClass(this Density d)
    {
        return d == Density.None ? "" : "ok-density-" + d.ToString().TrimStart('_').ToLowerInvariant();
    }

    private static string ToKebabCase(string s)
    {
        var sb = new StringBuilder(s.Length + 4);
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            if (char.IsUpper(c) && i > 0)
                sb.Append('-');
            sb.Append(char.ToLowerInvariant(c));
        }
        return sb.ToString(); // VibrantIndigo -> vibrant-indigo
    }
}
