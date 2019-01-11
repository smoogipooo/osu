﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Online.Multiplayer;
using osu.Game.Screens.Multi.Match.Components;
using osu.Game.Users;

namespace osu.Game.Tests.Visual
{
    [TestFixture]
    public class TestCaseMatchParticipants : OsuTestCase
    {
        public TestCaseMatchParticipants()
        {
            var room = new Room();

            Child = new CachedModelContainer<Room>
            {
                RelativeSizeAxes = Axes.Both,
                Model = room,
                Child = new Participants { RelativeSizeAxes = Axes.Both }
            };

            AddStep(@"set max to null", () => room.MaxParticipants.Value = null);
            AddStep(@"set users", () => room.Participants.Value = new[]
            {
                new User
                {
                    Username = @"Feppla",
                    Id = 4271601,
                    Country = new Country { FlagName = @"SE" },
                    CoverUrl = @"https://osu.ppy.sh/images/headers/profile-covers/c2.jpg",
                    IsSupporter = true,
                },
                new User
                {
                    Username = @"Xilver",
                    Id = 3099689,
                    Country = new Country { FlagName = @"IL" },
                    CoverUrl = @"https://osu.ppy.sh/images/headers/profile-covers/c2.jpg",
                    IsSupporter = true,
                },
                new User
                {
                    Username = @"Wucki",
                    Id = 5287410,
                    Country = new Country { FlagName = @"FI" },
                    CoverUrl = @"https://assets.ppy.sh/user-profile-covers/5287410/5cfeaa9dd41cbce038ecdc9d781396ed4b0108089170bf7f50492ef8eadeb368.jpeg",
                    IsSupporter = true,
                },
            });

            AddStep(@"set max", () => room.MaxParticipants.Value = 10);
            AddStep(@"clear users", () => room.Participants.Value = new User[] { });
            AddStep(@"set max to null", () => room.MaxParticipants.Value = null);
        }
    }
}
