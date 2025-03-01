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

using Colossal.UI.Binding;
using Game;
using Game.Citizens;
using Game.Events;
using Game.UI;
using Game.UI.InGame;
using StationNaming.System;
using Unity.Entities;

namespace CrimeRemover.System;

public partial class UIBindingSystem : UISystemBase
{
    private Entity _selectedEntity;
    private SelectedInfoUISystem _selectedInfoUISystem;
    private ValueBinding<Entity> _selectedEntityBinding;

    public override GameMode gameMode => GameMode.Game;

    protected override void OnUpdate()
    {
        base.OnUpdate();
        _selectedEntityBinding.Update(_selectedInfoUISystem.selectedEntity);
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        _selectedInfoUISystem =
            World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<SelectedInfoUISystem>();

        AddBinding(new TriggerBinding<Entity>(Mod.Name, "setSelectedEntity", SetSelectedEntity));

        AddBinding(
            _selectedEntityBinding = new ValueBinding<Entity>(
                Mod.Name,
                "selectedEntity",
                Entity.Null
            )
        );

        AddBinding(new TriggerBinding<Entity>(Mod.Name, "markCriminal", MarkCriminal));
        AddBinding(new TriggerBinding<Entity>(Mod.Name, "removeCriminal", RemoveCriminal));
        AddUpdateBinding(new GetterValueBinding<bool>(Mod.Name, "isCriminal", IsCriminal));
        AddUpdateBinding(
            new GetterValueBinding<bool>(Mod.Name, "isShowCitizenPanel", IsShowCitizenPanel)
        );
        AddBinding(new TriggerBinding<Entity>(Mod.Name, "callPolice", CallPolice));
    }

    internal void SetSelectedEntity(Entity entity)
    {
        if (_selectedEntity == entity && entity != Entity.Null && entity != default)
        {
            if (!EntityManager.HasComponent<Selected>(entity) && EntityManager.Exists(entity))
            {
                EntityManager.AddComponent<Selected>(entity);
            }
            return;
        }

        if (
            _selectedEntity != Entity.Null
            && EntityManager.HasComponent<Selected>(_selectedEntity)
            && EntityManager.Exists(_selectedEntity)
        )
        {
            EntityManager.RemoveComponent<Selected>(_selectedEntity);
        }

        if ((entity != Entity.Null || entity != default) && EntityManager.Exists(entity))
        {
            EntityManager.AddComponent<Selected>(entity);
        }

        _selectedEntity = entity;
    }

    internal void MarkCriminal(Entity entity)
    {
        if (entity == Entity.Null || entity == default)
        {
            return;
        }

        if (!EntityManager.Exists(entity))
        {
            return;
        }

        if (!EntityManager.HasComponent<Citizen>(entity))
        {
            return;
        }

        if (
            EntityManager.HasComponent<AddCriminalMark>(entity)
            || EntityManager.HasComponent<CriminalMark>(entity)
            || EntityManager.HasComponent<AddCriminal>(entity)
            || EntityManager.HasComponent<Criminal>(entity)
        )
        {
            return;
        }

        EntityManager.AddComponent<AddCriminalMark>(entity);
    }

    internal void RemoveCriminal(Entity entity)
    {
        if (entity == Entity.Null || entity == default)
        {
            return;
        }

        if (!EntityManager.Exists(entity))
        {
            return;
        }

        if (!EntityManager.HasComponent<Citizen>(entity))
        {
            return;
        }

        if (
            !EntityManager.HasComponent<Criminal>(entity)
            && !EntityManager.HasComponent<CriminalMark>(entity)
        )
        {
            return;
        }

        if (EntityManager.HasComponent<CriminalMark>(entity))
        {
            EntityManager.RemoveComponent<CriminalMark>(entity);
        }

        if (EntityManager.HasComponent<Criminal>(entity))
        {
            EntityManager.RemoveComponent<Criminal>(entity);
        }
    }

    internal bool IsCriminal()
    {
        var entity = _selectedInfoUISystem.selectedEntity;
        SetSelectedEntity(entity);

        if (entity == Entity.Null || entity == default)
        {
            return false;
        }

        return EntityManager.HasComponent<Criminal>(entity)
            || EntityManager.HasComponent<CriminalMark>(entity);
    }

    internal bool IsShowCitizenPanel()
    {
        var entity = _selectedInfoUISystem.selectedEntity;
        SetSelectedEntity(entity);

        if (entity == Entity.Null || entity == default)
        {
            return false;
        }

        return EntityManager.HasComponent<Citizen>(entity);
    }

    internal void CallPolice(Entity entity)
    {
        if (entity == Entity.Null || entity == default)
        {
            return;
        }

        if (EntityManager.Exists(entity))
        {
            EntityManager.AddComponent<PoliceRequestMark>(entity);
        }
    }
}
