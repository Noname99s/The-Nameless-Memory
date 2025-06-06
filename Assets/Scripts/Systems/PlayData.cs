using System;
using System.Collections.Generic;

[Serializable]
public class PlayData
{
    public float playTime = 0f;
    public int deathCount = 0;
    public float enforceRatio = 0f;
    public PlayerStatusInfo playerStatus = new PlayerStatusInfo();
    public bool isStatusLoaded = false;

    public bool[] stageCleared = new bool[(int)Stage.Max];
    public int[] stage_Will_fragmentCount = new int[(int)Stage.Max];

    public PlayData()
    {
        playTime = 0f;
        deathCount = 0;
        enforceRatio = 0f;
        for (int i = 0; i < (int)Stage.Max; i++)
        {
            stageCleared[i] = false; // 기본값 설정
            stage_Will_fragmentCount[i] = 0; // 기본값 설정
        }
    }
    public PlayData(float playTime, int deathCount, float enforceRatio, bool[] stageCleared, int[] stage_Will_fragmentCount)
    {
        this.playTime = playTime;
        this.deathCount = deathCount;
        this.enforceRatio = enforceRatio;
        // 배열 복사 시 null 체크 및 길이 확인
        if (stageCleared != null && stageCleared.Length == (int)Stage.Max)
        {
            Array.Copy(stageCleared, this.stageCleared, (int)Stage.Max);
        }
        else
        {
            // 예외 처리 또는 기본값으로 초기화
            for (int i = 0; i < (int)Stage.Max; i++)
            {
                this.stageCleared[i] = false;
            }
        }

        if (stage_Will_fragmentCount != null && stage_Will_fragmentCount.Length == (int)Stage.Max)
        {
            Array.Copy(stage_Will_fragmentCount, this.stage_Will_fragmentCount, (int)Stage.Max);
        }
        else
        {
            for (int i = 0; i < (int)Stage.Max; i++)
            {
                this.stage_Will_fragmentCount[i] = 0;
            }
        }
    }

    public void RecordData(PlayData data)
    {
        if (data == null) return; // null 체크
        playTime = data.playTime;
        deathCount = data.deathCount;
        enforceRatio = data.enforceRatio;
        if (data.stageCleared != null && data.stageCleared.Length == (int)Stage.Max)
        {
            Array.Copy(data.stageCleared, stageCleared, (int)Stage.Max);
        }
        if (data.stage_Will_fragmentCount != null && data.stage_Will_fragmentCount.Length == (int)Stage.Max)
        {
            Array.Copy(data.stage_Will_fragmentCount, stage_Will_fragmentCount, (int)Stage.Max);
        }
    }
}
[Serializable]
public class PlayDataCollection
{
    public List<PlayData> playDatas = new List<PlayData>();
}