using UnityEngine;
using System.Collections;

public class SpawnerArea : MonoBehaviour
{
    public GameObject spawnerPrefab; // Pr�fabriqu� pour les spawners.
    public int spawnerCount = 4; // Nombre de spawners � cr�er.
    public Vector3 zoneSize = new Vector3(20, 0, 20); // Taille de la zone.

    public float spawnInterval = 4f; // Temps entre les spawns
    private int alreadySpawn = 0; // Compteur d'ennemis actuels
    void Start()
    {
        CreateSpawners();
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (alreadySpawn < spawnerCount)
        {
            CreateSpawners();
            yield return new WaitForSeconds(spawnInterval); // Temps entre les spawns

        }


    }

    void CreateSpawners()
    {
        // G�n�rer une position al�atoire dans la zone.
        Vector3 randomPosition = GetRandomPosition();

        // Instancier un spawner � cette position.
        Instantiate(spawnerPrefab, randomPosition, Quaternion.identity);
    }

    Vector3 GetRandomPosition()
    {
        // G�n�rer une position al�atoire dans les limites de la zone.
        float randomX = Random.Range(-zoneSize.x / 2, zoneSize.x / 2);
        float randomY = Random.Range(-zoneSize.y / 2, zoneSize.y / 2);
        float randomZ = Random.Range(-zoneSize.z / 2, zoneSize.z / 2);

        // Retourner la position relative au centre de la zone.
        return transform.position + new Vector3(randomX, randomY, randomZ);
    }

    void OnDrawGizmosSelected()
    {
        // Visualiser la zone dans l'�diteur.
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, zoneSize);
    }
}
