using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GameDataManager
{
    string fileName = "GameData.json";

    public string GetFilePath()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        return path;
    }
    public void SaveGameDataCollection(List<PlayData> playDatasToSave)
    {
        // 래퍼 객체 생성
        PlayDataCollection collection = new PlayDataCollection();
        collection.playDatas = playDatasToSave;

        // 래퍼 객체를 직렬화
        string json = JsonUtility.ToJson(collection, true); // 'true'는 JSON을 읽기 좋게 정렬합니다.
        string filePath = GetFilePath();

        try
        {
            File.WriteAllText(filePath, json);
            Debug.Log($"성공적으로 {playDatasToSave.Count}개의 PlayData 항목을 다음 경로에 저장했습니다: {filePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"게임 데이터 컬렉션 저장 실패: {e.Message}");
        }
    }

    public List<PlayData> LoadGameDataCollection()
    {
        string filePath = GetFilePath();

        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                PlayDataCollection collection = JsonUtility.FromJson<PlayDataCollection>(json);

                if (collection != null && collection.playDatas != null)
                {
                    Debug.Log($"성공적으로 {collection.playDatas.Count}개의 PlayData 항목을 다음 경로에서 불러왔습니다: {filePath}");
                    return collection.playDatas;
                }
                else
                {
                    Debug.LogWarning("불러온 JSON이 비어있거나 형식이 잘못되었습니다. 빈 목록을 반환합니다.");
                    return new List<PlayData>();
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"게임 데이터 컬렉션 로드 실패: {e.Message}. 빈 목록을 반환합니다.");
                return new List<PlayData>();
            }
        }
        else
        {
            Debug.LogWarning("게임 데이터 컬렉션 파일이 없습니다. 빈 목록을 반환합니다.");
            return new List<PlayData>(); // 파일이 없으면 빈 목록 반환
        }
    }
    // 새로운 데이터를 추가하거나, 크기 이내의 인덱스면 해당 슬롯에 데이터를 업데이트합니다.
    // 데이터 크기는 3개로 제한
    public void AddOrUpdatePlayData(PlayData dataToAdd, int slotIndex = -1)
    {
        if(slotIndex < -1 || slotIndex > 2)
        {
            Debug.LogError("슬롯 인덱스는 -1에서 4 사이여야 합니다.");
            return;
        }
        List<PlayData> currentData = LoadGameDataCollection();

        // slotIndex가 더 큰 경우 1개 추가
        if (slotIndex == -1 || slotIndex >= currentData.Count)
        {
            currentData.Add(dataToAdd); // 슬롯이 비어있으면 추가
        }
        else
        {
            currentData[slotIndex] = dataToAdd; // 지정된 슬롯에 데이터 업데이트
        }
        SaveGameDataCollection(currentData); // 업데이트된 컬렉션 저장
    }
    // 지정된 인덱스의 PlayData를 반환합니다. 인덱스가 범위를 벗어나면 null을 반환합니다.
    public PlayData GetPlayData(int index)
    {
        List<PlayData> currentData = LoadGameDataCollection();
        if (index >= 0 && index < currentData.Count)
        {
            return currentData[index];
        }
        Debug.LogWarning($"인덱스 {index}의 PlayData를 찾을 수 없거나 범위를 벗어났습니다.");
        return null;
    }

}
