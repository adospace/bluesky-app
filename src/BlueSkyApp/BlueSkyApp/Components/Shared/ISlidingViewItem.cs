using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Components.Shared;

interface ISlidingViewItem
{
    string Title { get; }

    void OnScrolled(Action<MauiControls.ItemsViewScrolledEventArgs>? action);
}
