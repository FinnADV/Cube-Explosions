using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    [SerializeField] private float _spawnRange = 5f;
    [SerializeField] private float _spawnHeight = 0.5f;

    [SerializeField] private int _maxCube = 5;
    [SerializeField] private int _minCube = 2;
    [SerializeField] private int _scaleReduced = 2;

    public void SpawnCubes(GameObject originalCube, double currentChance, int currentLevel)
    {
        int numberOfCubes = Random.Range(_minCube, _maxCube + 1);

        Vector3 originalScale = originalCube.transform.localScale;
        Vector3 originalPosition = originalCube.transform.position;

        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 randomPosition = new Vector3(
                originalPosition.x + Random.Range(-_spawnRange, _spawnRange),
                originalPosition.y + _spawnHeight,
                originalPosition.z + Random.Range(-_spawnRange, _spawnRange)
            );

            GameObject newCube = Instantiate(_cubePrefab, randomPosition, Quaternion.identity);

            newCube.transform.localScale = originalScale / _scaleReduced;

            CubeData cubeData = newCube.GetComponent<CubeData>();

            if (cubeData != null)
            {
                double newChance = currentChance / _scaleReduced;

                int newLevel = currentLevel + 1;

                cubeData.SplitChance = newChance;
                cubeData.SplitLevel = newLevel;

                Debug.Log($"Создан куб уровня {newLevel} с шансом {newChance}");
            }

            Renderer renderer = newCube.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.material.color = new Color(Random.value, Random.value, Random.value);
            }
        }
    }
}