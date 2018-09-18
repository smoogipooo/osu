﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Mania.Edit.Layers.Selection.Overlays;
using osu.Game.Rulesets.Mania.Objects.Drawables;
using osu.Game.Rulesets.Objects.Drawables;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Game.Rulesets.Edit.Tools;
using osu.Game.Rulesets.Mania.Configuration;
using osu.Game.Rulesets.Mania.UI;

namespace osu.Game.Rulesets.Mania.Edit
{
    public class ManiaHitObjectComposer : HitObjectComposer
    {
        protected new ManiaConfigManager Config => (ManiaConfigManager)base.Config;

        public ManiaHitObjectComposer(Ruleset ruleset)
            : base(ruleset)
        {
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            var dependencies = new DependencyContainer(base.CreateChildDependencies(parent));
            dependencies.CacheAs<IScrollingInfo>(new ManiaScrollingInfo(Config));
            return dependencies;
        }

        protected override EditRulesetContainer CreateRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap)
        {
            return null;
        }

        protected override IReadOnlyList<HitObjectCompositionTool> CompositionTools { get; }

        public override HitObjectMask CreateMaskFor(DrawableHitObject hitObject)
        {
            switch (hitObject)
            {
                case DrawableNote note:
                    return new NoteMask(note);
                case DrawableHoldNote holdNote:
                    return new HoldNoteMask(holdNote);
            }

            return base.CreateMaskFor(hitObject);
        }
    }
}
