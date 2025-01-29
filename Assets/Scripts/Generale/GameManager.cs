using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public TMP_Text timerText; // R�f�rence � un Text UI pour afficher le timer
    [SerializeField]
    public TMP_Text speedEnemies; // R�f�rence � un Text UI pour afficher la vitesse des ennemis


    public static int enemyCount = 0; // Nombre d'ennemis dans la sc�ne
    public static int maxEnemies = 35; // Nombre maximum d'ennemis

    private float timer = 0f; // Temps �coul� en secondes
    private bool isGameActive = true; // Contr�le si le timer doit continuer ou s'arr�ter

    private float enemySpeed = 11f; // Vitesse de l'ennemi
    public float speedIncreaseRate = 0.30f; // Facteur d'augmentation de la vitesse
    private float timeElapsedForSpeedIncrease = 0f; // Temps �coul� pour l'augmentation de la vitesse

    void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime; // Incr�mente le temps �coul�
            timeElapsedForSpeedIncrease += Time.deltaTime;

            // V�rifie si 1 minute est pass�e pour augmenter la vitesse
            if (timeElapsedForSpeedIncrease >= 30f)
            {
                IncreaseEnemySpeed();
                timeElapsedForSpeedIncrease = 0f; // R�initialiser le timer d'augmentation de la vitesse
            }

            UpdateTimerDisplay();
            UpdateSpeedText();
        }
    }

    void UpdateSpeedText()
    {
        if (speedEnemies != null)
        {
            speedEnemies.text = "Vitesse ennemie : " + enemySpeed.ToString("F1"); // Affiche la vitesse avec une d�cimale
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            // Formater le temps �coul� en minutes et secondes
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = $"Timer: {minutes:00}:{seconds:00}";
        }
    }

    // M�thode pour d�marrer ou red�marrer le timer
    public void StartTimer()
    {
        isGameActive = true;
    }

    public void EnemyDied()
    {
        enemyCount--; // Diminue le compteur quand un ennemi meurt
    }

    public bool CanSpawnEnemy()
    {
        return enemyCount < maxEnemies; // V�rifie si on peut cr�er un nouvel ennemi
    }

    // Augmenter la vitesse de l'ennemi en fonction du temps �coul�
    private void IncreaseEnemySpeed()
    {
        enemySpeed += speedIncreaseRate; // Augmente la vitesse
        speedEnemies.text = $"Speed: {enemySpeed:0.0}"; // Met � jour l'affichage de la vitesse
        Debug.Log("Vitesse des ennemis augment�e � " + enemySpeed);
    }

    // Getter pour r�cup�rer la vitesse de l'ennemi
    public float GetEnemySpeed()
    {
        return enemySpeed;
    }
}
