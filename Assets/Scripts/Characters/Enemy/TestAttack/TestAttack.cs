using UnityEngine;

public class TestAttack : MonoBehaviour
{
    [SerializeField] int damageAmount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(ETag.Player.ToString())) return;
        TakeKnockBack(other.GetComponent<IknockbackAble>(), other.transform.position);
        TakeDamage(other.GetComponent<IDamageAble>());
    }
    private void TakeDamage(IDamageAble target)
    {
        if (target == null) return;
        target.OnDamage(damageAmount);
    }
    private void TakeKnockBack(IknockbackAble target,Vector2 targetPosi)
    {
        if (target == null) return;
        Vector2 pushDir = (Vector2)transform.position - targetPosi;
        target.knockback(-pushDir.normalized);
    }
}
