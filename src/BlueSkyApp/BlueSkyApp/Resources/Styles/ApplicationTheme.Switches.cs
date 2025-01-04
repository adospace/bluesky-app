using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Resources.Styles;

partial class ApplicationTheme
{
    public static ContentView Switch(bool toggle, Action<bool> onToggle, bool large = false)
    {
        return Component.ContentView(
            Component.Render(state =>
            {
                return Component.Border(
                    Component.Border()
                        .BackgroundColor(Colors.Semantic.AccentOnAccent)
                        .Padding(2)
                        .Width(large ? 28 : 20)
                        .Height(large ? 28 : 20)
                        .StrokeCornerRadius(9999)
                        .TranslationX(!state.Value ? (large ? -11 : -8) : (large ? 11 : 8))
                        .WithAnimation(duration: 200)
                    )
                    .OnTapped(() => onToggle(!toggle))
                    .Width(large ? 56 : 40)
                    .Height(large ? 32 : 24)
                    .StrokeCornerRadius(9999)
                    .BackgroundColor(state.Value ? Colors.Semantic.AccentModerate : Colors.Semantic.BgInteractivePrimary)
                    .WithAnimation();
            }, toggle)
        );
    }
}
