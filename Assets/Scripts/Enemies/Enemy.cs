using UnityEngine;
using TMPro; // N'oubliez pas d'importer le namespace TextMesh Pro

public class Enemy : MonoBehaviour
{
    private Transform player; // Transform du joueur.
    private int life = 100; // Points de vie de l'ennemi.
    private GameManager gameManager; // Référence au GameManager.

    private float speed; // Vitesse de déplacement de l'ennemi.
 



    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
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
        // Vérifier si l'ennemi est mort
        if (life <= 0)
        {
            // Détruire l'ennemi
            Destroy(gameObject);
            gameManager.EnemyDied();
        }
    }
}
