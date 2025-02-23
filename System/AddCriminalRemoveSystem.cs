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

using Game;
using Game.Citizens;
using Game.Common;
using Game.Events;
using Game.Tools;
using Unity.Collections;
using Unity.Entities;

namespace CrimeRemover.System;

public sealed partial class AddCriminalRemoveSystem : GameSystemBase
{
    private EntityQuery _crimeQuery;
    private EntityQuery _addCriminalQuery;

    protected override void OnUpdate()
    {
        if (!Mod.Setting.EnableCrimeRemover || !Mod.Setting.RemoveCriminals)
        {
            return;
        }

        var addCriminals = _addCriminalQuery
            .ToEntityArray(Allocator.Temp);

        foreach (var entity in addCriminals)
        {
            if (EntityManager.HasComponent<CriminalMark>(entity))
            {
                continue;
            }

            var addCriminal = EntityManager.GetComponentData<AddCriminal>(entity);
            var cEvent = addCriminal.m_Event;

            EntityManager.RemoveComponent<AddCriminal>(entity);

            if (cEvent != Entity.Null)
            {
                EntityManager.AddComponent<Deleted>(cEvent);
            }
        }

        var crimes = _crimeQuery.ToEntityArray(Allocator.Temp);

        foreach (var entity in crimes)
        {
            if (EntityManager.HasComponent<CriminalMark>(entity))
            {
                continue;
            }
            EntityManager.AddComponent<Deleted>(entity);
        }
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        Mod.GetLogger().Info("Setup AddCriminalRemoveSystem.");

        _addCriminalQuery = GetEntityQuery(
            ComponentType.ReadOnly<AddCriminal>(),
            ComponentType.Exclude<Deleted>(),
            ComponentType.Exclude<Temp>()
        );

        _crimeQuery = GetEntityQuery(
            ComponentType.ReadOnly<Crime>(),
            ComponentType.Exclude<Deleted>(),
            ComponentType.Exclude<Temp>()
        );

        RequireForUpdate(_addCriminalQuery);
        RequireForUpdate(_crimeQuery);
    }
}