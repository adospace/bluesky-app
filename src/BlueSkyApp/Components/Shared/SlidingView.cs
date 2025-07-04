using static BlueSkyApp.Resources.Styles.ApplicationTheme;
using Colors = BlueSkyApp.Resources.Styles.ApplicationTheme.Colors;

namespace BlueSkyApp.Components.Shared;

class SlidingViewState
{
    public int SelectedIndex { get; set; } = 0;

    public double ScrollX { get; set; }

    public double HeaderTranslateY { get; set; } = 48.0;

    public bool Translating { get; set; }
}

partial class SlidingView : Component<SlidingViewState>
{
    [Prop]
    Action<int>? _onSelectedViewIndex;

    [Prop]
    int _selectedIndex;

    protected override void OnMountedOrPropsChanged()
    {
        State.SelectedIndex = _selectedIndex;
        base.OnMountedOrPropsChanged();
    }


    public override VisualNode Render()
    {
        var children = Children().ToList();

        return Grid(
            CarouselView()
                .ItemsSource(Children(), item => item)
                .CurrentItem(()=> children[State.SelectedIndex])
                .OnCurrentItemChanged(args => SetState(s => s.SelectedIndex = children.IndexOf((VisualNode)args.CurrentItem)))
                .OnScrolled(OnScrolled)
                .Loop(false)
                .Margin(() => new Thickness(0, State.HeaderTranslateY, 0,0))
                
                ,

            Grid(
                [.. 
                Children()
                    .Cast<ISlidingViewItem>()
                    .Select((item, index) => RenderItem(index, item)),

                Border()
                    .VEnd()
                    .Height(1)
                    .BackgroundColor(Colors.Semantic.BorderSubtle)
                    .GridColumnSpan(children.Count)
                    ,

                Border()
                    .VEnd()
                    .Height(2)
                    .BackgroundColor(Colors.Semantic.AccentModerate)
                    .TranslationX(()=>State.ScrollX)
               ]
            )
            .VStart()
            .Height(48.0)
            .Columns(Children().Select(_=> new MauiControls.ColumnDefinition(GridLength.Star)))
            .TranslationY(() => State.HeaderTranslateY - 48.0)
            .Opacity(()=>State.HeaderTranslateY / 48.0)
        );
    }

    Label RenderItem(int index, ISlidingViewItem item)
    {
        item.OnScrolled(args => OnViewVerticallyScrolled(item, index, args));
        return Label(item.Title)
            .ThemeKey(Selector.Typo.LabelLgBold)
            .TextColor(index == State.SelectedIndex ? Colors.Semantic.AccentModerate : Colors.Semantic.FgSubtle)
            .HorizontalTextAlignment(TextAlignment.Center)
            .VerticalTextAlignment(TextAlignment.Center)
            .GridColumn(index)
            .OnTapped(() => SetState(s => s.SelectedIndex = index, false));
    }

    private void OnViewVerticallyScrolled(ISlidingViewItem item, int index, MauiControls.ItemsViewScrolledEventArgs args)
    {
        if (State.Translating)
        {
            State.Translating = false;
            return;
        }

        var headerTranslateY = Math.Max(0.0, Math.Min(State.HeaderTranslateY - args.VerticalDelta, 48.0));
        if (headerTranslateY != State.HeaderTranslateY)
        {
            System.Diagnostics.Debug.WriteLine($"args.VerticalDelta:{args.VerticalDelta} State.HeaderTranslateY:{State.HeaderTranslateY}");
            SetState(s =>
            {
                s.HeaderTranslateY = headerTranslateY;
                s.Translating = true;
            }, false);
        }
    }

    void OnScrolled(MauiControls.ItemsViewScrolledEventArgs args)
    {
        SetState(s => s.ScrollX = args.HorizontalOffset / Children().Count, invalidateComponent: false);
    }
}
