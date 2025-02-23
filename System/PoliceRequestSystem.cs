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
using Game.Creatures;
using Game.Economy;
using Game.Pathfind;
using Game.Simulation;
using Unity.Collections;
using Unity.Entities;

namespace CrimeRemover.System;

public sealed partial class PoliceRequestSystem : GameSystemBase
{
    private EntityArchetype _policeEmergencyRequestArchetype;
    private EntityQuery _policeRequestMarkQuery;

    protected override void OnUpdate()
    {
        var policeRequestMarks = _policeRequestMarkQuery.ToEntityArray(
            Allocator.Temp
        );
        foreach (var policeRequestMark in policeRequestMarks)
        {
            if (!EntityManager.Exists(policeRequestMark))
            {
                continue;
            }

            if (EntityManager.HasComponent<Citizen>(policeRequestMark))
            {
                if (EntityManager.HasComponent<Criminal>(policeRequestMark))
                {
                    UpdateCitizenFlag(policeRequestMark);
                }
                else
                {
                    // TODO
                }
            }

            // var policeEmergencyRequest = EntityManager.CreateEntity(_policeEmergencyRequestArchetype);
            // EntityManager.AddComponentData(
            //     policeEmergencyRequest,
            //     new PoliceEmergencyRequest(
            //         policeRequestMark,
            //         policeRequestMark,
            //         4f, PolicePurpose.Emergency)
            // );
            // EntityManager.AddComponentData(policeEmergencyRequest, new RequestGroup(4U));
            // EntityManager.AddComponentData(policeEmergencyRequest, new ServiceRequest(true));
            EntityManager.RemoveComponent<PoliceRequestMark>(policeRequestMark);
        }

        //policeRequestMarks.Dispose();
    }

    private void UpdateCitizenFlag(Entity policeRequestMark)
    {
        var criminal = EntityManager.GetComponentData<Criminal>(policeRequestMark);
        if ((criminal.m_Flags & CriminalFlags.Arrested) != 0
            || (criminal.m_Flags & CriminalFlags.Prisoner) != 0)
        {
            return;
        }

        criminal.m_Flags = CriminalFlags.Robber | CriminalFlags.Arrested;
        criminal.m_JailTime = 500;
        EntityManager.SetComponentData(policeRequestMark, criminal);

        var currentTransport = EntityManager.GetComponentData<CurrentTransport>(policeRequestMark);
        var transportEntity = currentTransport.m_CurrentTransport;

        if (!EntityManager.HasComponent<Resident>(transportEntity))
        {
            Mod.GetLogger().Info($"CurrentTransport {transportEntity} does not have Resident component.");
            return;
        }

        // TODO
        var resident = EntityManager.GetComponentData<Resident>(transportEntity);
        resident.m_Flags = ResidentFlags.None;
        resident.m_Timer = 0;
        EntityManager.SetComponentData(transportEntity, resident);

        if (EntityManager.HasComponent<Target>(transportEntity))
        {
            var target = EntityManager.GetComponentData<Target>(transportEntity);
            target.m_Target = Entity.Null;
            EntityManager.SetComponentData(transportEntity, target);
        }

        // if (EntityManager.HasBuffer<PathElement>(transportEntity))
        // {
        //     EntityManager.GetBuffer<PathElement>(transportEntity).Clear();
        // }
        //
        // if (EntityManager.HasComponent<PathOwner>(transportEntity))
        // {
        //     var pathOwner = EntityManager.GetComponentData<PathOwner>(transportEntity);
        //     pathOwner.m_State = PathFlags.Pending | PathFlags.Failed;
        //     pathOwner.m_ElementIndex = 0;
        //     EntityManager.SetComponentData(transportEntity, pathOwner);
        // }

        TravelPurpose travelPurpose = new()
        {
            m_Purpose = Purpose.GoingToJail,
            m_Resource = Resource.NoResource,
            m_Data = 0
        };
        EntityManager.AddComponentData(policeRequestMark, travelPurpose);
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        _policeEmergencyRequestArchetype = EntityManager.CreateArchetype(
            ComponentType.ReadWrite<ServiceRequest>(),
            ComponentType.ReadWrite<PoliceEmergencyRequest>(),
            ComponentType.ReadWrite<RequestGroup>()
        );
        _policeRequestMarkQuery = GetEntityQuery(new EntityQueryDesc
        {
            All =
            [
                ComponentType.ReadWrite<PoliceRequestMark>()
            ],
            None =
            [
                ComponentType.ReadOnly<Deleted>()
            ]
        });
        RequireForUpdate(_policeRequestMarkQuery);
    }
}