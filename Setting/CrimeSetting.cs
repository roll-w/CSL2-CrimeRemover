// Copyright (c) 2024 RollW
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;

namespace CrimeRemover.Setting
{
    [FileLocation(Mod.Name)]
    [SettingsUIShowGroupName(Experimental)]
    public class CrimeSetting : ModSetting
    {
        public CrimeSetting(IMod mod) : base(mod)
        {
        }

        private const string Experimental = "Experimental";

        /**
         * Enable or disable the crime remover
         */
        public bool EnableCrimeRemover { get; set; } = true;

        public bool IsDisabled()
        {
            return !EnableCrimeRemover;
        }

        [SettingsUISection(Experimental)]
        [SettingsUIDisableByCondition(typeof(CrimeSetting), nameof(IsDisabled))]
        public bool RemoveCriminals { get; set; } = true;

        /**
         * Set the global buildings crime percentage
         *
         * Must be between 0 and 100
         */
        [SettingsUISlider(max = 100, min = 0, step = 1)]
        [SettingsUISection(Experimental)]
        public float CrimeBuildingPercentage { get; set; } = 0;

        /**
         * The maximum crime value
         */
        [SettingsUISlider(max = 100000, min = 0, step = 1000)]
        [SettingsUISection(Experimental)]
        public float MaxCrime { get; set; } = 25000;

        /**
         * The scale of the building crime percentage
         */
        [SettingsUISlider(max = 100, min = 0, step = 1)]
        [SettingsUISection(Experimental)]
        public float CrimePercentage { get; set; } = 0;

        /**
         * Enable police patrol
         */
        [SettingsUISection(Experimental)]
        public bool PolicePatrol { get; set; } = true;

        /**
         * Remove the notification when the crime is removed
         */
        [SettingsUISection(Experimental)]
        public NotificationType RemoveNotification { get; set; } = NotificationType.NeverRemove;

        public bool NeedRemoveNotification()
        {
            return RemoveNotification switch
            {
                NotificationType.AlwaysRemove => true,
                NotificationType.NeverRemove => false,
                NotificationType.OnlyEnable => EnableCrimeRemover,
                // if the building crime percentage is greater than 0,
                // then we will not remove the notification
                NotificationType.OnlyPercentage => EnableCrimeRemover && CrimePercentage == 0,
                _ => false
            };
        }

        public override void SetDefaults()
        {
            EnableCrimeRemover = true;
            CrimeBuildingPercentage = 0;
            CrimePercentage = 0;
            MaxCrime = 25000;
            PolicePatrol = true;
            RemoveNotification = NotificationType.NeverRemove;
        }

        public override void Apply()
        {
            base.Apply();
        }
    }
}