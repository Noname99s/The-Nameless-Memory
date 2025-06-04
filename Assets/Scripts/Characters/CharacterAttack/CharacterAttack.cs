using Orpul1;
using UnityEngine;

public class CharacterAttack : MonoBehaviour, ISetDataAble<int>
{
    [SerializeField] ETag attackAbleTag;
    [SerializeField] Collider2D attackCollider;
    int damage = 0;
    private void OnEnable()
    {
        attackCollider.enabled = false;
    }
    public void SetValue(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(attackAbleTag.ToString()))
            return;

        //KnockbackTarget(other.GetComponent<IknockbackAble>(),other.transform.position);
        DamageTarget(other.GetComponent<IDamageAble>());
    }
    private void DamageTarget(IDamageAble target)
    {
        if (target == null) return;
        target.OnDamage(damage);
    }
    private void KnockbackTarget(IknockbackAble target,Vector2 targetPosi)
    {
        if (target == null) return;
        /*  Vector2 pushDir = -(targetPosi - (Vector2)transform.position);
          pushDir.y = 1;*/
        target.knockback(Vector2.up);
    }
}
