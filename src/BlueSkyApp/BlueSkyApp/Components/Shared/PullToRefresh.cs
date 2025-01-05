using MauiReactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Components.Shared;

class PullToRefreshState
{
    public double? PanTotalY { get; set; }
}


partial class PullToRefresh : Component<PullToRefreshState>
{
    const double _refreshThreshold = 100;

    [Prop]
    Action? _onRefresh;

    [Prop]
    bool _isRefreshing;

    protected override void OnMountedOrPropsChanged()
    {
        State.PanTotalY = _isRefreshing ? null : State.PanTotalY;
        base.OnMountedOrPropsChanged();
    }

    public override VisualNode Render()
    {
        return Grid(
            Children()
                .Concat([

                    Border()
                        .Shadow(new Shadow())
                        .StrokeThickness(0)
                        .StrokeCornerRadius(9999)
                        .Width(44)
                        .Height(44)
                        .TranslationY(Math.Max(State.PanTotalY.GetValueOrDefault(),  0))
                        .IsVisible(State.PanTotalY != null)
                        .VStart()
                        .HCenter(),

                    ActivityIndicator()
                        .Height(44)
                        .Width(44)
                        .IsVisible(_isRefreshing)
                        .IsRunning(true)
                        .Margin(0,_refreshThreshold,0,0)
                        .VStart(),

                    Grid()
                        .OnPanUpdated(OnPanUpdated)
                ])
        );
    }

    void OnPanUpdated(MauiControls.PanUpdatedEventArgs args)
    {
        System.Diagnostics.Debug.WriteLine(System.Text.Json.JsonSerializer.Serialize(args));

        SetState(s => 
        {
            if (args.StatusType == GestureStatus.Started)
            {
                s.PanTotalY = 0;
            }
            else if (args.StatusType == GestureStatus.Running || args.StatusType == GestureStatus.Completed)
            {
                s.PanTotalY = args.TotalY;
            }
            else if (args.StatusType == GestureStatus.Canceled)
            {
                s.PanTotalY = null;
            }
        }, false);

        if (State.PanTotalY > _refreshThreshold)
        {
            _onRefresh?.Invoke();
        }
    }
}
