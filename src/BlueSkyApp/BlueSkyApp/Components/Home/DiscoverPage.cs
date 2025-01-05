using BlueSkyApp.Components.Shared;
using BlueSkyApp.Framework;
using BlueSkyApp.Resources.Styles;
using BlueSkyApp.Services;
using CommunityToolkit.Maui.Core.Extensions;
using FishyFlip.Lexicon.App.Bsky.Feed;
using FishyFlip.Lexicon.Fyi.Unravel.Frontpage;
using FishyFlip.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Components.Home;

class DiscoverPageState
{
    public bool IsInitializing { get; set; } = true;

    public bool IsRefreshing { get; set; }

    public bool IsRefreshingListFromBottom { get; set; }

    public string? Cursor { get; set; }

    public ObservableCollection<(bool LoadingItem, FeedViewPost? Post)> Items { get; set; } = [];
}

partial class DiscoverPage : Component<DiscoverPageState>
{
    [Inject]
    IBlueSkyService _blueSkyService;

    public override VisualNode Render()
    {
        return ContentPage(
            Grid(                
                new SfPullToRefresh()
                {
                    CollectionView()
                        .ItemsSource(State.Items, RenderItem)
                }
                .IsRefreshing(State.IsRefreshing)
                .OnRefreshing(RefreshList),

                ActivityIndicator()
                    .Height(60)
                    .Width(60)
                    .IsVisible(State.IsInitializing)
                    .IsRunning(true)
            )            
        )
        .OnAppearing(RefreshList);
    }

    Grid RenderItem((bool LoadingItem, FeedViewPost? PostView) item)
    {
        if (item.LoadingItem)
        {
            if (!State.IsRefreshingListFromBottom)
            {
                State.IsRefreshingListFromBottom = true;
                RefreshListBottom();
            }

            return Grid("Auto", "*",
                ActivityIndicator()
                    .Height(24)
                    .Width(24)
                    .IsVisible(true)
                    .IsRunning(true)
                    .HCenter()
                    .Margin(4)
            );
        }

        var post = item.PostView?.Post;

        return Grid("Auto,*,Auto", "52,*",

            post?.Author?.Avatar == null ? null :
            Border(
                Image(post.Author.Avatar)
                    .Aspect(Aspect.AspectFill)
            )
            .VStart()
            .GridRowSpan(3)
            .Width(44)
            .Height(44)
            .StrokeCornerRadius(9999)
            .StrokeThickness(0)
            .Margin(5,5,5,5),

            Grid("*", "Auto,*,Auto",
                Label(post?.Author?.DisplayName)
                    .ThemeKey(ApplicationTheme.Selector.Typo.LabelMdBold)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgBase),

                Label(post?.Author?.Handle)
                    .LineBreakMode(LineBreakMode.CharacterWrap)
                    .Margin(3,0)
                    .MaxLines(1)
                    .ThemeKey(ApplicationTheme.Selector.Typo.LabelMdRegular)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgMuted)
                    .GridColumn(1),

                Label(post?.IndexedAt.ToShortDateTimeString())
                    .ThemeKey(ApplicationTheme.Selector.Typo.LabelMdRegular)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgMuted)
                    .GridColumn(2)
            )
            .GridColumn(1),

            Label((post?.Record as FishyFlip.Lexicon.App.Bsky.Feed.Post)?.Text ?? "")
                .ThemeKey(ApplicationTheme.Selector.Typo.LabelMdRegular)
                .TextColor(ApplicationTheme.Colors.Semantic.FgBase)
                .GridColumn(1)
                .GridRow(1)
                .Margin(0,0,0,5)
        );
    }


    async void RefreshListBottom()
        => await RefreshList(false);

    async Task RefreshList()
        => await RefreshList(true);

    async Task RefreshList(bool fullRefresh)
    {
        if (fullRefresh && !State.IsInitializing)
        {
            SetState(s => s.IsRefreshing = true);
        }

        if (!await _blueSkyService.InitializeAsync())
        {
            SetState(s =>
            {
                s.IsInitializing = false;
                s.IsRefreshing = false;
                s.IsRefreshingListFromBottom = false;
            });

            return;
        }

        var result = fullRefresh ?
            await _blueSkyService.Client.GetTimelineAsync()
            :
            await _blueSkyService.Client.GetTimelineAsync(cursor: State.Cursor);

        if (result.IsT0)
        {
            SetState(s =>
            {
                s.IsInitializing = false;
                s.IsRefreshing = false;
                s.IsRefreshingListFromBottom = false;
                s.Cursor = result.AsT0!.Cursor;
                if (fullRefresh)
                {
                    s.Items = result.AsT0!.Feed!.Select(post => (false, (FeedViewPost?)post)).Concat([(true, null)]).ToObservableCollection();
                }
                else
                {
                    if (s.Items.Count > 0 && s.Items[^1].LoadingItem)
                    {
                        s.Items.RemoveAt(s.Items.Count - 1);
                    }

                    foreach (var post in result.AsT0!.Feed!)
                    {
                        s.Items.Add((false, (FeedViewPost?)post));
                    }

                    s.Items.Add((true, null));
                }
            });
        }
        else
        {
            SetState(s =>
            {
                s.IsInitializing = false;
                s.IsRefreshing = false;
                s.IsRefreshingListFromBottom = false;
            });

            var error = result.AsT1!;

            await AppUtils.DisplaySnackbarAsync($"Unable to fetch the feed ({error.StatusCode}, {error.Detail?.Error} - {error.Detail?.Message})");
        }
    }
}
