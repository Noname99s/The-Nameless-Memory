using UnityEngine;

public enum GameState
{
    MainMenu,
    Home,
    Battle,
    Boss,
    Ending,
}
public enum Stage
{
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    BossStage,
    Max,
    NONE, // Used for no stage selected
}
public class GameManager 
{
    public bool[] stageCleared = new bool[(int)Stage.Max];
    //PlayerContext playerData;
    public GameState CurrentState { get; private set; } = GameState.MainMenu;
    public void Init()
    {
        //playerData = Managers.Player.GetPlayerData();
        //playerData.Initialize();
        for(int i = 0; i < stageCleared.Length; i++)
        {
            stageCleared[i] = false; 
        }
    }
    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;
        CurrentState = newState;
        Debug.Log($"Game state changed to: {CurrentState}");
        Managers.SceneLoad.LoadScene(newState.ToString());
        
    }
    public void SettingScene()
    {
        switch (CurrentState)
        {
            case GameState.MainMenu:
                // Logic for main menu
                break;
            case GameState.Home:
                // Logic for home
                //playerData.hp = playerData.maxHp; // Reset player HP when entering home
                break;

            case GameState.Battle:
                // Logic for battle
                Managers.UI.ShowPopupUI<InGameComposition>(); // Show loading UI
                Managers.Map.EntranceMap(); // Load the current map
                break;

            case GameState.Boss:
                // Logic for boss fight
                break;

            case GameState.Ending:
                // Logic for ending
                break;
        }
    }
}

