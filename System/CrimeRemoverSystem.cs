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
using Game;
using Game.Buildings;
using Game.Common;
using Game.Tools;
using Unity.Collections;
using Unity.Entities;

namespace CrimeRemover.System
{
    public sealed partial class CrimeRemoverSystem : GameSystemBase
    {
        private EntityQuery _crimeProducersQuery;

        protected override void OnUpdate()
        {
            if (!Mod.Setting.EnableCrimeRemover)
            {
                return;
            }

            var maxCrime = Mod.Setting.MaxCrime;
            var buildingPercentage = Mod.Setting.CrimeBuildingPercentage;
            var crimePercentage = Mod.Setting.CrimePercentage;
            var crimeCoefficient = maxCrime * crimePercentage / 100;

            var entities = _crimeProducersQuery.ToEntityArray(Allocator.Temp);
            var length = entities.Length;

            var wouldBeCrime = 0;
            if (buildingPercentage > 0)
            {
                wouldBeCrime = (int)(length * buildingPercentage / 100);
            }

            for (var index = 0; index < length; index++)
            {
                var entity = entities[index];
                var crimeProducer = EntityManager.GetComponentData<CrimeProducer>(entity);

                if (index < wouldBeCrime)
                {
                    crimeProducer.m_Crime = crimeCoefficient;
                    crimeProducer = CheckCrimePatrolRequest(crimeProducer, !Mod.Setting.PolicePatrol);
                    EntityManager.SetComponentData(entity, crimeProducer);
                    continue;
                }

                if (crimeProducer.m_Crime <= 0)
                {
                    continue;
                }

                crimeProducer.m_Crime = 0;
                crimeProducer = CheckCrimePatrolRequest(crimeProducer);
                EntityManager.SetComponentData(entity, crimeProducer);
            }
        }

        private CrimeProducer CheckCrimePatrolRequest(CrimeProducer crimeProducer, bool remove = true)
        {
            if (!remove)
            {
                return crimeProducer;
            }

            var rawRequest = crimeProducer.m_PatrolRequest;
            if (rawRequest == default || rawRequest == Entity.Null)
            {
                return crimeProducer;
            }

            crimeProducer.m_PatrolRequest = default;
            try
            {
                if (EntityManager.Exists(rawRequest))
                {
                    EntityManager.AddComponent<Deleted>(rawRequest);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return crimeProducer;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            _crimeProducersQuery = GetEntityQuery(
                ComponentType.ReadWrite<CrimeProducer>(),
                ComponentType.Exclude<Deleted>(),
                ComponentType.Exclude<Temp>()
            );

            RequireForUpdate(_crimeProducersQuery);
        }

        public override int GetUpdateInterval(SystemUpdatePhase phase)
        {
            return 1;
        }

        public override int GetUpdateOffset(SystemUpdatePhase phase)
        {
            return -1;
        }
    }
}
