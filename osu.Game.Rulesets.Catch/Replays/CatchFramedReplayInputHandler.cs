﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Diagnostics;
using osu.Framework.Input.StateChanges;
using osu.Framework.MathUtils;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Catch.Replays
{
    public class CatchFramedReplayInputHandler : FramedReplayInputHandler<CatchReplayFrame>
    {
        public CatchFramedReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        protected override bool IsImportant(CatchReplayFrame frame) => frame.Position > 0;

        protected float? Position
        {
            get
            {
                var frame = StartFrame;

                if (frame == null)
                    return null;

                Debug.Assert(CurrentTime != null);

                return EndFrame != null ? Interpolation.ValueAt(CurrentTime.Value, frame.Position, EndFrame.Position, frame.Time, EndFrame.Time) : frame.Position;
            }
        }

        public override List<IInput> GetPendingInputs()
        {
            if (!Position.HasValue) return new List<IInput>();

            var actions = new List<CatchAction>();

            if (StartFrame.Dashing)
                actions.Add(CatchAction.Dash);

            if (Position.Value > StartFrame.Position)
                actions.Add(CatchAction.MoveRight);
            else if (Position.Value < StartFrame.Position)
                actions.Add(CatchAction.MoveLeft);

            return new List<IInput>
            {
                new CatchReplayState
                {
                    PressedActions = actions,
                    CatcherX = Position.Value
                },
            };
        }

        public class CatchReplayState : ReplayState<CatchAction>
        {
            public float? CatcherX { get; set; }
        }
    }
}
