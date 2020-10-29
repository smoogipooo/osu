// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using osuTK;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModSpinIn : ModWithFirstObjectVisibilityIncrease
    {
        public override string Name => "Spin In";
        public override string Acronym => "SI";
        public override IconUsage? Icon => FontAwesome.Solid.Undo;
        public override ModType Type => ModType.Fun;
        public override string Description => "Circles spin in. No approach circles.";
        public override double ScoreMultiplier => 1;

        // todo: this mod should be able to be compatible with hidden with a bit of further implementation.
        public override Type[] IncompatibleMods => new[] { typeof(OsuModObjectScaleTween), typeof(OsuModHidden), typeof(OsuModTraceable) };

        private const int rotate_offset = 360;
        private const float rotate_starting_width = 2;

        protected override void ApplyVisibilityState(DrawableHitObject hitObject, ArmedState state)
        {
            base.ApplyVisibilityState(hitObject, state);

            if (hitObject is DrawableSpinner)
                return;

            var h = (OsuHitObject)hitObject.HitObject;

            switch (hitObject)
            {
                case DrawableHitCircle circle:
                    using (circle.BeginAbsoluteSequence(h.StartTime - h.TimePreempt, true))
                    {
                        circle.ApproachCircle.Hide();

                        circle.RotateTo(rotate_offset).Then().RotateTo(0, h.TimePreempt, Easing.InOutSine);
                        circle.ScaleTo(new Vector2(rotate_starting_width, 0)).Then().ScaleTo(1, h.TimePreempt, Easing.InOutSine);

                        // bypass fade in.
                        if (state == ArmedState.Idle)
                            circle.FadeIn();
                    }

                    break;

                case DrawableSlider slider:
                    using (slider.BeginAbsoluteSequence(h.StartTime - h.TimePreempt))
                    {
                        slider.ScaleTo(0).Then().ScaleTo(1, h.TimePreempt, Easing.InOutSine);

                        // bypass fade in.
                        if (state == ArmedState.Idle)
                            slider.FadeIn();
                    }

                    break;
            }
        }
    }
}
