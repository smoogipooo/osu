// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Online.RealtimeMultiplayer;
using osu.Game.Users;
using osu.Game.Users.Drawables;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Multi.Realtime.Participants
{
    public class ParticipantPanel : MatchComposite
    {
        public readonly MultiplayerRoomUser User;

        private ParticipantReadyMark readyMark;

        public ParticipantPanel(MultiplayerRoomUser user)
        {
            User = user;

            RelativeSizeAxes = Axes.X;
            Height = 40;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Debug.Assert(User.User != null);

            var backgroundColour = Color4Extensions.FromHex("#33413C");

            InternalChild = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                CornerRadius = 5,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = backgroundColour
                    },
                    new UserCoverBackground
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        RelativeSizeAxes = Axes.Both,
                        Width = 0.75f,
                        User = User.User,
                        Colour = ColourInfo.GradientHorizontal(Color4.White.Opacity(0), Color4.White.Opacity(0.25f))
                    },
                    new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Spacing = new Vector2(10),
                        Direction = FillDirection.Horizontal,
                        Children = new Drawable[]
                        {
                            new UpdateableAvatar
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                RelativeSizeAxes = Axes.Both,
                                FillMode = FillMode.Fit,
                                User = User.User
                            },
                            new UpdateableFlag
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Size = new Vector2(30, 20),
                                Country = User.User.Country
                            },
                            new OsuSpriteText
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Font = OsuFont.GetFont(weight: FontWeight.Bold, size: 18),
                                Text = User.User.Username
                            },
                            new OsuSpriteText
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Font = OsuFont.GetFont(size: 14),
                                Text = User.User.CurrentModeRank != null ? $"#{User.User.CurrentModeRank}" : string.Empty
                            }
                        }
                    },
                    readyMark = new ParticipantReadyMark
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        Margin = new MarginPadding { Right = 10 },
                        Alpha = 0
                    }
                }
            };
        }

        protected override void OnRoomChanged()
        {
            base.OnRoomChanged();

            if (User.State == MultiplayerUserState.Ready)
                readyMark.FadeIn(50);
            else
                readyMark.FadeOut(50);
        }
    }
}