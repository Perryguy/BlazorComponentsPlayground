using AngleSharp.Dom;
using Bunit;

namespace Overkill.UI.Tests.SpinnerTests;

/// <summary>
/// Shared helpers for Spinner tests. Inherit from this instead of TestContext.
/// </summary>
public abstract class SpinnerTestBase : Bunit.TestContext
{
    // Single source of truth for the root selector / test id
    public static readonly string RootSelector = "[data-testid='ok-spinner']";

    /// <summary>
    /// Renders a Spinner with optional parameter builder.
    /// Usage: var cut = RenderSpinner(p => p.Add(x => x.Size, "4rem"));
    /// </summary>
    protected IRenderedComponent<Spinner> RenderSpinner(
       Action<ComponentParameterCollectionBuilder<Spinner>>? parameters = null)
    {
        return parameters is null
               ? RenderComponent<Spinner>()
               : RenderComponent(parameters);
    }

    /// <summary>
    /// Shorthand for finding the spinner root element.
    /// </summary>
    public static IElement RootOf(IRenderedFragment cut)
    {
        return cut.Find(RootSelector);
    }

    /// <summary>
    /// Parses inline style into a dictionary; keys are lowercase and trimmed.
    /// Works well for asserting CSS custom properties.
    /// </summary>
    public static Dictionary<string, string> ParseStyle(string? style)
    {
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (string.IsNullOrWhiteSpace(style))
            return map;
        foreach (var pair in style.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var idx = pair.IndexOf(':');
            if (idx > 0)
                map[pair[..idx].Trim()] = pair[(idx + 1)..].Trim();
        }
        return map;
    }
}
