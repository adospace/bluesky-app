using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Resources.Styles;

partial class ApplicationTheme
{
    public static partial class Selector
    {
        public static class Typo
        {
            public static string DisplayDisplay1 = nameof(DisplayDisplay1);
            public static string DisplayDisplay2 = nameof(DisplayDisplay2);
            public static string HeadingHeading1 = nameof(HeadingHeading1);
            public static string HeadingHeading2 = nameof(HeadingHeading2);
            public static string HeadingHeading3 = nameof(HeadingHeading3);
            public static string HeadingHeading4 = nameof(HeadingHeading4);
            public static string HeadingHeading5 = nameof(HeadingHeading5);
            public static string HeadingHeading6 = nameof(HeadingHeading6);
            public static string BodyLg = nameof(BodyLg);
            public static string BodyMd = nameof(BodyMd);
            public static string BodySm = nameof(BodySm);
            public static string BodyXs = nameof(BodyXs);
            public static string LabelLgBold = nameof(LabelLgBold);
            public static string LabelLgRegular = nameof(LabelLgRegular);
            public static string LabelMdBold = nameof(LabelMdBold);
            public static string LabelMdRegular = nameof(LabelMdRegular);
            public static string LabelSmBold = nameof(LabelSmBold);
            public static string LabelSmRegular = nameof(LabelSmRegular);
            public static string LabelXsBold = nameof(LabelXsBold);
            public static string LabelXsRegular = nameof(LabelXsRegular);
            public static string Default140 = nameof(Default140);
            public static string Default64 = nameof(Default64);
            public static string Default40Bold = nameof(Default40Bold);
            public static string Default40Medium = nameof(Default40Medium);
            public static string Default32 = nameof(Default32);
            public static string Default24Bold = nameof(Default24Bold);
            public static string Default24Medium = nameof(Default24Medium);
            public static string Default24Regular = nameof(Default24Regular);
            public static string Default18Bold = nameof(Default18Bold);
            public static string Default18Regular = nameof(Default18Regular);
            public static string Default16Bold = nameof(Default16Bold);
            public static string Default16Regular = nameof(Default16Regular);
            public static string Default14Medium = nameof(Default14Medium);
        }
    }

    static void ApplyTypographyStyles()
    {
        LabelStyles.Themes[Selector.Typo.DisplayDisplay1] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(64)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.DisplayDisplay2] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(40)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.HeadingHeading1] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(32)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.HeadingHeading2] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(24)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.HeadingHeading3] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(18)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.HeadingHeading4] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(16)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.HeadingHeading5] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(14)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.HeadingHeading6] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(12)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.BodyLg] = _ => _
            .FontFamily("PlusJakartaSansRegular")
            .FontSize(18)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.BodyMd] = _ => _
            .FontFamily("PlusJakartaSansRegular")
            .FontSize(16)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.BodySm] = _ => _
            .FontFamily("PlusJakartaSansRegular")
            .FontSize(14)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.BodyXs] = _ => _
            .FontFamily("PlusJakartaSansRegular")
            .FontSize(12)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.LabelLgBold] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(18)
            .FontAttributes(MauiControls.FontAttributes.Bold)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.LabelLgRegular] = _ => _
            .FontFamily("PlusJakartaSansRegular")
            .FontSize(18)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.LabelMdBold] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontAttributes(MauiControls.FontAttributes.Bold)
            .FontSize(16)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.LabelMdRegular] = _ => _
            .FontFamily("PlusJakartaSansRegular")
            .FontSize(16)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.LabelSmBold] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(14)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.LabelSmRegular] = _ => _
            .FontFamily("PlusJakartaSansRegular")
            .FontSize(14)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.LabelXsBold] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(12)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.LabelXsRegular] = _ => _
            .FontFamily("PlusJakartaSansRegular")
            .FontSize(12)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default140] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(140)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default64] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(64)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default40Bold] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(40)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default40Medium] = _ => _
            .FontFamily("PlusJakartaSansMedium")
            .FontSize(40)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default32] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(32)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default24Bold] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(24)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default24Medium] = _ => _
            .FontFamily("PlusJakartaSansMedium")
            .FontSize(24)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default24Regular] = _ => _
            .FontFamily("PlusJakartaSansRegular")
            .FontSize(24)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default18Bold] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(18)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default18Regular] = _ => _
            .FontFamily("PlusJakartaSansRegular")
            .FontSize(18)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default16Bold] = _ => _
            .FontFamily("PlusJakartaSansBold")
            .FontSize(16)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default16Regular] = _ => _
            .FontFamily("PlusJakartaSansRegular")
            .FontSize(16)
            .TextColor(Colors.Global.Grey100);

        LabelStyles.Themes[Selector.Typo.Default14Medium] = _ => _
            .FontFamily("PlusJakartaSansMedium")
            .FontSize(14)
            .TextColor(Colors.Global.Grey100);
    }
}
