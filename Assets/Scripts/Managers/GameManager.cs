using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}
public class GameManager 
{
    public GameState CurrentState { get; private set; } = GameState.MainMenu;
    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;
        CurrentState = newState;
        Debug.Log($"Game state changed to: {CurrentState}");
        // Handle state-specific logic here
        switch (CurrentState)
        {
            case GameState.MainMenu:
                // Logic for main menu
                Managers.SceneLoad.LoadScene("MainMenuScene");
                break;
            case GameState.Playing:
                // Logic for playing
                Time.timeScale = 1;
                break;
            case GameState.Paused:
                // Logic for paused
                Time.timeScale = 0;
                break;
            case GameState.GameOver:
                // Logic for game over

                break;
        }
    }
    public void StartGame()
    {
        ChangeState(GameState.Playing);
        // Load the game scene or any other necessary setup
        Managers.SceneLoad.LoadScene("GameScene");
    }



}
