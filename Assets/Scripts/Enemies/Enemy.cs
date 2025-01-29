using UnityEngine;
using TMPro;
using System.Collections; // N'oubliez pas d'importer le namespace TextMesh Pro

public class Enemy : MonoBehaviour
{
    private Transform player; // Transform du joueur.
    private int life = 300; // Points de vie de l'ennemi.
    private GameManager gameManager; // R�f�rence au GameManager.
    private float speed; // Vitesse de d�placement de l'ennemi.

    private Renderer enemyRenderer;
    private Color originalColor;
    private float damageFlashTime = 0.2f; // Temps avant de revenir � la couleur normale

    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();

       
        Transform polySurface1 = transform.GetChild(0); // r�cup�re le MeshRenderer de l'ennemi dans la hierarchie

        // Rechercher automatiquement le joueur par son tag "Player"
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Aucun objet avec le tag 'Player' trouv� dans la sc�ne.");
        }

        // R�cup�rer le Renderer de l'ennemi et stocker sa couleur de base
        GameObject thisObject = transform.GetChild(0).gameObject;
        enemyRenderer = polySurface1.gameObject.GetComponent<Renderer>(); ;
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
    }

    void Update()
    {
        // Met � jour la vitesse en r�cup�rant la vitesse actuelle du GameManager
        speed = gameManager.GetEnemySpeed();

        // V�rifie si le joueur est assign�
        if (player != null)
        {
            // Calculer la direction vers le joueur
            Vector3 direction = (player.position - transform.position).normalized;

            // D�placement vers le joueur
            transform.position += direction * speed * Time.deltaTime;

            // Faire face au joueur
            direction.y = 0; // Ne pas incliner verticalement
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void TakeDamage(int damage)
    {
        // R�duire les points de vie de l'ennemi
        life -= damage;

        // Activer l'effet rouge
        if (enemyRenderer != null)
        {
            StopAllCoroutines(); // Arr�te les changements de couleur en cours pour �viter les bugs
            StartCoroutine(FlashRed());
        }

        // V�rifier si l'ennemi est mort
        if (life <= 0)
        {
            Destroy(gameObject);
            gameManager.gainXP();
            gameManager.EnemyDied();
        }
    }

    private IEnumerator FlashRed()
    {
       
        enemyRenderer.material.color = Color.red; // Change en rouge
        yield return new WaitForSeconds(damageFlashTime); // Attend un instant
        enemyRenderer.material.color = originalColor; // Revient � la couleur d'origine
    }
}
