using System.Collections.Generic;
using UnityEngine;

namespace Orpul1.State
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] GameObject stateObj;
        List<IState> states = new List<IState>();
        IFactoryable<List<IState>> stateFactory;
        IState curState;
        private void Start()
        {
            stateFactory = stateObj.GetComponent<IFactoryable<List<IState>>>();
            if(stateFactory == null)
            {
                Debug.LogError("stateFactory is Null!");
            }
            else
            {
                states = stateFactory.Create();
            }
        }
        public void SetContext<T>(T t)
        {

        }
        public void TransitionState(int index)
        {
            if(curState != null)
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
