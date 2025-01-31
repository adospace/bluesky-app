﻿using BlueSkyApp.Framework;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Resources.Styles;

partial class ApplicationTheme : Theme
{
    public static partial class Selector
    {
    }

    protected override void OnApply()
    {
        ApplyTypographyStyles();
        ApplyTextFieldsStyles();
        ApplyButtonsStyles();

        ContentPageStyles.Default = _ => _
            .BackgroundColor(Colors.Semantic.BgCanvas)
            .AddChildren(
                new StatusBarBehavior()
                    .StatusBarColor(Colors.Semantic.BgCanvas)
                    .StatusBarStyle(IsLightTheme ?
                        StatusBarStyle.DarkContent :
                        StatusBarStyle.LightContent)
            );

        ActivityIndicatorStyles.Default = _ => _
            .Color(Colors.Semantic.AccentModerate);


        ShellStyles.Default = _ => _
            //.FlyoutIcon("menu_black.png")
            .Set(MauiControls.Shell.ForegroundColorProperty, Colors.Semantic.FgBase)
            .Set(MauiControls.Shell.TitleColorProperty, Colors.Semantic.FgBase)
            .Set(MauiControls.Shell.UnselectedColorProperty, Colors.Semantic.FgSubtle)
            .Set(MauiControls.Shell.BackgroundColorProperty, Colors.Semantic.BgCanvas)
            ;
    }
}