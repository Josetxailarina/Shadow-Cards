using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool movingCard = false;
    public static bool autoMove = false;
    public static bool menu = false;
    // Start is called before the first frame update

    public void RestartGame()
    {
        // Obtiene el nombre de la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;
        movingCard = false;
        autoMove = false;
        menu = false;
        // Recarga la escena actual
        SceneManager.LoadScene(currentSceneName);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
