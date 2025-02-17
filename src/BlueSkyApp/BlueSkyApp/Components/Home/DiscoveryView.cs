using BlueSkyApp.Components.Shared;
using BlueSkyApp.Framework;
using BlueSkyApp.Resources.Styles;
using BlueSkyApp.Services;
using CommunityToolkit.Maui.Core.Extensions;
using FishyFlip.Lexicon.App.Bsky.Feed;
using FishyFlip.Lexicon.Fyi.Unravel.Frontpage;
using FishyFlip.Models;
using MauiReactor;
using MauiReactor.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Components.Home;

class DiscoverViewState
{
    public bool IsInitializing { get; set; } = true;

    public bool IsRefreshing { get; set; }

    public bool IsRefreshingListFromBottom { get; set; }

    public string? Cursor { get; set; }

    public ObservableCollection<(bool LoadingItem, FeedViewPost? Post)> Items { get; set; } = [];
}

partial class DiscoveryView : Component<DiscoverViewState>, ISlidingViewItem
{
    [Inject]
    IBlueSkyService _blueSkyService;

    private Action<MauiControls.ItemsViewScrolledEventArgs>? _scrolled;
    public void OnScrolled(Action<MauiControls.ItemsViewScrolledEventArgs>? action)
    {
        _scrolled = action;
    }

    public string Title => "Discovery";

    protected override void OnMounted()
    {
        Task.Run(RefreshList);

        base.OnMounted();
    }

    public override VisualNode Render()
    {
        return
            Grid(
                RefreshView(
                    CollectionView()
                        .ItemsSource(State.Items, RenderItem)
                        .OnScrolled(_scrolled)
                )
                .RefreshColor(ApplicationTheme.Colors.Semantic.AccentModerate)
                .IsRefreshing(State.IsRefreshing)
                .OnRefreshing(RefreshList),

                ActivityIndicator()
                    .Height(60)
                    .Width(60)
                    .IsVisible(State.IsInitializing)
                    .IsRunning(true)
            );        
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

        var postRecord = post?.Record as FishyFlip.Lexicon.App.Bsky.Feed.Post;


        return Grid("22,*,32,10,1,10", "56,*",

            Button()
                .GridRowSpan(4)
                .GridColumnSpan(2)
                .BackgroundColor(ApplicationTheme.Colors.Semantic.BgCanvas)
                //.OnClicked(async () => await ContainerPage.DisplayAlert("Hello!", "ok", "cancel"))
                ,

            post?.Author?.Avatar == null ? null :
            Border(
                Image(post.Author.Avatar)
                    .Aspect(Aspect.AspectFill)
            )
            .VStart()
            .GridRowSpan(3)
            .Width(40)
            .Height(40)
            .StrokeCornerRadius(9999)
            .StrokeThickness(0)
            .Margin(8),

            Grid("*", "Auto,*,Auto",
                Label(post?.Author?.DisplayName)
                    .LineBreakMode(LineBreakMode.TailTruncation)
                    .FontAttributes(Microsoft.Maui.Controls.FontAttributes.Bold)
                    .ThemeKey(ApplicationTheme.Selector.Typo.LabelMdRegular)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgBase),

                Label(post?.Author?.Handle)
                    .LineBreakMode(LineBreakMode.TailTruncation)
                    .Margin(3, 0)
                    .MaxLines(1)
                    .ThemeKey(ApplicationTheme.Selector.Typo.LabelMdRegular)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgMuted)
                    .GridColumn(1),

                Label(post?.IndexedAt.ToShortDateTimeString())
                    .ThemeKey(ApplicationTheme.Selector.Typo.LabelMdRegular)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgMuted)
                    .GridColumn(2)
            )
            .Margin(0, 0, 10, 0)
            .GridColumn(1),

            Label((post?.Record as FishyFlip.Lexicon.App.Bsky.Feed.Post)?.Text ?? "")
                .ThemeKey(ApplicationTheme.Selector.Typo.LabelMdRegular)
                .TextColor(ApplicationTheme.Colors.Semantic.FgBase)
                .GridColumn(1)
                .GridRow(1)
                .Margin(0, 0, 10, 0),


            Grid("*", "22,*,22,*,22,*,*,22",

                Image("reply.png")
                    .Aspect(Aspect.AspectFit)
                    .Height(16)
                    .Margin(0, 6, 0, 2),

                Label(post?.ReplyCount)
                    .VerticalTextAlignment(TextAlignment.Center)
                    .ThemeKey(ApplicationTheme.Selector.Typo.LabelSmRegular)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgMuted)
                    .GridColumn(1),

                Image("retweet.png")
                    .Aspect(Aspect.AspectFit)
                    .Height(16)
                    .Margin(0, 6, 0, 2)
                    .GridColumn(2),

                Label(post?.RepostCount)
                    .VerticalTextAlignment(TextAlignment.Center)
                    .ThemeKey(ApplicationTheme.Selector.Typo.LabelSmRegular)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgMuted)
                    .GridColumn(3),

                Image("likes.png")
                    .Aspect(Aspect.AspectFit)
                    .Height(16)
                    .GridColumn(4),

                Label(post?.LikeCount)
                    .VerticalTextAlignment(TextAlignment.Center)
                    .ThemeKey(ApplicationTheme.Selector.Typo.LabelSmRegular)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgMuted)
                    .GridColumn(5),

                Image("dots.png")
                    .Margin(0, 6, 0, 2)
                    .Height(16)
                    .GridColumn(7)
            )
            .Margin(0, 0, 10, 0)
            .GridRow(2)
            .GridColumn(1)
            .GridColumnSpan(2),

            Rectangle()
                .StrokeThickness(0)
                .VCenter()
                .Fill(ApplicationTheme.Colors.Semantic.BorderSubtle)
                .GridRow(4)
                .GridColumnSpan(2)
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

                //System.Diagnostics.Debug.WriteLine(System.Text.Json.JsonSerializer.Serialize(s.Items.Select(_=>_.Post)));
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

