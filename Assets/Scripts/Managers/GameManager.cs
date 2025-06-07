using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Home,
    Battle,
    RemindRoom,
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
    public PlayData playData; // Game data
    public GameState CurrentState { get; private set; } = GameState.MainMenu;
    public void Init()
    {
        playData = new PlayData(); // Initialize play data
    }
    public void UpdatePlayTime()
    {
        if (CurrentState == GameState.MainMenu || CurrentState == GameState.Ending) return;
        playData.playTime += Time.deltaTime; 
    }
    public void StartStage(Stage stage, int interactionCount)
    {
        while (playData.dialogComplete[(int)stage].datas.Count < interactionCount)
        {
            playData.dialogComplete[(int)stage].datas.Add(false);
        }
    }
    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Time.timeScale = 1f; 
        Debug.Log($"Game state changed to: {CurrentState}");
        // Listener ON
        Managers.Sound.ActiveListner();
        Managers.Player.RemovePlayer(); 
        Managers.SceneLoad.LoadScene(newState.ToString());
    }
    public void SettingScene()
    {
        Managers.Sound.DeactiveListner();

        switch (CurrentState)
        {
            case GameState.MainMenu:
                // Logic for main menu
                Managers.UI.GetPopupUI<SaveLoadComposition>().RecordData(playData); // Record current play data
                Managers.Player.RemovePlayer(); // Remove player if exists
                break;
            case GameState.Home:
                // Logic for home
                Managers.UI.ShowPopupUI<InGameComposition>();
                Managers.Player.Init(new Vector3(9, 8, 0)); // Initialize player
                break;
            case GameState.RemindRoom:
                break;

            case GameState.Battle:
                // Logic for battle
                Managers.UI.ShowPopupUI<InGameComposition>(); 
                Managers.Map.TransitionMap(); // Load the current map
                Managers.Player.Init(Managers.Map.movePoint); // Initialize player at the entrance point
                break;

            case GameState.Boss:
                // Logic for boss fight
                Managers.UI.ShowPopupUI<InGameComposition>();
                Managers.Player.Init(new Vector3(10, 10, 0)); // Initialize player
                break;

            case GameState.Ending:
                // Logic for ending
                break;
        }
    }
}

