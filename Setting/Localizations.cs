﻿// Copyright (c) 2024 RollW
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

using Colossal.Localization;
using Game.SceneFlow;

namespace CrimeRemover.Setting
{
    public static class Localizations
    {
        // TODO: generate keys using ModSettings
        private static readonly Translation[] Translations =
        {
            new Translation("Options.SECTION[CrimeRemover.CrimeRemover.Mod]")
                .AddTranslation(LocaleCode.EnUs, "Crime Remover")
                .AddTranslation(LocaleCode.ZhHans, "犯罪移除器")
                .AddTranslation(LocaleCode.ZhHant, "犯罪移除器"),
            new Translation("Options.OPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.EnableCrimeRemover]")
                .AddTranslation(LocaleCode.EnUs, "Enable Crime Remover")
                .AddTranslation(LocaleCode.ZhHans, "启用犯罪移除器")
                .AddTranslation(LocaleCode.ZhHant, "啟用犯罪移除器"),
            new Translation("Options.OPTION_DESCRIPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.EnableCrimeRemover]")
                .AddTranslation(LocaleCode.EnUs,
                    "Enable or disable the crime remover. When disabled, the set crime values will not be removed, " +
                    "but no new crime values will be set or removed")
                .AddTranslation(LocaleCode.ZhHans, "在此启用或禁用犯罪移除器。在禁用时，已设置的犯罪值不会被移除，但不会再继续设置或移除新的犯罪值")
                .AddTranslation(LocaleCode.ZhHant, "在此啟用或禁用犯罪移除器。在禁用時，已設置的犯罪值不會被移除，但不會再繼續設置或移除新的犯罪值"),
            new Translation(
                    "Options.OPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.RemoveCriminals]")
                .AddTranslation(LocaleCode.EnUs, "Remove Criminals")
                .AddTranslation(LocaleCode.ZhHans, "移除罪犯")
                .AddTranslation(LocaleCode.ZhHant, "移除罪犯"),
            new Translation("Options.OPTION_DESCRIPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.RemoveCriminals]")
                .AddTranslation(LocaleCode.EnUs, "When enabled, all criminals will be continuously removed. " +
                                                 "Manually marked criminals will not be removed.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，将持续移除所有罪犯。被手动标记的罪犯不会被移除。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，將持續移除所有罪犯。被手動標記的罪犯不會被移除。"),
            new Translation("Options.OPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.CrimeBuildingPercentage]")
                .AddTranslation(LocaleCode.EnUs, "Building Crime Percentage")
                .AddTranslation(LocaleCode.ZhHans, "建筑犯罪百分比")
                .AddTranslation(LocaleCode.ZhHant, "建築犯罪百分比"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.CrimeBuildingPercentage]")
                .AddTranslation(LocaleCode.EnUs, "Set the global buildings crime percentage")
                .AddTranslation(LocaleCode.ZhHans, "设置全局建筑犯罪百分比")
                .AddTranslation(LocaleCode.ZhHant, "設置全局建築犯罪百分比"),
            new Translation("Options.OPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.MaxCrime]")
                .AddTranslation(LocaleCode.EnUs, "Max Crime")
                .AddTranslation(LocaleCode.ZhHans, "最大犯罪值")
                .AddTranslation(LocaleCode.ZhHant, "最大犯罪值"),
            new Translation("Options.OPTION_DESCRIPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.MaxCrime]")
                .AddTranslation(LocaleCode.EnUs, "The maximum crime value. When this value is greater than 25000, " +
                                                 "it may cause the city crime rate to be greater than 100%.")
                .AddTranslation(LocaleCode.ZhHans, "设置最大犯罪值。此值大于 25000 时可能导致城市犯罪率大于 100%。")
                .AddTranslation(LocaleCode.ZhHant, "設置最大犯罪值。此值大於 25000 時可能導致城市犯罪率大於 100%。"),
            new Translation("Options.OPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.CrimePercentage]")
                .AddTranslation(LocaleCode.EnUs, "Building Crime Multiplier Rate")
                .AddTranslation(LocaleCode.ZhHans, "建筑犯罪值乘率")
                .AddTranslation(LocaleCode.ZhHant, "建築犯罪值乘率"),
            new Translation("Options.OPTION_DESCRIPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.CrimePercentage]")
                .AddTranslation(LocaleCode.EnUs, "The scale of the building crime percentage, " +
                                                 "the final building crime value is Building Crime Multiplier Rate * Max Crime")
                .AddTranslation(LocaleCode.ZhHans, "建筑犯罪值乘率，最终建筑犯罪值为 建筑犯罪值乘率 * 最大犯罪值")
                .AddTranslation(LocaleCode.ZhHant, "建築犯罪值乘率，最終建築犯罪值為 建築犯罪值乘率 * 最大犯罪值"),
            new Translation("Options.OPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.PolicePatrol]")
                .AddTranslation(LocaleCode.EnUs, "Enable Police Patrol")
                .AddTranslation(LocaleCode.ZhHans, "启用警察巡逻")
                .AddTranslation(LocaleCode.ZhHant, "啟用警察巡邏"),
            new Translation("Options.OPTION_DESCRIPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.PolicePatrol]")
                .AddTranslation(LocaleCode.EnUs,
                    "Enable police patrol. When disabled, the police will no longer patrol." +
                    "If you set the building crime value greater than 0, as this mod will always set the crime value, " +
                    "the police patrol will not reduce the building crime value, " +
                    "it may reduce system load after disabling. " +
                    "Note: Disabling this option will not prevent the police from arriving at the \"Crime Scene\".")
                .AddTranslation(LocaleCode.ZhHans,
                    "启用警察巡逻。禁用后，警察将不再巡逻。" +
                    "如果你设置了建筑犯罪值大于0，由于本Mod会始终设置犯罪值，警察巡逻无法使建筑犯罪值降低，禁用后或可降低部分系统占用。" +
                    "注意：禁用本选项不会阻止警察到达「犯罪现场」。")
                .AddTranslation(LocaleCode.ZhHant,
                    "啟用警察巡邏。禁用後，警察將不再巡邏。" +
                    "如果你設置了建築犯罪值大于0，由於本Mod會始終設置犯罪值，警察巡邏無法使建築犯罪值降低，禁用後或可降低部分系統佔用。" +
                    "注意：禁用本選項不會阻止警察到達「犯罪現場」。"),
            new Translation("Options.OPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.RemoveNotification]")
                .AddTranslation(LocaleCode.EnUs, "Remove Crime Scene")
                .AddTranslation(LocaleCode.ZhHans, "移除犯罪现场")
                .AddTranslation(LocaleCode.ZhHant, "移除犯罪現場"),
            new Translation("Options.OPTION_DESCRIPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.RemoveNotification]")
                .AddTranslation(LocaleCode.EnUs, "Remove the \"Crime Scene\" event. " +
                                                 "Removing the notification also prevents the event from occurring.")
                .AddTranslation(LocaleCode.ZhHans, "移除「犯罪现场」事件。移除通知同时阻止事件产生。")
                .AddTranslation(LocaleCode.ZhHant, "移除「犯罪現場」事件。移除通知同時阻止事件產生。"),
            new Translation("Options.CrimeRemover.CrimeRemover.Mod.NOTIFICATIONTYPE[AlwaysRemove]")
                .AddTranslation(LocaleCode.EnUs, "Always remove")
                .AddTranslation(LocaleCode.ZhHans, "始终移除")
                .AddTranslation(LocaleCode.ZhHant, "始終移除"),
            new Translation("Options.CrimeRemover.CrimeRemover.Mod.NOTIFICATIONTYPE[NeverRemove]")
                .AddTranslation(LocaleCode.EnUs, "Never remove")
                .AddTranslation(LocaleCode.ZhHans, "从不移除")
                .AddTranslation(LocaleCode.ZhHant, "從不移除"),
            new Translation("Options.CrimeRemover.CrimeRemover.Mod.NOTIFICATIONTYPE[OnlyEnable]")
                .AddTranslation(LocaleCode.EnUs, "Only remove when enabled")
                .AddTranslation(LocaleCode.ZhHans, "仅在启用时移除")
                .AddTranslation(LocaleCode.ZhHant, "僅在啟用時移除"),
            new Translation(
                    "Options.CrimeRemover.CrimeRemover.Mod.NOTIFICATIONTYPE[OnlyPercentage]")
                .AddTranslation(LocaleCode.EnUs, "Only remove when the building crime percentage is 0")
                .AddTranslation(LocaleCode.ZhHans, "仅在建筑犯罪百分比为 0 时移除")
                .AddTranslation(LocaleCode.ZhHant, "僅在建築犯罪百分比為 0 時移除"),
            new Translation("Options.GROUP[CrimeRemover.CrimeRemover.Mod.Experimental]")
                .AddTranslation(LocaleCode.EnUs, "Experimental")
                .AddTranslation(LocaleCode.ZhHans, "实验性")
                .AddTranslation(LocaleCode.ZhHant, "實驗性"),
            new Translation("Options.GROUP_DESCRIPTION[CrimeRemover.CrimeRemover.Mod.Experimental]")
                .AddTranslation(LocaleCode.EnUs, "Experimental features, may cause game instability")
                .AddTranslation(LocaleCode.ZhHans, "实验性功能，可能会导致游戏不稳定")
                .AddTranslation(LocaleCode.ZhHant, "實驗性功能，可能會導致遊戲不穩定"),
            new Translation("CrimeRemover.CRIME_REMOVER")
                .AddTranslation(LocaleCode.EnUs, "Crime Remover")
                .AddTranslation(LocaleCode.ZhHans, "犯罪移除器")
                .AddTranslation(LocaleCode.ZhHant, "犯罪移除器"),
            new Translation("CrimeRemover.MarkCriminal")
                .AddTranslation(LocaleCode.EnUs, "Mark as Criminal")
                .AddTranslation(LocaleCode.ZhHans, "标记为罪犯")
                .AddTranslation(LocaleCode.ZhHant, "標記為罪犯"),
            new Translation("CrimeRemover.MarkCriminal.DESC")
                .AddTranslation(LocaleCode.EnUs,
                    "Mark the currently selected citizen as a criminal, remove the current occupation and convert it to a criminal. " +
                    "There is a certain delay in the conversion process.")
                .AddTranslation(LocaleCode.ZhHans, "将当前选中的市民标记为罪犯，将移除当前职业并将其转为罪犯。转换过程存在一定的延迟。")
                .AddTranslation(LocaleCode.ZhHant, "將當前選中的市民標記為罪犯，將移除當前職業並將其轉為罪犯。轉換過程存在一定的延遲。"),
            new Translation("CrimeRemover.RemoveCriminal")
                .AddTranslation(LocaleCode.EnUs, "Remove Criminal Mark")
                .AddTranslation(LocaleCode.ZhHans, "移除罪犯标记")
                .AddTranslation(LocaleCode.ZhHant, "移除罪犯標記"),
            new Translation("CrimeRemover.RemoveCriminal.DESC")
                .AddTranslation(LocaleCode.EnUs,
                    "Remove the criminal mark of the currently selected citizen, restore it to a normal citizen," +
                    " but the original occupation will not be restored.")
                .AddTranslation(LocaleCode.ZhHans, "移除当前选中市民的罪犯标记，将其恢复为普通市民，但原有职业不会恢复。")
                .AddTranslation(LocaleCode.ZhHant, "移除當前選中市民的罪犯標記，將其恢復為普通市民，但原有職業不會恢復。"),
        };

        public static void LoadTranslations()
        {
            foreach (var supportedLocale in GameManager.instance
                         .localizationManager.GetSupportedLocales())
            {
                var locale = FromString(supportedLocale);
                var dictionary = Translation.ToDictionary(Translations, locale);
                GameManager.instance.localizationManager.AddSource(
                    supportedLocale, new MemorySource(dictionary)
                );
            }
        }

        private static LocaleCode FromString(string code)
        {
            return code switch
            {
                "en-US" => LocaleCode.EnUs,
                "zh-HANS" => LocaleCode.ZhHans,
                "zh-HANT" => LocaleCode.ZhHant,
                _ => LocaleCode.EnUs
            };
        }
    }
}