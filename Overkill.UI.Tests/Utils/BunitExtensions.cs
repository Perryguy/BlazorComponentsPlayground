using AngleSharp.Dom;
using Bunit;
using Overkill.UI.Tests.SpinnerTests;

namespace Overkill.UI.Tests.Utils;


/// <summary>
/// Lightweight extension helpers to make assertions terse & readable.
/// </summary>
public static class BunitExtensions
{

    /// <summary>
    /// Checks whether the element has the specified CSS class.
    /// </summary>
    /// <param name="el">The element under test.</param>
    /// <param name="cls">The class name to check (without a leading dot).</param>
    /// <returns><c>true</c> if <paramref name="cls"/> is present in the element's class list; otherwise <c>false</c>.</returns>
    /// <remarks>
    /// Uses AngleSharp's <see cref="IElement.ClassList"/>, which correctly handles whitespace, duplicates, and case sensitivity.
    /// </remarks>
    /// <example>
    /// var root = cut.Find("[data-testid='ok-spinner']");
    /// Assert.True(root.HasClass("ok-ring"));
    /// </example>
    public static bool HasClass(this IElement el, string cls)
    {
        return el.ClassList.Contains(cls);
    }


    /// <summary>
    /// Gets an attribute value from the element, or an empty string if the attribute is missing.
    /// </summary>
    /// <param name="el">The element under test.</param>
    /// <param name="name">The attribute name (e.g., <c>aria-label</c>).</param>
    /// <returns>The attribute value, or <see cref="string.Empty"/> if the attribute does not exist.</returns>
    /// <remarks>
    /// Returning an empty string keeps assertions simple (no null checks) for patterns like <c>Assert.Equal("status", root.Attr("role"))</c>.
    /// </remarks>
    public static string Attr(this IElement el, string name)
    {
        return el.GetAttribute(name) ?? string.Empty;
    }

    /// <summary>
    /// Parses the element's inline <c>style</c> attribute into a dictionary (case-insensitive).
    /// </summary>
    /// <param name="el">The element whose inline styles should be parsed.</param>
    /// <returns>
    /// A dictionary mapping CSS property names to values. Useful for asserting CSS custom properties
    /// like <c>--ok-size-w</c>, <c>--ok-color</c>, etc.
    /// </returns>
    /// <remarks>
    /// Delegates to <see cref="SpinnerTestBase.ParseStyle(string?)"/> so the parsing logic is defined in one place.
    /// </remarks>
    /// <example>
    /// var vars = cut.Root().StyleVars();
    /// Assert.Equal("4rem", vars["--ok-size-w"]);
    /// </example>
    public static Dictionary<string, string> StyleVars(this IElement el)
    {
        return SpinnerTestBase.ParseStyle(el.GetAttribute("style"));
    }

    /// <summary>
    /// Returns the standard spinner root element for the current component under test.
    /// </summary>
    /// <param name="cut">The rendered fragment (component under test).</param>
    /// <returns>The spinner's root <see cref="IElement"/>.</returns>
    /// <exception cref="ElementNotFoundException">Thrown if the root element cannot be found.</exception>
    /// <remarks>
    /// Uses the shared selector defined in <see cref="SpinnerTestBase.RootSelector"/> (currently <c>[data-testid='ok-spinner']</c>).
    /// Centralizing the selector keeps tests DRY and easy to change.
    /// </remarks>
    public static IElement Root(this IRenderedFragment cut)
    {
        return cut.Find(SpinnerTestBase.RootSelector);
    }
}
