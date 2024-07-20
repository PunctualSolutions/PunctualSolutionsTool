#region

using System;
using System.Collections.Generic;

#endregion

namespace PunctualSolutions.Tool.StateMachine
{
    /// <summary>
    ///     所有状态机的基类
    ///     请勿在子类中使用 Update() 和 FixedUpdate()
    /// </summary>
    public class StateMachine
    {
        //状态字典，获取状态用，它将由子类的数组赋值
        protected Dictionary<Type, IState> stateDic;

        //当前状态
        public IState CurrentState { get; set; }
        public IState LastState    { get; set; }

        public virtual void Update()
        {
            CurrentState?.LogicUpdate();
        }

        public virtual void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }

        public virtual void Begin(IState state)
        {
            CurrentState = state;
            LastState    = null;
            CurrentState.Enter();
        }

        public virtual void Begin(Type stateType)
        {
            Begin(stateDic[stateType]);
        }

        public virtual void Begin<T>() where T : IState
        {
            Begin(typeof(T));
        }

        public virtual void SwitchState(IState state)
        {
            CurrentState.Exit();
            LastState    = CurrentState;
            CurrentState = state;
            CurrentState.Enter();
        }

        public virtual void SwitchState(Type stateType)
        {
            SwitchState(stateDic[stateType]);
        }

        public virtual void SwitchState<T>() where T : IState
        {
            SwitchState(typeof(T));
        }
    }
}