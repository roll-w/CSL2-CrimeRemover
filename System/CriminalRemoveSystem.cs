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
using Game.Tools;
using Unity.Collections;
using Unity.Entities;

namespace CrimeRemover.System;

public sealed partial class CriminalRemoveSystem : GameSystemBase
{
    private EntityQuery _criminalQuery;

    protected override void OnUpdate()
    {
        if (!Mod.Setting.EnableCrimeRemover || !Mod.Setting.RemoveCriminals)
        {
            return;
        }

        var criminals = _criminalQuery.ToEntityArray(Allocator.Temp);

        foreach (var entity in criminals)
        {
            var criminal = EntityManager.GetComponentData<Criminal>(entity);
            var cEvent = criminal.m_Event;

            EntityManager.RemoveComponent<Criminal>(entity);

            if (cEvent != Entity.Null && EntityManager.Exists(cEvent))
            {
                EntityManager.AddComponent<Deleted>(cEvent);
            }
        }
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        Mod.GetLogger().Info("Setup CriminalRemoveSystem.");

        _criminalQuery = GetEntityQuery(
            ComponentType.ReadWrite<Criminal>(),
            ComponentType.Exclude<Deleted>(),
            ComponentType.Exclude<Temp>()
        );

        RequireForUpdate(_criminalQuery);
    }
}
