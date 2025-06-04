using Orpul1;
using Orpul1.Boss.Mirrored;
using Orpul1.Camera;
using Orpul1.Object;
using SOBehaviorTree;
using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour,IFindTreeAble, IDamageAble
{
    [SerializeField] BehaviorTree tree;
    [SerializeField] MirroredBlackboard mirroredBlackboard;
    [SerializeField] MirroredBlackboardComponent mirroredBlackboardComponent;
    [SerializeField] GameObject skillEffectSpawner;
    MirroredBossInfo bossInfo;
    MirroredHitStateInfo hitInfo;
    WaitForSeconds blinkDelay;
    ISetCamerGroupAble cameraGroup;
    int curblinkCount = 0;
    public BehaviorTree GetBehaviorTree()
    {
        return tree;
    }
    private void Awake()
    {
        bossInfo = mirroredBlackboardComponent.bossInfo;
        hitInfo = mirroredBlackboardComponent.hitStateInfo;
        mirroredBlackboard.btComponent = mirroredBlackboardComponent;
        blinkDelay = new WaitForSeconds(mirroredBlackboard.btComponent.hitStateInfo.blinkeDelay);
    }
    void Start()
    {
        Transform camera = Managers.Camera.GetCamera<Transform>((int)ECameraType.MainCamera);
        mirroredBlackboardComponent.bossSkillInfo.cameraEffect = camera.GetComponent<ICameraEffectAble>();
        cameraGroup = camera.GetComponent<ISetCamerGroupAble>();

        tree.root.Init();
    }
    private void OnDisable()
    {
        //cameraGroup.RemoveGroup(transform);
    }

    void Update()
    {
        tree.root.Process();
    }

    public void OnDamage(int damage)
    {
        mirroredBlackboard.btComponent.bossInfo.hp -= damage;
        StartCoroutine("OnDamageCoroutine");
    }
    IEnumerator OnDamageCoroutine()
    {
        curblinkCount = 0;
        while (!IsFillHitCount())
        {
            yield return BlinkSpriteColorInHitState();
        }
    }
    IEnumerator BlinkSpriteColorInHitState()
    {
        bossInfo.bossSprite.color = hitInfo.hitColor;
        yield return blinkDelay;

        bossInfo.bossSprite.color = hitInfo.orginColor;
        yield return blinkDelay;
        curblinkCount++;
    }
    private bool IsFillHitCount()
    {
        if (curblinkCount < hitInfo.blinkCount)
            return false;
        return true;
    }
}
