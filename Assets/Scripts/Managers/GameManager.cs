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
    public void PlayNewGame()
    {
        playData.playTime = 0f; // Reset play time
        playData.isStatusLoaded = false; // Reset status loaded flag
        for (int i = 0; i < (int)Stage.Max; i++)
        {
            playData.stageCleared[i] = false; // Reset stage cleared status
            playData.dialogComplete[i].datas.Clear(); // Clear dialog completion data
        }
        playData.playerSkill = new PlayerSkillInfo(); // Reset player skills
        Managers.Map.Init(); // Initialize map manager
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
        Managers.Sound.DeactiveListner();
        Managers.Player.RemovePlayer(); 
        Managers.SceneLoad.LoadScene(newState.ToString());
    }
    public void SettingScene()
    {
        int bgmType = (int)CurrentState;
        
        switch (CurrentState)
        {
            case GameState.MainMenu:
                Managers.Sound.ActiveListner();
                // Logic for main menu
                Managers.UI.GetPopupUI<SaveLoadComposition>().RecordData(playData); // Record current play data
                Managers.UI.ClosePopupUI<SaveLoadComposition>(); // Close save/load UI
                Managers.Player.RemovePlayer(); // Remove player if exists
                bgmType = (int)ESceneType.MainMenu;
                break;
            case GameState.Home:
                // Logic for home
                bgmType = (int)ESceneType.Home;
                Managers.UI.ShowPopupUI<InGameComposition>();
                Managers.Player.Init(new Vector3(9, 8, 0)); // Initialize player
                break;
            case GameState.RemindRoom:
                bgmType = (int)ESceneType.RemindRoom;
                break;

            case GameState.Battle:
                // Logic for battle
                Managers.UI.ShowPopupUI<InGameComposition>(); 
                Managers.Map.TransitionMap(); // Load the current map
                Managers.Player.Init(Managers.Map.movePoint); // Initialize player at the entrance point
                bgmType = (int)ESceneType.Battle + (int)Managers.Map.stageType;
                break;

            case GameState.Boss:
                // Logic for boss fight
                Managers.UI.ShowPopupUI<InGameComposition>();
                Managers.Player.Init(new Vector3(10, 10, 0)); // Initialize player
                bgmType = (int)ESceneType.Boss;
                break;

            case GameState.Ending:
                Managers.Sound.ActiveListner();
                // Logic for ending
                break;
        }
        Managers.Sound.PlayBGM((ESceneType)bgmType);
    }
}

