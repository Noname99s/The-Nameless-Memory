using System.Collections.Generic;
using Orpul1.Player;
using UnityEngine;

namespace Orpul1.State
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] GameObject stateObj;
        List<IState> states = new List<IState>();
        IStateFactory<List<IState>> stateFactory;
        IState curState;
        [SerializeField] EPlayerState state;
        private void Start()
        {
            stateFactory = stateObj.GetComponent<IStateFactory<List<IState>>>();
            if(stateFactory == null)
            {
                Debug.LogError("stateFactory is Null!");
            }
            else
            {
                states = stateFactory.Create(TransitionState);
            }
        }
        public void SetContext<T>(T t)
        {

        }
        public void TransitionState(int index)
        {
            state = (EPlayerState)index;
            if (curState != null)
            {
                curState.OnExit();
            }
            curState = states[index];
            curState.OnStart();
        }
        public void Process()
        {
            curState.OnUpdate();
        }
    }
}
