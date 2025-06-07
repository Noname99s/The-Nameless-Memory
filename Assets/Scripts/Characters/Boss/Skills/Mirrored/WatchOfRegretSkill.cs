using System;
using System.Collections;
using Orpul1;
using Orpul1.Boss.Sound;
using Orpul1.Object;
using UnityEngine;

public class WatchOfRegretSkill : BossSkill,ISetDataAble<Vector2>
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Vector2 arrivePosi;
    [SerializeField] float posiGap = 0.1f;
    [SerializeField] float moveSpeed = 1;
    bool isExecute = false;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (!isExecute) return;
        MoveArrivePosi();
        if (IsArrivePosi()) return;
        enqueueAction(gameObject);
    }
    public override void Execute()
    {
        base.Execute();
        sound.PlaySound((int)EBossSkillSound.Skill2_Projectile);
        isExecute = true;
    }

    public void SetValue(Vector2 arrivePosi)
    {
        this.arrivePosi = arrivePosi;
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
        IDamageAble player = other.GetComponent<IDamageAble>();
        if (player == null) return;
        player.OnDamage(damage);
    }
    private void OnDisable()
    {
        ResetValue();
    }

    public override void SetValue(Action<GameObject> enqueueAction)
    {
        this.enqueueAction = enqueueAction;
    }

    public override void ResetValue()
    {
        isExecute = false;
    }
}
