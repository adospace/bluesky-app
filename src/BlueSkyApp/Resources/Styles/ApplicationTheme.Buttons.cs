using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.Controls.VisualStateManager;

namespace BlueSkyApp.Resources.Styles;

partial class ApplicationTheme
{
    public static partial class Selector
    {
        public static class Buttons
        {
            public static string Primary = "PrimaryButton";
        }
    }

    static void ApplyButtonsStyles()
    {
        ButtonStyles.Themes[Selector.Buttons.Primary] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(16)
            .CornerRadius(9999)
            .FontAttributes(MauiControls.FontAttributes.Bold)
            .VisualState(nameof(CommonStates), nameof(CommonStates.Normal), MauiControls.VisualElement.BackgroundColorProperty, Colors.Semantic.AccentModerate)
            .VisualState(nameof(CommonStates), nameof(CommonStates.Normal), MauiControls.Button.TextColorProperty, Colors.Semantic.AccentOnAccent)
            .VisualState(nameof(CommonStates), nameof(CommonStates.Disabled), MauiControls.VisualElement.BackgroundColorProperty, Colors.Semantic.BgDisabled)
            .VisualState(nameof(CommonStates), nameof(CommonStates.Disabled), MauiControls.Button.TextColorProperty, Colors.Semantic.FgDisabled)
            ;
    }
}
