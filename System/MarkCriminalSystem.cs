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
using Unity.Collections;
using Unity.Entities;
using Event = Game.Events.Event;

namespace CrimeRemover.System
{
    public sealed partial class MarkCriminalSystem : GameSystemBase
    {
        private EntityQuery _addCriminalMarkQuery;
        private EntityArchetype _criminalEventArchetype;

        protected override void OnUpdate()
        {
            var addCriminalMarks = _addCriminalMarkQuery.ToEntityArray(Allocator.Temp);

            foreach (var addCriminalMarkEntity in addCriminalMarks)
            {
                if (!EntityManager.Exists(addCriminalMarkEntity))
                {
                    continue;
                }

                if (!EntityManager.HasComponent<Citizen>(addCriminalMarkEntity))
                {
                    EntityManager.RemoveComponent<AddCriminalMark>(addCriminalMarkEntity);
                    continue;
                }

                var crimeEventEntity = EntityManager.CreateEntity(_criminalEventArchetype);

                var addCriminal = new AddCriminal
                {
                    m_Event = crimeEventEntity,
                    m_Target = addCriminalMarkEntity,
                    m_Flags = CriminalFlags.Robber | CriminalFlags.Planning,
                };

                var targetElement = new TargetElement { m_Entity = addCriminalMarkEntity };

                var dynamicBuffer = GetBuffer<TargetElement>(crimeEventEntity);
                dynamicBuffer.Add(targetElement);

                var criminal = new Criminal(crimeEventEntity, addCriminal.m_Flags);
                EntityManager.AddComponentData(crimeEventEntity, addCriminal);
                EntityManager.AddComponentData(addCriminalMarkEntity, criminal);
                EntityManager.AddComponent<CriminalMark>(addCriminalMarkEntity);
                EntityManager.RemoveComponent<AddCriminalMark>(addCriminalMarkEntity);

                // Remove Worker, if any. Only a jobless citizen can be a criminal.
                if (EntityManager.HasComponent<Worker>(addCriminalMarkEntity))
                {
                    EntityManager.RemoveComponent<Worker>(addCriminalMarkEntity);
                }
            }
        }

        private DynamicBuffer<T> GetBuffer<T>(Entity entity)
            where T : unmanaged, IBufferElementData
        {
            return EntityManager.HasBuffer<T>(entity)
                ? EntityManager.GetBuffer<T>(entity)
                : EntityManager.AddBuffer<T>(entity);
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            _addCriminalMarkQuery = GetEntityQuery(
                new EntityQueryDesc
                {
                    All = new[] { ComponentType.ReadWrite<AddCriminalMark>() },
                    None = new[] { ComponentType.ReadOnly<CriminalMark>(), ComponentType.ReadOnly<Deleted>() },
                }
            );

            _criminalEventArchetype = EntityManager.CreateArchetype(
                ComponentType.ReadWrite<Event>(),
                ComponentType.ReadWrite<AddCriminal>()
            );

            RequireForUpdate(_addCriminalMarkQuery);
        }
    }
}