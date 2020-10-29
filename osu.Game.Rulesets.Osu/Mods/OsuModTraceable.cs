﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects.Drawables.Pieces;

namespace osu.Game.Rulesets.Osu.Mods
{
    internal class OsuModTraceable : ModWithFirstObjectVisibilityIncrease
    {
        public override string Name => "Traceable";
        public override string Acronym => "TC";
        public override ModType Type => ModType.Fun;
        public override string Description => "Put your faith in the approach circles...";
        public override double ScoreMultiplier => 1;

        public override Type[] IncompatibleMods => new[] { typeof(OsuModHidden), typeof(OsuModSpinIn), typeof(OsuModObjectScaleTween) };

        protected override void ApplyVisibilityState(DrawableHitObject hitObject, ArmedState state)
        {
            base.ApplyVisibilityState(hitObject, state);

            if (!(hitObject is DrawableOsuHitObject))
                return;

            //todo: expose and hide spinner background somehow

            switch (hitObject)
            {
                case DrawableHitCircle circle:
                    // we only want to see the approach circle
                    applyCirclePieceState(circle, circle.CirclePiece);
                    break;

                case DrawableSliderTail sliderTail:
                    applyCirclePieceState(sliderTail);
                    break;

                case DrawableSliderRepeat sliderRepeat:
                    // show only the repeat arrow
                    applyCirclePieceState(sliderRepeat, sliderRepeat.CirclePiece);
                    break;

                case DrawableSlider slider:
                    slider.AccentChanged -= onAccentChanged;
                    slider.AccentChanged += onAccentChanged;
                    onAccentChanged(slider);
                    break;
            }
        }

        private void applyCirclePieceState(DrawableOsuHitObject hitObject, IDrawable hitCircle = null)
        {
            var h = hitObject.HitObject;
            using (hitObject.BeginAbsoluteSequence(h.StartTime - h.TimePreempt, true))
                (hitCircle ?? hitObject).Hide();
        }

        private void onAccentChanged(DrawableSlider slider)
        {
            ((PlaySliderBody)slider.Body.Drawable).AccentColour = slider.AccentColour.Value.Opacity(0);
            ((PlaySliderBody)slider.Body.Drawable).BorderColour = slider.AccentColour.Value;
        }
    }
}
