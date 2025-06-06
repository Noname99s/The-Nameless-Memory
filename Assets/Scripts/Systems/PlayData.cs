using System;
using System.Collections.Generic;

[Serializable]
public class PlayData
{
    public float playTime = 0f;
    public bool isStatusLoaded = false;
    public bool[] stageCleared = new bool[(int)Stage.Max];
    public List<bool>[] dialogComlete = new List<bool>[(int)Stage.Max]
    {
        new List<bool>(), // Stage1
        new List<bool>(), // Stage2
        new List<bool>(), // Stage3
        new List<bool>(), // Stage4
        new List<bool>(), // BossStage
    };
    public PlayerStatusInfo playerStatus = new PlayerStatusInfo();

    public PlayData()
    {
        playTime = 0f;
        isStatusLoaded = false;
        for (int i = 0; i < (int)Stage.Max; i++)
        {
            stageCleared[i] = false; // 기본값 설정
            dialogComlete[i] = new List<bool>(); // 각 스테이지에 대한 대화 완료 리스트 초기화
        }
        playerStatus = new PlayerStatusInfo(); // 기본 플레이어 상태 초기화
    }
    public void RecordData(PlayData data)
    {
        if (data == null) return; // null 체크
        playTime = data.playTime;
        if (data.stageCleared != null && data.stageCleared.Length == (int)Stage.Max)
        {
            Array.Copy(data.stageCleared, stageCleared, (int)Stage.Max);
        }
        if (data.dialogComlete != null && data.dialogComlete.Length == (int)Stage.Max)
        {
            for (int i = 0; i < (int)Stage.Max; i++)
            {
                dialogComlete[i] = new List<bool>(data.dialogComlete[i]);
            }
        }
        playerStatus = new PlayerStatusInfo(data.playerStatus);
        isStatusLoaded = true;
    }
}
[Serializable]
public class PlayDataCollection
{
    public List<PlayData> playDatas = new List<PlayData>();
}