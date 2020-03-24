// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Skinning;

namespace osu.Game.Rulesets.Mania
{
    public abstract class ManiaSkinComponent<T> : GameplaySkinComponent<T>
    {
        protected ManiaSkinComponent(T component)
            : base(component)
        {
        }

        protected override string RulesetPrefix => ManiaRuleset.SHORT_NAME;

        protected override string ComponentName => Component.ToString().ToLower();
    }
}
