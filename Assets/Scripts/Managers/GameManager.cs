using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Play,
    AutoMove
}
public class GameManager : MonoBehaviour
{
    public static GameState gameState = GameState.Menu;
    public static bool isDraggingCard = false;
    // Start is called before the first frame update

    public void RestartGame()
    {
        gameState = GameState.Play;
        isDraggingCard = false;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
