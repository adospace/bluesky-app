﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Components.Home;

partial class SearchPage : Component
{
    public override VisualNode Render()
    {
        return ContentPage(
            Label("Search").Center()
            );
    }
}