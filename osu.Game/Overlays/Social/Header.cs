// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Overlays.Browse;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics;
using osu.Framework.Allocation;

namespace osu.Game.Overlays.Social
{
    public class Header : BrowseHeader<SocialTab>
    {
        private OsuSpriteText browser;

        protected override Color4 BackgroundColour => OsuColour.FromHex(@"672b51");
        protected override float TabStripWidth => 438;
        protected override SocialTab DefaultTab => SocialTab.OnlinePlayers;

        protected override Drawable CreateHeaderText()
        {
            return new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                Children = new[]
                {
                    new OsuSpriteText
                    {
                        Text = "social ",
                        TextSize = 25,
                    },
                    browser = new OsuSpriteText
                    {
                        Text = "browser",
                        TextSize = 25,
                        Font = @"Exo2.0-Light",
                    },
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            browser.Colour = colours.Pink;
        }
    }
}
