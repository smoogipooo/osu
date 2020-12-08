// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Online.Multiplayer;
using osu.Game.Screens.Multi.Lounge;
using osu.Game.Screens.Multi.Lounge.Components;

namespace osu.Game.Screens.Multi.Realtime
{
    public class RealtimeLoungeSubScreen : LoungeSubScreen
    {
        protected override FilterControl CreateFilterControl() => new RealtimeFilterControl();

        protected override MultiplayerSubScreen CreateRoomSubScreen(Room room) => new RealtimeMatchSubScreen(room);
    }
}
