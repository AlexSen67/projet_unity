using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public TMP_Text timerText; // Référence à un Text UI pour afficher le timer
    [SerializeField]
    public TMP_Text speedEnemies; // Référence à un Text UI pour afficher la vitesse des ennemis
    [SerializeField]
    public TMP_Text xpText; // Référence à un Text UI pour afficher la vitesse des ennemis

    public static int xp = 0;


    public static int enemyCount = 0; // Nombre d'ennemis dans la scène
    //public static int maxEnemies = 35; // Nombre maximum d'ennemis

    private float timer = 0f; // Temps écoulé en secondes
    private bool isGameActive = true; // Contrôle si le timer doit continuer ou s'arrêter

    private float enemySpeed = 13f; // Vitesse de l'ennemi
    private float playerSpeed = 12f; // Vitesse de l'ennemi
    public float speedIncreaseRate = 1.6f; // Facteur d'augmentation de la vitesse
    private float timeElapsedForSpeedIncrease = 0f; // Temps écoulé pour l'augmentation de la vitesse

    void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime; // Incrémente le temps écoulé
            timeElapsedForSpeedIncrease += Time.deltaTime;

            // Vérifie si 20 secondes est passée pour augmenter la vitesse
            if (timeElapsedForSpeedIncrease >= 20f)
            {
                IncreaseEnemySpeed();
                playerSpeed += speedIncreaseRate;
                timeElapsedForSpeedIncrease = 0f; // Réinitialiser le timer d'augmentation de la vitesse
            }

            UpdateTimerDisplay();
            UpdateSpeedText();
            displayXP();
        }
    }

    void UpdateSpeedText()
    {
        if (speedEnemies != null)
        {
            speedEnemies.text = "Vitesse ennemie : " + enemySpeed.ToString("F1"); // Affiche la vitesse avec une décimale
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            // Formater le temps écoulé en minutes et secondes
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = $"Timer: {minutes:00}:{seconds:00}";
        }
    }

    // Méthode pour démarrer ou redémarrer le timer
    public void StartTimer()
    {
        isGameActive = true;
    }

    public void EnemyDied()
    {
        enemyCount--; // Diminue le compteur quand un ennemi meurt
    }

    public void gainXP()
    {
        xp += 100;
    }

    //public bool CanSpawnEnemy()
    //{
    //    return enemyCount < maxEnemies; // Vérifie si on peut créer un nouvel ennemi
    //}

    // Augmenter la vitesse de l'ennemi en fonction du temps écoulé
    private void IncreaseEnemySpeed()
    {
        enemySpeed += speedIncreaseRate; // Augmente la vitesse
        speedEnemies.text = $"Speed: {enemySpeed:0.0}"; // Met à jour l'affichage de la vitesse
        Debug.Log("Vitesse des ennemis augmentée à " + enemySpeed);
    }

    private void displayXP()
    {
        if (xpText != null)
        {
            xpText.text = "XP : " + xp.ToString();
        }
    }

    // Getter pour récupérer la vitesse de l'ennemi
    public float GetEnemySpeed()
    {
        return enemySpeed;
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }
}
