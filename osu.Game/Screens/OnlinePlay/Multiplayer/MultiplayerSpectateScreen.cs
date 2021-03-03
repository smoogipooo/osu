// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;
using osu.Game.Database;
using osu.Game.Online.Rooms;
using osu.Game.Rulesets;
using osu.Game.Rulesets.Mods;
using osu.Game.Screens.Play;
using osuTK;

namespace osu.Game.Screens.OnlinePlay.Multiplayer
{
    public class MultiplayerSpectateScreen : OsuScreen
    {
        // Isolates beatmap/ruleset to this screen.
        public override bool DisallowExternalBeatmapRulesetChanges => true;
        private readonly PlaylistItem playlistItem;
        private readonly int[] userIds;

        [Resolved]
        private UserLookupCache userLookupCache { get; set; }

        public MultiplayerSpectateScreen(PlaylistItem playlistItem, int[] userIds)
        {
            this.playlistItem = playlistItem;
            this.userIds = userIds;
        }

        [BackgroundDependencyLoader]
        private void load(UserLookupCache userLookupCache)
        {
            Drawable[,] instances = new Drawable[4, 4];

            for (int x = 0; x < instances.GetLength(0); x++)
            {
                for (int y = 0; y < instances.GetLength(1); y++)
                    instances[x, y] = createInstance(userIds[0]);
            }

            InternalChild = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                Content = instances.ToJagged()
            };
        }

        private Drawable createInstance(int userId)
        {
            var stack = new ScalingScreenStack();
            var user = userLookupCache.GetUserAsync(userId).Result;
            stack.Push(new Spectator(user));

            return stack;
        }

        private class ScalingScreenStack : OsuScreenStack
        {
            public ScalingScreenStack()
            {
                RelativeSizeAxes = Axes.None;
            }

            protected override void Update()
            {
                base.Update();

                Size = Parent.Parent.DrawSize;
                Scale = Vector2.Divide(Parent.DrawSize, Size);
            }
        }
    }
}
