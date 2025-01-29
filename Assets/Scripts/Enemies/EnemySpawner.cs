using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject enemyPrefab; // Référence au prefab d'ennemi, pas à un objet de la scène

    public int maxEnemiesPerSpawn = 6;     // Nombre maximum d'ennemis
    private float spawnInterval = 1.8f; // Temps entre les spawns
    private int nbEnemies = 0; // Nombre d'ennemis actuels


    private GameManager gameManager; // Référence au GameManager

    private Animator animator; // Référence à l'Animator

    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        animator = GetComponent<Animator>();
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (nbEnemies < maxEnemiesPerSpawn && gameManager.CanSpawnEnemy())
        {
            CreateEnemy();
            yield return new WaitForSeconds(spawnInterval); // Temps entre les spawns
            animator.SetBool("ShouldClose", false);

        }
        animator.SetBool("ShouldClose", true);
        Destroy(gameObject);


    }

    void CreateEnemy()
    {

        // Créer une position aléatoire pour le futur ennemi
        Vector3 randomSpawnPosition = GetRandomPosition();

        // Instancier un nouvel ennemi basé sur le prefab
        Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);

        // Incrémenter le compteur d'ennemis
        nbEnemies++;
        GameManager.enemyCount++;
        Debug.Log("Nombre d'ennemis : " + GameManager.enemyCount);
    }

    Vector3 GetRandomPosition()
    {
        // Position centrale de la sphère
        Vector3 center = transform.position;

        // Rayon de la sphère
        float radius = GetComponent<SphereCollider>().radius * transform.localScale.x;

        // Générer une position aléatoire dans la sphère
        Vector3 randomPoint = Random.insideUnitSphere * radius;

        // Retourner la position finale
        return center + randomPoint;
    }
}
