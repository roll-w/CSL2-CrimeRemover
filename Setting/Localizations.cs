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

using Colossal.Localization;
using Game.SceneFlow;

namespace CrimeRemover.Setting
{
    public static class Localizations
    {
        private static readonly Translation[] Translations =
        [
            new Translation("Options.SECTION[CrimeRemover.CrimeRemover.Mod]")
                .AddTranslation(LocaleCode.EnUs, "Crime Remover")
                .AddTranslation(LocaleCode.ZhHans, "犯罪移除器")
                .AddTranslation(LocaleCode.ZhHant, "犯罪移除器"),
            new Translation("Options.OPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.EnableCrimeRemover]")
                .AddTranslation(LocaleCode.EnUs, "Enable Crime Remover")
                .AddTranslation(LocaleCode.ZhHans, "启用犯罪移除器")
                .AddTranslation(LocaleCode.ZhHant, "啟用犯罪移除器"),
            new Translation("Options.OPTION_DESCRIPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.EnableCrimeRemover]")
                .AddTranslation(LocaleCode.EnUs, "Enable or disable the crime remover. When disabled, the set crime values will not be removed, " +
                                                 "but no new crime values will be set or removed")
                .AddTranslation(LocaleCode.ZhHans, "在此启用或禁用犯罪移除器。在禁用时，已设置的犯罪值不会被移除，但不会再继续设置或移除新的犯罪值")
                .AddTranslation(LocaleCode.ZhHant, "在此啟用或禁用犯罪移除器。在禁用時，已設置的犯罪值不會被移除，但不會再繼續設置或移除新的犯罪值"),
            new Translation("Options.OPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.CrimeBuildingPercentage]")
                .AddTranslation(LocaleCode.EnUs, "Building Crime Percentage")
                .AddTranslation(LocaleCode.ZhHans, "建筑犯罪百分比")
                .AddTranslation(LocaleCode.ZhHant, "建築犯罪百分比"),
            new Translation("Options.OPTION_DESCRIPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.CrimeBuildingPercentage]")
                .AddTranslation(LocaleCode.EnUs, "Set the global buildings crime percentage")
                .AddTranslation(LocaleCode.ZhHans, "设置全局建筑犯罪百分比")
                .AddTranslation(LocaleCode.ZhHant, "設置全局建築犯罪百分比"),
            new Translation("Options.OPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.MaxCrime]")
                .AddTranslation(LocaleCode.EnUs, "Max Crime")
                .AddTranslation(LocaleCode.ZhHans, "最大犯罪值")
                .AddTranslation(LocaleCode.ZhHant, "最大犯罪值"),
            new Translation("Options.OPTION_DESCRIPTION[CrimeRemover.CrimeRemover.Mod.CrimeSetting.MaxCrime]")
                .AddTranslation(LocaleCode.EnUs, "The maximum crime value")
                .AddTranslation(LocaleCode.ZhHans, "设置最大犯罪值")
                .AddTranslation(LocaleCode.ZhHant, "設置最大犯罪值"),
            new Translation("Options.OPTION_DESCRIPTION[CrimeRemover.MaxCrime]")
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
            new Translation("Options.GROUP[CrimeRemover.CrimeRemover.Mod.Experimental]")
                .AddTranslation(LocaleCode.EnUs, "Experimental")
                .AddTranslation(LocaleCode.ZhHans, "实验性")
                .AddTranslation(LocaleCode.ZhHant, "實驗性"),
            new Translation("Options.GROUP_DESCRIPTION[CrimeRemover.CrimeRemover.Mod.Experimental]")
                .AddTranslation(LocaleCode.EnUs, "Experimental features, may cause game instability")
                .AddTranslation(LocaleCode.ZhHans, "实验性功能，可能会导致游戏不稳定")
                .AddTranslation(LocaleCode.ZhHant, "實驗性功能，可能會導致遊戲不穩定"),
        ];

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