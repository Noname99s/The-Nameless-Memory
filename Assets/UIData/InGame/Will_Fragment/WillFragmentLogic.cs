using UnityEngine;

public class WillFragmentLogic : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Will Fragment collected!");
            Managers.Map.getFragment = true;
            Destroy(gameObject);
        }
    }
}
