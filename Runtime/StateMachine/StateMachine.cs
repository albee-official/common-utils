using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CommonUtils.Attributes;

namespace CommonUtils.StateMachines
{
    /// <summary> A basic state machine class. </summary>
    [Serializable]
    public abstract class StateMachine
    {
        /// <summary> Current state machine state </summary>
        [field: Header("State Machine")]
        [field: Space(6f)] [field: SerializeField] [field: ReadOnly] protected State currentState;

        /// <summary> Array of all states, that implement a "ConstantUpdate()" method and need to be updated every frame </summary>
        [field: Space(6f)] [field: SerializeField] [field: ReadOnly] protected State[] constantStates;

        /// <summary> Disabled state machines won't update their current state. </summary>
        [field: Space(2f)] [field: SerializeField] [field: ReadOnly] protected bool enabled = true;

        /// <summary> Returns whether the state machine is currently waiting for a pending state. </summary>
        [field: Space(2f)] [field: SerializeField] [field: ReadOnly] protected bool pending = false;
        
        /// <summary> State that is pending to be switched into. </summary>
        [field: Space(2f)] [field: SerializeField] [field: ReadOnly] protected State pendingState;



        /// <summary> Current state machine state </summary>
        public State CurrentState { get { return currentState; } }

        /// <summary> Array of all states, that implement a "ConstantUpdate()" method and need to be updated every frame </summary>
        public State[] ConstantStates { get { return constantStates; } }

        /// <summary> Disabled state machines won't update their current state. </summary>
        public bool Enabled { get { return enabled; } }

        /// <summary> Returns whether the state machine is currently waiting for a pending state. </summary>
        public bool Pending { get { return pending; } }
        
        /// <summary> State that is pending to be switched into. </summary>
        public State PendingState { get { return pendingState; } }

        /// <summary> Disable the state machine. </summary>
        public void Disable() {
            OnDisabled();
            enabled = false;
        }

        /// <summary> Enable the state machine. </summary>
        public void Enable() {
            OnEnabled();
            enabled = true;
        }

        /// <summary> Disable state machine if it is enabled. Disable otherwise. </summary>
        public void Toggle() {
            enabled = !enabled;
            if (enabled) Enable();
            else Disable();
        }

        /// <summary> Callback method for when the state machine is enabled. </summary>
        protected virtual void OnEnabled() {
        }

        /// <summary> Callback method for when the state machine is disabled. </summary>
        protected virtual void OnDisabled() {
        }

        /// <summary> Change state machine state. Returns true if state has successfuly been changed. </summary>
        public virtual bool ChangeState(State newState) {
            if (pending || newState == null) return false;

            if ( !newState.CanEnter() ) return false;

            if (currentState != null) {
                switch (currentState.CanLeave()) {
                    case EStateMachineAvailability.PENDING:
                        pending = true;
                        pendingState = newState;
                        return false;
                    
                    case EStateMachineAvailability.REJECT:
                        return false;
                }
            }

            onStateChange?.Invoke(currentState, newState);

            // Upon first launch, current state is null.
            currentState?.Exit();
            currentState?.DrawExit();
            currentState = newState;
            currentState.DrawEnter();
            currentState.Enter();

            return true;
        }

        /// <summary> Update method current state, as well as all constantUpdate states. </summary>
        public virtual void Update() {
            if (pending) {
                EStateMachineAvailability canLeave = currentState.CanLeave();

                switch (canLeave) {
                    case EStateMachineAvailability.RESOLVED:
                        pending = false;
                        ChangeState(pendingState);
                        pendingState = null;
                        return;
                    
                    case EStateMachineAvailability.REJECT:
                        pending = false;
                        pendingState = null;
                        return;
                }
            }


            if (enabled) {
                currentState?.DrawUpdate();
                currentState?.Update();
            }
        }

        public virtual void LateUpdate() {
            if (enabled) currentState?.LateUpdate();
        }

        public virtual void PhysicsUpdate() {
            if (enabled) currentState?.PhysicsUpdate();
        }

        public delegate void StateChangedEventHandler(State oldState, State newState);

        public event StateChangedEventHandler onStateChange;
    }
}
