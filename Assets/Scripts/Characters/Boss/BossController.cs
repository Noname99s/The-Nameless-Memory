using DG.Tweening;
using Orpul1.Camera;
using SOBehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour,IFindTreeAble, IDamageAble
{
    [SerializeField] BehaviorTree tree;
    [SerializeField] MirroredBlackboard mirroredBlackboard;
    [SerializeField] MirroredBlackboardComponent mirroredBlackboardComponent;
    [SerializeField] MirroredDB blackboardDB;
    [SerializeField] GameObject skillEffectSpawner;
    [SerializeField] Slider bossHpBar;
    MirroredBossInfo bossInfo;
    MirroredHitStateInfo hitInfo;
    WaitForSeconds blinkDelay;
    ISetCamerGroupAble cameraGroup;
    int curblinkCount = 0;

    [SerializeField] SO_StaticDialogData bossIntroDialog;
    [SerializeField] SO_DynamicDialogData bossDialogData;
    [SerializeField] SO_DynamicDialogData bossoutroDialogData;
    public event System.Action<List<string>> bossDialogAnim;
    public BehaviorTree GetBehaviorTree()
    {
        return tree;
    }
    private void Awake()
    {
        bossIntroDialog.Init();
        bossDialogData.Init();
        bossoutroDialogData.Init();
        bossDialogAnim += Managers.UI.GetPopupUI<InGameComposition>().OnBossDialogAnimConnection;
        bossIntroDialog.TransferDialogToUI();
        SetBlackboardDB();
        bossInfo = mirroredBlackboardComponent.bossInfo;
        hitInfo = mirroredBlackboardComponent.hitStateInfo;
        mirroredBlackboard.btComponent = mirroredBlackboardComponent;
        blinkDelay = new WaitForSeconds(mirroredBlackboard.btComponent.hitStateInfo.blinkeDelay);
    }
    public void BossOutro(Action action)
    {
        bossDialogAnim?.Invoke(bossoutroDialogData.dialogTexts);
        StartCoroutine(BossOutroCoroutine(action));
    }
    IEnumerator BossOutroCoroutine(Action action)
    {
        yield return new WaitForSeconds(18f);
        action?.Invoke();
    }

    private void SetBlackboardDB()
    {
        blackboardDB.SetDB(mirroredBlackboardComponent);
    }
    void Start()
    {
        bossDialogAnim?.Invoke(bossDialogData.dialogTexts);

        Transform camera = Managers.Camera.GetCamera<Transform>((int)ECameraType.MainCamera);
        mirroredBlackboardComponent.bossSkillInfo.cameraEffect = camera.GetComponent<ICameraEffectAble>();
        cameraGroup = camera.GetComponent<ISetCamerGroupAble>();

        tree.root.Init();
    }
    private void OnDisable()
    {
        //cameraGroup.RemoveGroup(transform);
        bossIntroDialog.ClearAction();
        bossDialogData.ClearAction();
        bossoutroDialogData.ClearAction();
        bossDialogAnim -= Managers.UI.GetPopupUI<InGameComposition>().OnBossDialogAnimConnection;
    }

    void Update()
    {
        tree.root.Process();
    }

    public void OnDamage(int damage)
    {
        if (mirroredBlackboardComponent.bossInfo.hp <= 0) return;
        mirroredBlackboard.btComponent.bossInfo.hp -= damage;
        SetHpUI();
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
    public void SetHpUI()
    {
        bossHpBar.value = (float)mirroredBlackboardComponent.bossInfo.hp / mirroredBlackboardComponent.bossInfo.maxHp;
    }
}
