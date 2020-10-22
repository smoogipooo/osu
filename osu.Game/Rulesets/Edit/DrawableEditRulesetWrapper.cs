// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.UI;
using osu.Game.Screens.Edit;

namespace osu.Game.Rulesets.Edit
{
    /// <summary>
    /// A wrapper for a <see cref="DrawableRuleset{TObject}"/>. Handles adding visual representations of <see cref="HitObject"/>s to the underlying <see cref="DrawableRuleset{TObject}"/>.
    /// </summary>
    internal class DrawableEditRulesetWrapper<TObject> : CompositeDrawable
        where TObject : HitObject
    {
        public Playfield Playfield => drawableRuleset.Playfield;

        private readonly DrawableRuleset<TObject> drawableRuleset;

        [Resolved]
        private EditorBeatmap beatmap { get; set; }

        public DrawableEditRulesetWrapper(DrawableRuleset<TObject> drawableRuleset)
        {
            this.drawableRuleset = drawableRuleset;

            RelativeSizeAxes = Axes.Both;

            InternalChild = drawableRuleset;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            drawableRuleset.FrameStablePlayback = false;
            Playfield.DisplayJudgements.Value = false;
        }

        [Resolved(canBeNull: true)]
        private IEditorChangeHandler changeHandler { get; set; }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            beatmap.HitObjectAdded += addHitObject;
            beatmap.HitObjectRemoved += removeHitObject;

            if (changeHandler != null)
            {
                // for now only regenerate replay on a finalised state change, not HitObjectUpdated.
                changeHandler.OnStateChange += updateReplay;
            }
            else
            {
                beatmap.HitObjectUpdated += _ => updateReplay();
            }
        }

        private void updateReplay() => drawableRuleset.RegenerateAutoplay();

        private void addHitObject(HitObject hitObject)
        {
            drawableRuleset.AddHitObject((TObject)hitObject);

#pragma warning disable 618
            drawableRuleset.Playfield.PostProcess();
#pragma warning restore 618
        }

        private void removeHitObject(HitObject hitObject)
        {
            drawableRuleset.RemoveHitObject((TObject)hitObject);

#pragma warning disable 618
            drawableRuleset.Playfield.PostProcess();
#pragma warning restore 618
        }

        public override bool PropagatePositionalInputSubTree => false;

        public override bool PropagateNonPositionalInputSubTree => false;

        public PlayfieldAdjustmentContainer CreatePlayfieldAdjustmentContainer() => drawableRuleset.CreatePlayfieldAdjustmentContainer();

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (beatmap != null)
            {
                beatmap.HitObjectAdded -= addHitObject;
                beatmap.HitObjectRemoved -= removeHitObject;
            }
        }
    }
}
