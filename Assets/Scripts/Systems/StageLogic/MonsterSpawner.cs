using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab;
    void Start()
    {
        Instantiate(monsterPrefab, transform.position, Quaternion.identity);
    }
}
