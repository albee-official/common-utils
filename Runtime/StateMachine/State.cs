using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CommonUtils.Attributes;

namespace CommonUtils.StateMachines
{
    public enum EStateMachineAvailability
    {
        /// <summary> Makes state machine leave immideatly upon state change. </summary>
        RESOLVED,
        PENDING,
        REJECT,
    }

    /// <summary> Anything inheriting this abstract class can be passed to the StateMachine. You can implement CanEnter() and CanLeave methods for special behaviour. </summary>
    [Serializable]
    public abstract class State
    {
        [field: SerializeField, ReadOnly] public string Name { get; protected set; }

        public State(string name)
        {
            Name = name;
        }

        /// <summary> Returns true if the state can be switched to. </summary>
        public virtual bool CanEnter() {
            return true;
        }

        /// <summary> Returns RESOLVED if the state can be switched from. Returns REJECT if the state is unable to get swtched from, and should not be awaited. Returns PENDING if the state is currently unnable to determine whether it should return, and should be awaited. State machine will call this method until it gets either RESOLVE or REJECT. </summary>
        public virtual EStateMachineAvailability CanLeave() {
            return EStateMachineAvailability.RESOLVED;
        }

        /// <summary> Triggered upon state entry. </summary>
        public abstract void Enter();

        /// <summary> Triggered upon state exit. </summary>
        public abstract void Exit();

        /// <summary> Triggered on every frame to update input. Return true if input has been handled. </summary>
        public abstract bool HandleInput();

        /// <summary> Triggered on every frame, if state machine is enabled. </summary>
        public abstract void Update();
        
        /// <summary> Triggered on every frame after all updates were called, if state machine is enabled. </summary>
        public abstract void LateUpdate();

        /// <summary> For physics stuff, called only if state machine is enabled. </summary>
        public abstract void PhysicsUpdate();


        /// <summary> Triggered upon state entry. </summary>
        public abstract void DrawEnter();

        /// <summary> Triggered upon state exit </summary>
        public abstract void DrawExit();

        /// <summary> Triggered on every frame, if state machine is enabled. </summary>
        public abstract void DrawUpdate();
    }
}

