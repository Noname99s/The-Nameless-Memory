using System.Collections;
using Orpul1;
using Orpul1.Object;
using UnityEngine;

public class WatchOfRegretSkill : BossSkill,ISetDataAble
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Vector2 arrivePosi;
    [SerializeField] int damage = 0;
    [SerializeField] float posiGap = 0.1f;
    [SerializeField] float moveSpeed = 1;
    bool isExecute = false;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
    }
    private void FixedUpdate()
    {
        if (!isExecute) return;
        MoveArrivePosi();
        if (IsArrivePosi()) return;
        ObjectPoolManager.Instance.EnqueueObj((int)EObjectType.EnergyBall, gameObject);
    }
    public override void Execute()
    {
        isExecute = true;
    }

    public void SetValue<T>(T t)
    {
        if (!(t is Vector2 posi)) return;

        arrivePosi = posi;
    }
    
    private void MoveArrivePosi()
    {
        Vector2 dir = arrivePosi - (Vector2)transform.position;
        rigid.linearVelocity = dir.normalized * moveSpeed * Time.deltaTime;
    }
    private bool IsArrivePosi()
    {
        if (Vector2.Distance(transform.position, arrivePosi) < posiGap) return false;
        return true;
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(ETag.Player.ToString())) return;
        IDamageAble player = other as IDamageAble;
        if (player == null) return;
        player.OnDamage(damage);
    }
    IEnumerator DelayDeactive()
    {
        yield return null;
    }
    private void OnDisable()
    {
        isExecute = false;
    }
}
