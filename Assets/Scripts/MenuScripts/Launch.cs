using UnityEngine;
using UnityEngine.SceneManagement;

public class Launch : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

}
