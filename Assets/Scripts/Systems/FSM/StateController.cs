using Orpul1.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Orpul1.State
{
    public class StateController : MonoBehaviour
    {
        List<IState> states = new List<IState>();
        [SerializeField] StateFactory stateFactory;
        IState curState;
        public int CurState;
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
        
        public void ProcessState()
        {
            if (curState == null) return;
            curState.OnUpdate();
        }
        public void TransitionState(int index)
        {
            if (curState != null)
                curState.OnExit();

            CurState = index;
            curState = states[index];
            curState.OnStart();
        }

    }
}
