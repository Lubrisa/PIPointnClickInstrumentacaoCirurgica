using System.Collections.Generic;

namespace CustomTools.StateMachine
{
    public abstract class BaseState
    {
        private BaseStateMachine m_machine;
        private List<BaseState> m_subStates;

        public BaseState(BaseStateMachine machine)
        {
            m_machine = machine;
        }

        public abstract void EnterState();

        public abstract void LogicUpdate();

        public abstract void PhysicsUpdate();

        public abstract void StateSwitchCheck();

        public abstract void ExitState();

        public virtual void EnterSubState(BaseState exitingSubState, BaseState enteringSubState)
        {
            if (m_subStates.Contains(exitingSubState))
            {
                exitingSubState.ExitState();
                m_subStates.Remove(exitingSubState);
            }
            enteringSubState.EnterState();
            m_subStates.Add(enteringSubState);
        }
    }
}