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

using System.Linq;
using Game;
using Game.Common;
using Game.Notifications;
using Game.Prefabs;
using Game.Tools;
using Unity.Collections;
using Unity.Entities;

namespace CrimeRemover.System;

public sealed partial class CrimeNotificationRemoverSystem : GameSystemBase
{
    private EntityQuery _policeConfigurationQuery;
    private EntityQuery _notificationsQuery;


    protected override void OnUpdate()
    {
        if (!Mod.Setting.NeedRemoveNotification())
        {
            return;
        }

        var policeConfigurations = _policeConfigurationQuery
            .ToComponentDataArray<PoliceConfigurationData>(Allocator.Temp);
        if (policeConfigurations.Length == 0)
        {
            return;
        }

        // it should only have one configuration
        var policeConfiguration = policeConfigurations[0];
        var crimeScenePrefab = policeConfiguration.m_CrimeSceneNotificationPrefab;

        var entities = _notificationsQuery
            .ToEntityArray(Allocator.Temp);

        foreach (var entity in from entity in entities
                 let prefabRef = EntityManager.GetComponentData<PrefabRef>(entity)
                 where prefabRef == crimeScenePrefab ||
                       prefabRef.m_Prefab == crimeScenePrefab
                 select entity)
        {
            EntityManager.AddComponent<Deleted>(entity);
        }
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        Mod.GetLogger().Info("Setup CrimeNotificationRemoverSystem.");

        _policeConfigurationQuery = GetEntityQuery(
            ComponentType.ReadOnly<PoliceConfigurationData>(),
            ComponentType.Exclude<Deleted>(),
            ComponentType.Exclude<Temp>()
        );

        _notificationsQuery = GetEntityQuery(
            ComponentType.ReadOnly<Icon>(),
            ComponentType.ReadOnly<PrefabRef>(),
            ComponentType.Exclude<Deleted>(),
            ComponentType.Exclude<Temp>()
        );

        RequireForUpdate(_policeConfigurationQuery);
        RequireForUpdate(_notificationsQuery);
    }
}