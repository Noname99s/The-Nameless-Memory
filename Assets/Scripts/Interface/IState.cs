using UnityEngine;

namespace Orpul1.State
{
    public interface IState
    {
        public void OnStart();
        public void OnUpdate();
        public void OnExit();
    }

}
