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

using System;
using Colossal.IO.AssetDatabase;
using Colossal.Json;
using Colossal.Logging;
using CrimeRemover.Setting;
using CrimeRemover.System;
using Game;
using Game.Modding;
using Game.SceneFlow;

namespace CrimeRemover
{
    public class Mod : IMod
    {
        public const string Name = "RollW_CrimeRemover";
        private static readonly ILog LOG = LogManager.GetLogger($"{Name}.Mod")
            .SetShowsErrorsInUI(true);

        public static CrimeSetting Setting { get; private set; }

        public void OnLoad(UpdateSystem updateSystem)
        {
            try
            {
                LOG.Info(nameof(OnLoad));
                SetupOnLoad(updateSystem);
            }
            catch (Exception e)
            {
                LOG.Error(e, "Error during OnLoad");
            }
        }

        private void SetupOnLoad(UpdateSystem updateSystem)
        {
            if (GameManager.instance.modManager
                .TryGetExecutableAsset(this, out var asset))
            {
                LOG.Info($"Current mod asset at {asset.path}");
            }
            Setting = new CrimeSetting(this);
            Setting.RegisterInOptionsUI();
            Localizations.LoadTranslations();

            AssetDatabase.global.LoadSettings(Name,
                Setting, new CrimeSetting(this)
            );

            LOG.Info($"Load settings: {Setting.ToJSONString()}");

            updateSystem.UpdateAfter<CrimeNotificationRemoverSystem>(SystemUpdatePhase.Deserialize);
            updateSystem.UpdateAfter<CrimeRemoverSystem>(SystemUpdatePhase.Deserialize);
            updateSystem.UpdateAfter<CrimeRemoverSystem>(SystemUpdatePhase.GameSimulation);
        }

        public void OnDispose()
        {
            LOG.Info(nameof(OnDispose));
        }

        public static ILog GetLogger() => LOG;
    }
}