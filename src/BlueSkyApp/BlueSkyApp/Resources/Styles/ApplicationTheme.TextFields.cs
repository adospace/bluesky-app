using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor;

namespace BlueSkyApp.Resources.Styles;

partial class ApplicationTheme
{
    public static partial class TextFields
    {


    }

    static void ApplyTextFieldsStyles()
    {
        // Add your custom TextField styles here
        EntryStyles.Default = _ => _
            .FontFamily("PlusJakartaSansRegular")
            .FontSize(18)
            .TextColor(Colors.Global.Grey100);

        EntryStyles.Themes[Selector.Typo.LabelMdRegular] = _ => _
            .PlaceholderColor(Colors.Semantic.FgSubtle)
            .FontSize(16)
            .TextColor(Colors.Semantic.FgBase);
    }

    public static Border TextField(string text, Action<string> textChanged, string? placeholder = null, bool isPassword = false)
        => Component.Border(
            Component.Entry()
                .ThemeKey(Selector.Typo.LabelMdRegular)
                .Text(text)
                .OnTextChanged(textChanged)
                .IsPassword(isPassword)
                .Placeholder(placeholder ?? string.Empty)
        )
        .Padding(16, 4)
        .StrokeCornerRadius(8)
        .Stroke(Brushes.Semantic.BorderSubtle);

    public static Grid TextField(string label, string text, Action<string> textChanged, string? placeholder = null, bool isPassword = false, string? caption = null)
        => Component.Grid("Auto,*,Auto", "*", 
                Component.Label(label)
                    .ThemeKey(Selector.Typo.LabelMdBold)
                    .TextColor(Colors.Semantic.FgBase),

                Component.Border(
                    Component.Entry()
                        .ThemeKey(Selector.Typo.LabelMdRegular)
                        .Text(text)
                        .OnTextChanged(textChanged)
                        .IsPassword(isPassword)
                        .Placeholder(placeholder ?? string.Empty)
                )
                .Padding(16, 4)
                .StrokeCornerRadius(8)
                .Stroke(Brushes.Semantic.BorderSubtle)
                .GridRow(1),

                caption == null ? null :
                Component.Label(caption)
                    .ThemeKey(Selector.Typo.BodySm)
                    .TextColor(Colors.Semantic.FgSubtle)
                    .GridRow(2)
        )
        .RowSpacing(10);
}
