using UnityEngine;

public class UserSourceTest : MonoBehaviour
{

    //DB_TestPlayerData playerData, monsterData;
    private void Start()
    {
        //BG_DB_SaveLoad.Load();
    }
    private void OnGUI()
    {
        if (GUILayout.Button("Add"))
        {
            UpdataData(3);
        }
        if (GUILayout.Button("Sub"))
        {
            UpdataData(-3);
        }

    }
    public void UpdataData(int num)
    {
        var playerData = BG_DB_PlayerData.GetEntity(0);
        var monsterData = BG_DB_PlayerData.GetEntity(1);


        playerData._PlayerHP += num;
        playerData._PlayerMP += num * 2;

        monsterData._PlayerHP += num * 3;
        monsterData._PlayerMP += num * 4;


        var shortSword = BG_DB_Sword.GetEntity(0);
        var longSword = BG_DB_Sword.GetEntity(1);

        shortSword._Damage += num;
        shortSword._LV += num;

        longSword._Damage += num * 2;
        longSword._LV += num * 2;

        BG_DB_SaveLoad.Save();

    }

}
