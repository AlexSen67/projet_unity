using UnityEngine;
using TMPro; // N'oubliez pas d'importer le namespace TextMesh Pro

public class Enemy : MonoBehaviour
{
    private Transform player; // Transform du joueur.
    private int life = 100; // Points de vie de l'ennemi.
    private GameManager gameManager; // R�f�rence au GameManager.

    private float speed; // Vitesse de d�placement de l'ennemi.
 



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
            Debug.LogError("Aucun objet avec le tag 'Player' trouv� dans la sc�ne.");
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
        // V�rifier si l'ennemi est mort
        if (life <= 0)
        {
            // D�truire l'ennemi
            Destroy(gameObject);
            gameManager.EnemyDied();
        }
    }
}
