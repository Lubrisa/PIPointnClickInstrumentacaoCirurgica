using UnityEngine;

namespace CustomTools.StateMachine
{
    /// <summary>
    /// Base class for creating state machines.
    /// </summary>
    public abstract class BaseStateMachine : MonoBehaviour
    {
        private BaseState m_currentState;

        protected virtual void Start() => m_currentState.EnterState();

        protected virtual void Update() => m_currentState.LogicUpdate();

        protected virtual void FixedUpdate() => m_currentState.PhysicsUpdate();

        protected virtual void SwitchCurrentState()
        {

        }
    }
}