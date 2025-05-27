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
        private void FixedUpdate()
        {
            if (curState == null) return;
            curState.OnUpdate();
        }
        public void TransitionState(int index)
        {
            CurState = index;
            if (curState != null)
            {
                curState.OnExit();
            }
            curState = states[index];
            curState.OnStart();
        }

    }
}
