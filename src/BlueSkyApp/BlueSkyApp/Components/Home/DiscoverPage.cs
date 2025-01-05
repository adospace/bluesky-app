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

record FeedViewPostModel(FeedViewPost? Post);

class DiscoverPageState
{
    public bool IsInitializing { get; set; } = true;

    public bool IsRefreshing { get; set; }

    public bool IsRefreshingListFromBottom { get; set; }

    public string? Cursor { get; set; }

    public ObservableCollection<FeedViewPostModel> Items { get; set; } = [];
}

partial class DiscoverPage : Component<DiscoverPageState>
{
    [Inject]
    IBlueSkyService _blueSkyService;

    public override VisualNode Render()
    {
        return ContentPage(
            Grid(                
                new SfPullToRefresh(                
                    CollectionView()
                        .ItemsSource(State.Items, RenderItem)
                )
                .IsRefreshing(() => State.IsRefreshing)
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

    Grid RenderItem(FeedViewPostModel item)
    {
        if (item.Post == null)
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

        var post = item.Post;

        return Grid("Auto,*,Auto", "52,*",

            post.Post?.Author?.Avatar == null ? null :
            Border(
                Image(post.Post.Author.Avatar)
                    .Aspect(Aspect.AspectFill)
            )
            .VStart()
            .GridRowSpan(3)
            .Width(44)
            .Height(44)
            .StrokeCornerRadius(9999)
            .StrokeThickness(0)
            .Margin(4),

            Grid("*", "Auto,*,Auto",
                Label(post.Post?.Author?.DisplayName)
                    .ThemeKey(ApplicationTheme.Selector.Typo.LabelXsBold)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgBase),

                Label(post.Post?.Author?.Handle)
                    .LineBreakMode(LineBreakMode.CharacterWrap)
                    .Margin(3,0)
                    .MaxLines(1)
                    .ThemeKey(ApplicationTheme.Selector.Typo.LabelXsRegular)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgMuted)
                    .GridColumn(1),

                Label(post.Post?.IndexedAt)
                    .ThemeKey(ApplicationTheme.Selector.Typo.LabelXsRegular)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgMuted)
                    .GridColumn(2)
            )
            .GridColumn(1),

            Label((post.Post?.Record as FishyFlip.Lexicon.App.Bsky.Feed.Post)?.Text ?? "")
                .ThemeKey(ApplicationTheme.Selector.Typo.LabelXsRegular)
                .TextColor(ApplicationTheme.Colors.Semantic.FgBase)
                .GridColumn(1)
                .GridRow(1)
        );
    }


    async void RefreshListBottom()
        => await RefreshList(false);

    async Task RefreshList()
        => await RefreshList(true);

    async Task RefreshList(bool fullRefresh)
    {
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
                s.Cursor = result.AsT0!.Cursor;
                if (fullRefresh)
                {
                    s.Items = result.AsT0!.Feed!.Select(post => new FeedViewPostModel(post)).Concat([new FeedViewPostModel(null)]).ToObservableCollection();
                }
                else
                {
                    if (s.Items.Count > 0 && s.Items[^1].Post == null)
                    {
                        s.Items.RemoveAt(s.Items.Count - 1);
                    }

                    foreach (var post in result.AsT0!.Feed!)
                    {
                        s.Items.Add(new FeedViewPostModel(post));
                    }

                    s.Items.Add(new FeedViewPostModel(null));
                }                    
            });
        }
        else
        {
            SetState(s =>
            {
                s.IsInitializing = false;
                s.IsRefreshing = false;
            });

            var error = result.AsT1!;

            await AppUtils.DisplaySnackbarAsync($"Unable to fetch the feed ({error.StatusCode}, {error.Detail?.Error} - {error.Detail?.Message})");
        }
    }
}
