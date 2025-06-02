using UnityEngine;
using UnityEngine.SceneManagement;

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
    Hub, // Used for no stage selected
}
public class GameManager 
{
    public bool[] stageCleared = new bool[(int)Stage.Max];
    public GameState CurrentState { get; private set; } = GameState.MainMenu;
    public void Init()
    {
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
        Managers.Player.MoveStage(false); // Move player to the new state
        Managers.SceneLoad.LoadScene(newState.ToString());
        
    }
    public void SettingScene()
    {
        switch (CurrentState)
        {
            case GameState.MainMenu:
                // Logic for main menu
                Managers.Player.RemovePlayer(); // Remove player if exists
                break;
            case GameState.Home:
                // Logic for home
                Managers.UI.ShowPopupUI<InGameComposition>();
                Managers.Player.Init(); // Initialize player
                Managers.Player.ChangePlayerPos(true); // Set player position to home
                Managers.Player.MoveStage(true); // Activate player and camera
                break;

            case GameState.Battle:
                // Logic for battle
                Managers.UI.ShowPopupUI<InGameComposition>(); 
                Managers.Map.EntranceMap(); // Load the current map
                Managers.Player.ChangePlayerPos(false); // Set player position to the current map
                Managers.Player.MoveStage(true); // Activate player and camera
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

