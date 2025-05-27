using UnityEngine;

public class TestAttack : MonoBehaviour
{
    [SerializeField] int damageAmount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(ETag.Player.ToString())) return;
        IDamageAble target = other.GetComponent<IDamageAble>();
        target.OnDamage(damageAmount);
    }
}
