using System;
using System.Collections.Generic;
[Serializable]
public class DialogCompleteData
{
    public List<bool> datas = new List<bool>();
    public DialogCompleteData(int interactionCount)
    {
        for (int i = 0; i < interactionCount; i++)
        {
            datas.Add(false); // 초기화
        }
    }
    public DialogCompleteData(List<bool> interactionData)
    {
        datas = new List<bool>(interactionData);
    }
}

[Serializable]
public class PlayData
{
    public float playTime = 0f;

    public bool isStatusLoaded = false;
    public bool[] stageCleared = new bool[(int)Stage.Max];
    //public List<List<bool>> dialogComplete = new List<List<bool>>
    //{
    //    new List<bool>(), // Stage1
    //    new List<bool>(), // Stage2
    //    new List<bool>(), // Stage3
    //    new List<bool>(), // Stage4
    //    new List<bool>(), // BossStage
    //};
    public DialogCompleteData[] dialogComplete = new DialogCompleteData[(int)Stage.Max];
    public PlayerStatusInfo playerStatus = new PlayerStatusInfo();
    public PlayerSkillInfo playerSkill = new PlayerSkillInfo();

    public PlayData()
    {
        playTime = 0f;
        isStatusLoaded = false;
        for (int i = 0; i < (int)Stage.Max; i++)
        {
            stageCleared[i] = false; // 기본값 설정
            dialogComplete[i] = new DialogCompleteData(0); // 초기화
        }
        playerStatus = new PlayerStatusInfo(); // 기본 플레이어 상태 초기화
        playerSkill = new PlayerSkillInfo();
    }
    public void RecordData(PlayData data)
    {
        if (data == null) return; // null 체크
        playTime = data.playTime;
        if (data.stageCleared != null && data.stageCleared.Length == (int)Stage.Max)
        {
            Array.Copy(data.stageCleared, stageCleared, (int)Stage.Max);
        }
        for (int i = 0; i < (int)Stage.Max; i++)
        {
            if (data.dialogComplete[i] != null)
                dialogComplete[i] = new DialogCompleteData(data.dialogComplete[i].datas);
            else
                dialogComplete[i] = new DialogCompleteData(0); // 초기화
        }

        playerStatus = new PlayerStatusInfo(data.playerStatus);
        playerSkill = new PlayerSkillInfo(data.playerSkill); // 플레이어 스킬 정보 복사
        isStatusLoaded = true;
    }
}
[Serializable]
public class PlayDataCollection
{
    public List<PlayData> playDatas = new List<PlayData>();
}