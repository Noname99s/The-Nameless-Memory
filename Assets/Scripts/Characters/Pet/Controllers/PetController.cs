using Orpul1.State;
using UnityEngine;

public class PetController : MonoBehaviour
{
    [SerializeField] StateController stateController;
    
    void Start()
    {
        stateController.TransitionState((int)EPetState.Follow);
    }

    void Update()
    {
        
    }
}
