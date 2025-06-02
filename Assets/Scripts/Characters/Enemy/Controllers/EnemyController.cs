using System;
using Orpul1;
using Orpul1.Enemy.State;
using Orpul1.State;
using UnityEngine;
using UnityEngine.Experimental.AI;
namespace Orpul1.Enemy
{
    public class EnemyController : MonoBehaviour, ISetDataAble<Action<GameObject>>, IResetAble
    {
        [SerializeField] StateController stateController;
        Action<GameObject> deactiveAction;
        [SerializeField] EEnemyState state;
        Ray ray;
        RaycastHit2D hit;

        private void Start()
        {
            stateController.TransitionState((int)state);
        }
        void Update()
        {
        }

        private void Sight()
        {
            
        }

        private void FixedUpdate()
        {
            stateController.ProcessState();
        }
        public void ResetValue()
        {
        }

        public void SetValue(Action<GameObject> enqueueAction)
        {
            this.deactiveAction = enqueueAction;
        }
    }
}
