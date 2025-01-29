using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Recharge la sc�ne du jeu
        SceneManager.LoadScene("Game"); // Charge la sc�ne pr�c�dente
    }

    public void QuitGame()
    {
        Application.Quit(); // Quitte l'application
    }
    
}
