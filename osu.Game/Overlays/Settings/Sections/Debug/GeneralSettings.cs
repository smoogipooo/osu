﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;

namespace osu.Game.Overlays.Settings.Sections.Debug
{
    public class GeneralSettings : SettingsSubsection
    {
        protected override string Header => "General";

        [BackgroundDependencyLoader]
        private void load(FrameworkDebugConfigManager config, FrameworkConfigManager frameworkConfig)
        {
            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "Show log overlay",
                    Bindable = frameworkConfig.GetBindable<bool>(FrameworkSetting.ShowLogOverlay)
                },
                new SettingsCheckbox
                {
                    LabelText = "Performance logging",
                    Bindable = frameworkConfig.GetBindable<bool>(FrameworkSetting.PerformanceLogging)
                },
                new SettingsCheckbox
                {
                    LabelText = "Bypass caching (slow)",
                    Bindable = config.GetBindable<bool>(DebugSetting.BypassCaching)
                },
                new SettingsCheckbox
                {
                    LabelText = "Depth pre-pass",
                    Bindable = config.GetBindable<bool>(DebugSetting.DepthPrePass)
                },
                new SettingsCheckbox
                {
                    LabelText = "Depth testing",
                    Bindable = config.GetBindable<bool>(DebugSetting.DepthTesting)
                }
            };
        }
    }
}
