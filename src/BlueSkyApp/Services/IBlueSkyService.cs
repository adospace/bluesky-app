using FishyFlip.Lexicon.App.Bsky.Feed;
using FishyFlip.Models;
using FishyFlip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishyFlip.Lexicon.Com.Atproto.Server;
using System.Reflection.Metadata;

namespace BlueSkyApp.Services;

interface IBlueSkyService
{
    Task<bool> InitializeAsync();

    Task<bool> LoginAsync(string handle, string appPassword, bool remember);

    bool IsLoggedIn { get; }

    ATProtocol Client { get; }
}
