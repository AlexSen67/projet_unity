using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Recharge la scène du jeu
        SceneManager.LoadScene("Game"); // Charge la scène précédente
    }

    public void QuitGame()
    {
        Application.Quit(); // Quitte l'application
    }
    
}
