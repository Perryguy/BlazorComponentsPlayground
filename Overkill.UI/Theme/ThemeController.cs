using Microsoft.JSInterop;

namespace Overkill.UI.Theme;

public class ThemeController
{
    private readonly OverkillThemeProvider _p;
    internal ThemeController(OverkillThemeProvider provider)
    {
        _p = provider;
    }

    public ThemeMode Mode => _p.Mode;
    public BrandFamily Brand => _p.Brand;
    public NeutralFamily Neutral => _p.Neutral;
    public Preset Preset => _p.Preset;
    public Density Density => _p.Density;

    public event Action? Changed;

    public void SetMode(ThemeMode m) { _p.Mode = m; _p.NotifyChanged(); Changed?.Invoke(); }
    public void ToggleDark()
    {
        _p.Mode = _p.Mode == ThemeMode.Dark ? ThemeMode.Light :
                  _p.Mode == ThemeMode.Light ? ThemeMode.Dark : ThemeMode.Dark;
        _p.NotifyChanged();
        Changed?.Invoke();
    }
    public void SetBrand(BrandFamily v) { _p.Brand = v; _p.NotifyChanged(); Changed?.Invoke(); }
    public void SetNeutral(NeutralFamily v) { _p.Neutral = v; _p.NotifyChanged(); Changed?.Invoke(); }
    public void SetPreset(Preset v) { _p.Preset = v; _p.NotifyChanged(); Changed?.Invoke(); }
    public void SetDensity(Density v) { _p.Density = v; _p.NotifyChanged(); Changed?.Invoke(); }

    // Called by JS when OS theme changes (Mode == System)
    [JSInvokable("Overkill_OnSystemModeChanged")]
    public void OnSystemModeChanged(string mode) // "light" or "dark"
    {
        _p.UpdateResolved(mode);
        Changed?.Invoke();
    }
}
