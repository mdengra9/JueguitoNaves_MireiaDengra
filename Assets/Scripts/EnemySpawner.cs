using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Prefab que quieres spawnear
    public Transform spawnPoint; // Punto de spawn, usualmente una empty

    public int numberOfPrefabsToSpawn = 1; // Cantidad de prefabs a spawnear
    public float moveSpeed = 5f; // Velocidad de movimiento en el eje -z
    public float spawnRange = 5f; // Rango de spawn

    void Start()
    {
        // Loop para spawnear la cantidad especificada de prefabs
        for (int i = 0; i < numberOfPrefabsToSpawn; i++)
        {
            // Calcular una posición aleatoria dentro del rango de spawn
            Vector3 randomSpawnPos = spawnPoint.position + Random.insideUnitSphere * spawnRange;
            randomSpawnPos.y = spawnPoint.position.y; // Mantener la misma altura de spawn

            // Spawnear el prefab en la posición aleatoria calculada
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, randomSpawnPos, Quaternion.identity);

            // Obtener el componente Rigidbody del prefab si existe
            Rigidbody rb = spawnedPrefab.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Mover el prefab en el eje -z con la velocidad especificada
                rb.velocity = new Vector3(0f, 0f, -moveSpeed);
            }
            else
            {
                Debug.LogWarning("El prefab no tiene Rigidbody adjunto. No se puede mover.");
            }
        }
    }
}
