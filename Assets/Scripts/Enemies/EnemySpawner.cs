using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject enemyPrefab; // R�f�rence au prefab d'ennemi, pas � un objet de la sc�ne

    public int maxEnemiesPerSpawn = 6;     // Nombre maximum d'ennemis
    private float spawnInterval = 1.8f; // Temps entre les spawns
    private int nbEnemies = 0; // Nombre d'ennemis actuels


    private GameManager gameManager; // R�f�rence au GameManager

    private Animator animator; // R�f�rence � l'Animator

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

        // Cr�er une position al�atoire pour le futur ennemi
        Vector3 randomSpawnPosition = GetRandomPosition();

        // Instancier un nouvel ennemi bas� sur le prefab
        Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);

        // Incr�menter le compteur d'ennemis
        nbEnemies++;
        GameManager.enemyCount++;
        Debug.Log("Nombre d'ennemis : " + GameManager.enemyCount);
    }

    Vector3 GetRandomPosition()
    {
        // Position centrale de la sph�re
        Vector3 center = transform.position;

        // Rayon de la sph�re
        float radius = GetComponent<SphereCollider>().radius * transform.localScale.x;

        // G�n�rer une position al�atoire dans la sph�re
        Vector3 randomPoint = Random.insideUnitSphere * radius;

        // Retourner la position finale
        return center + randomPoint;
    }
}
