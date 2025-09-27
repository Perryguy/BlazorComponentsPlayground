using Bunit;

// Aliases for nested enums (Spinner.SpinnerVariant / Spinner.SpinnerLabelPosition)
using Var = Overkill.UI.Spinner.SpinnerVariant;
using LabPos = Overkill.UI.Spinner.SpinnerLabelPosition;
using Overkill.UI.Tests.Utils;

namespace Overkill.UI.Tests.SpinnerTests;

/// <summary>
/// Spinner tests using small helpers to avoid selector & style noise.
/// </summary>
public class SpinnerTests : SpinnerTestBase
{
    [Fact]
    public void DefaultRenderIsRingWithA11yAndStyleVars()
    {
        var cut = RenderSpinner();
        var root = cut.Root();

        // classes
        Assert.True(root.HasClass("ok-spinner"));
        Assert.True(root.HasClass("ok-ring"));

        // a11y
        Assert.Equal("status", root.Attr("role"));
        Assert.Equal("Loading", root.Attr("aria-label"));
        Assert.Equal("polite", root.Attr("aria-live"));
        Assert.Equal("true", root.Attr("aria-busy"));

        // style vars (parsed once, assert keys)
        var vars = root.StyleVars();
        Assert.Equal("2.5rem", vars["--ok-size-w"]);
        Assert.Equal("2.5rem", vars["--ok-size-h"]);
        Assert.Equal(".3rem", vars["--ok-thickness"]);
        Assert.Equal("currentColor", vars["--ok-color"]);
        Assert.Equal("rgba(0,0,0,.12)", vars["--ok-track"]);
        Assert.Equal("1s", vars["--ok-speed"]);
    }

    [Fact]
    public void VariantDotsRendersThreeDots()
    {
        var cut = RenderSpinner(p => p.Add(x => x.Variant, Var.Dots));
        var root = cut.Root();
        Assert.True(root.HasClass("ok-dots"));
        Assert.Equal(3, cut.FindAll(".ok-dot").Count);
    }

    [Fact]
    public void VariantBarsRendersFiveBars()
    {
        var cut = RenderSpinner(p => p.Add(x => x.Variant, Var.Bars));
        var root = cut.Root();
        Assert.True(root.HasClass("ok-bars"));
        Assert.Equal(5, cut.FindAll(".ok-bar").Count);
    }

    [Fact]
    public void VariantDualringHasClass()
    {
        var cut = RenderSpinner(p => p.Add(x => x.Variant, Var.Dualring));
        Assert.True(cut.Root().HasClass("ok-dualring"));
    }

    [Fact]
    public void WidthAndHeightOverrideSize()
    {
        var cut = RenderSpinner(p => p
            .Add(x => x.Size, "4rem")
            .Add(x => x.Width, "50px")
            .Add(x => x.Height, "60px"));

        var vars = cut.Root().StyleVars();
        Assert.Equal("50px", vars["--ok-size-w"]);
        Assert.Equal("60px", vars["--ok-size-h"]);
        Assert.True(vars.ContainsKey("--ok-thickness"));
    }

    [Fact]
    public void ColorTrackSpeedAppearInStyleVars()
    {
        var cut = RenderSpinner(p => p
            .Add(x => x.Color, "tomato")
            .Add(x => x.TrackColor, "#eee")
            .Add(x => x.Speed, ".7s"));

        var vars = cut.Root().StyleVars();
        Assert.Equal("tomato", vars["--ok-color"]);
        Assert.Equal("#eee", vars["--ok-track"]);
        Assert.Equal(".7s", vars["--ok-speed"]);
    }

    [Fact]
    public void PausedAddsClassOnRoot()
    {
        var cut = RenderSpinner(p => p.Add(x => x.Paused, true));
        Assert.True(cut.Root().HasClass("ok-paused"));
    }

    [Fact]
    public void LabelHiddenByDefault()
    {
        var cut = RenderSpinner();
        Assert.Empty(cut.FindAll(".ok-label"));
        Assert.Empty(cut.FindAll(".ok-sr"));
    }

    [Fact]
    public void LabelSrOnlyRendersScreenReaderLabel()
    {
        var cut = RenderSpinner(p => p
            .Add(x => x.LabelPosition, LabPos.SrOnly)
            .Add(x => x.VisibleLabel, "Fetching…"));

        var sr = cut.Find(".ok-sr");
        Assert.Equal("Fetching…", sr.TextContent);
    }

    [Fact]
    public void LabelBeforeAndAfterRender()
    {
        var before = RenderSpinner(p => p
            .Add(x => x.LabelPosition, LabPos.Before)
            .Add(x => x.VisibleLabel, "Loading A"));
        Assert.Equal("Loading A", before.Find(".ok-label").TextContent);

        var after = RenderSpinner(p => p
            .Add(x => x.LabelPosition, LabPos.After)
            .Add(x => x.VisibleLabel, "Loading B"));
        Assert.Contains("Loading B", after.Markup);
    }

    [Fact]
    public void ClassMergesAndAdditionalAttributesPassThrough()
    {
        var cut = RenderSpinner(p => p
            .Add(x => x.Class, "extra")
            .AddUnmatched("data-extra", "123"));

        var root = cut.Root();
        Assert.True(root.HasClass("extra"));
        Assert.Equal("123", root.Attr("data-extra"));
    }

    [Fact]
    public void CanOverrideRoleLiveLabel()
    {
        var cut = RenderSpinner(p => p
            .Add(x => x.Role, "progressbar")
            .Add(x => x.AriaLive, "assertive")
            .Add(x => x.AriaLabel, "Working…"));

        var root = cut.Root();
        Assert.Equal("progressbar", root.Attr("role"));
        Assert.Equal("assertive", root.Attr("aria-live"));
        Assert.Equal("Working…", root.Attr("aria-label"));
    }
}
