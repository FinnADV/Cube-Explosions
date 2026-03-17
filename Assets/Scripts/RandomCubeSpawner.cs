using UnityEngine;
using UnityEngine.InputSystem;

public class RandomCubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    [SerializeField] private float _spawnRange = 5f;
    [SerializeField] private float _spawnHeight = 0.5f;

    [SerializeField] private int _maxCube = 5;
    [SerializeField] private int _minCube = 2;
    [SerializeField] private int _scaleReduced = 2;

    private double _currentChance = 1.0;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    HandleClick();
                    Destroy(gameObject);
                }
            }
        }
    }

    private void SpawnCubes()
    {
        int numberOfCubes = Random.Range(_minCube, _maxCube + 1);

        Vector3 originalScale = transform.localScale;

        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 randomPosition = new Vector3(
                transform.position.x + Random.Range(-_spawnRange, _spawnRange),
                transform.position.y + _spawnHeight,
                transform.position.z + Random.Range(-_spawnRange, _spawnRange)
                );

            GameObject newCube = Instantiate(_cubePrefab, randomPosition, Quaternion.identity);

            newCube.transform.localScale = originalScale / _scaleReduced;

            RandomCubeSpawner newSpawner = newCube.GetComponent<RandomCubeSpawner>();

            if (newSpawner != null)
            {
                newSpawner.SetChance(_currentChance / _scaleReduced);
            }

            Renderer cubeRenderer = newCube.GetComponent<Renderer>();

            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = new Color(Random.value, Random.value, Random.value);
            }
        }
    }

    private void SetChance(double chance)
    {
        _currentChance = chance;
    }

    private void HandleClick()
    {
        double randomValue = Random.value;

        if (randomValue <= _currentChance)
        {
            SpawnCubes();

            _currentChance /= _scaleReduced;

            Explode();
        }
        else
        { 
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Explosion explosion = GetComponent<Explosion>();

        if (explosion != null)
        {
            explosion.Explode();
        }
        else
        {
            Debug.LogWarning("Компонент Explosion не найден на объекте!");
        }
    }
}