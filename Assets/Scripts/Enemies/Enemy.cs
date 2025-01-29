using UnityEngine;
using TMPro;
using System.Collections; // N'oubliez pas d'importer le namespace TextMesh Pro

public class Enemy : MonoBehaviour
{
    private Transform player; // Transform du joueur.
    private int life = 300; // Points de vie de l'ennemi.
    private GameManager gameManager; // Référence au GameManager.
    private float speed; // Vitesse de déplacement de l'ennemi.

    private Renderer enemyRenderer;
    private Color originalColor;
    private float damageFlashTime = 0.2f; // Temps avant de revenir à la couleur normale

    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();

       
        Transform polySurface1 = transform.GetChild(0); // récupère le MeshRenderer de l'ennemi dans la hierarchie

        // Rechercher automatiquement le joueur par son tag "Player"
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Aucun objet avec le tag 'Player' trouvé dans la scène.");
        }

        // Récupérer le Renderer de l'ennemi et stocker sa couleur de base
        GameObject thisObject = transform.GetChild(0).gameObject;
        enemyRenderer = polySurface1.gameObject.GetComponent<Renderer>(); ;
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
    }

    void Update()
    {
        // Met à jour la vitesse en récupérant la vitesse actuelle du GameManager
        speed = gameManager.GetEnemySpeed();

        // Vérifie si le joueur est assigné
        if (player != null)
        {
            // Calculer la direction vers le joueur
            Vector3 direction = (player.position - transform.position).normalized;

            // Déplacement vers le joueur
            transform.position += direction * speed * Time.deltaTime;

            // Faire face au joueur
            direction.y = 0; // Ne pas incliner verticalement
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void TakeDamage(int damage)
    {
        // Réduire les points de vie de l'ennemi
        life -= damage;

        // Activer l'effet rouge
        if (enemyRenderer != null)
        {
            StopAllCoroutines(); // Arrête les changements de couleur en cours pour éviter les bugs
            StartCoroutine(FlashRed());
        }

        // Vérifier si l'ennemi est mort
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
        enemyRenderer.material.color = originalColor; // Revient à la couleur d'origine
    }
}
