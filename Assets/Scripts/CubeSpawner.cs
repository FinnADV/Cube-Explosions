using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    [SerializeField] private int _maxCube = 5;
    [SerializeField] private int _minCube = 2;
    [SerializeField] private int _scaleReduced = 2;

     private Painting _painter = new Painting();

    public void SpawnCubes(GameObject originalCube, double currentChance, int currentLevel)
    {
        int numberOfCubes = Random.Range(_minCube, _maxCube + 1);

        Vector3 originalScale = originalCube.transform.localScale;
        Vector3 originalPosition = originalCube.transform.position;

        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 randomPosition = originalPosition;

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

            _painter.SetRandomColor(newCube);
        }
    }
}