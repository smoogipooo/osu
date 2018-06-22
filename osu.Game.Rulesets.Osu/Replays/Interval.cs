﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;

namespace osu.Game.Rulesets.Osu.Replays
{
    public class Interval : IComparable<Interval>
    {
        public double Start;
        public double End;

        public Interval()
        {
        }

        public Interval(double start, double end)
        {
            Start = start;
            End = end;
        }

        public bool Contains(double value)
        {
            return value >= Start && value <= End;
        }

        public int CompareTo(Interval i)
        {
            if (End < i.Start)
                return -1;
            if (Start > i.End)
                return 1;
            return 0; // Overlap
        }

        public double Clamp(double value)
        {
            return Math.Max(Start, Math.Min(End, value));
        }
    }
}
