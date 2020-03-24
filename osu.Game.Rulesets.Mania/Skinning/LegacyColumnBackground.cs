// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.Mania.UI;
using osu.Game.Rulesets.UI.Scrolling;
using osu.Game.Skinning;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Mania.Skinning
{
    public class LegacyColumnBackground : CompositeDrawable, IKeyBindingHandler<ManiaAction>
    {
        private readonly IBindable<ScrollingDirection> direction = new Bindable<ScrollingDirection>();

        private Sprite light;

        [Resolved]
        private Column column { get; set; }

        [Resolved(CanBeNull = true)]
        private ManiaStage stage { get; set; }

        public LegacyColumnBackground()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(ISkinSource skin, IScrollingInfo scrollingInfo)
        {
            string lightImage = skin.GetConfig<LegacyColumnSkinConfiguration, string>(new LegacyColumnSkinConfiguration(0, LegacyColumnSkinConfigurations.LightImage))?.Value ?? "mania-stage-light";

            float leftLineWidth = skin.GetConfig<LegacyColumnSkinConfiguration, float>(new LegacyColumnSkinConfiguration(column.Index, LegacyColumnSkinConfigurations.LeftLineWidth))?.Value ?? 1;
            float rightLineWidth = skin.GetConfig<LegacyColumnSkinConfiguration, float>(new LegacyColumnSkinConfiguration(column.Index, LegacyColumnSkinConfigurations.RightLineWidth))?.Value ?? 1;

            bool hasLeftLine = leftLineWidth > 0;
            bool hasRightLine = rightLineWidth > 0 && skin.GetConfig<LegacySkinConfiguration.LegacySetting, decimal>(LegacySkinConfiguration.LegacySetting.Version)?.Value >= 2.4m
                                || stage == null || column.Index == stage.Columns.Count - 1;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black
                },
                new Box
                {
                    RelativeSizeAxes = Axes.Y,
                    Width = leftLineWidth,
                    Alpha = hasLeftLine ? 1 : 0
                },
                new Box
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    RelativeSizeAxes = Axes.Y,
                    Width = rightLineWidth,
                    Alpha = hasRightLine ? 1 : 0
                },
                light = new Sprite
                {
                    Origin = Anchor.BottomCentre,
                    Texture = skin.GetTexture(lightImage),
                    RelativeSizeAxes = Axes.X,
                    Width = 1,
                    Alpha = 0
                }
            };

            direction.BindTo(scrollingInfo.Direction);
            direction.BindValueChanged(onDirectionChanged, true);
        }

        private void onDirectionChanged(ValueChangedEvent<ScrollingDirection> direction)
        {
            if (direction.NewValue == ScrollingDirection.Up)
            {
                light.Anchor = Anchor.TopCentre;
                light.Rotation = 180;
            }
            else
            {
                light.Anchor = Anchor.BottomCentre;
                light.Rotation = 0;
            }
        }

        public bool OnPressed(ManiaAction action)
        {
            if (action == column.Action.Value)
            {
                light.FadeIn();
                light.ScaleTo(Vector2.One);
            }

            return false;
        }

        public void OnReleased(ManiaAction action)
        {
            // Todo: Should be 400 * 100 / CurrentBPM
            const double animation_length = 250;

            if (action == column.Action.Value)
            {
                light.FadeTo(0, animation_length);
                light.ScaleTo(new Vector2(1, 0), animation_length);
            }
        }
    }
}
