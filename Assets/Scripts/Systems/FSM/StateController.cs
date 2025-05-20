using System.Collections.Generic;
using Orpul1.Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace Orpul1.State
{
    public class StateController : MonoBehaviour
    {
        List<IState> states = new List<IState>();
        [SerializeField] StateFactory stateFactory;
        IState curState;
        public int CurState { get { return (int)CurState; } }
        private void Awake()
        {
            if(stateFactory == null)
            {
                Debug.LogError("stateFactory is Null!");
            }
            else
            {
                states = stateFactory.Create(TransitionState);
            }
        }
        private void FixedUpdate()
        {
            if (curState == null) return;
            curState.OnUpdate();
        }
        public void TransitionState(int index)
        {
            if (curState != null)
            {
                curState.OnExit();
            }
            curState = states[index];
            curState.OnStart();
        }

    }
}
