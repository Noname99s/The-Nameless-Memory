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

        IDamageAble target = other.GetComponent<IDamageAble>();
        if (target == null) 
            return;
        Debug.Log($"Hit Attack to {other.name}");
        target.OnDamage(damage);
    }
}
