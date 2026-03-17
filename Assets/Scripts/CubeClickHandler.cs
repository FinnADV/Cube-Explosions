using UnityEngine;
using UnityEngine.InputSystem;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _cubeLayer;

    [SerializeField] private CubeSpawner _cubeSpawner;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;

        if (_cubeSpawner == null)
        {
            _cubeSpawner = FindAnyObjectByType<CubeSpawner>();
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _cubeLayer))
            {
                CubeData cubeData = hit.transform.GetComponent<CubeData>();

                if (cubeData != null)
                {
                    HandleCubeClick(cubeData, hit.transform.gameObject);
                }
            }
        }
    }

    private void HandleCubeClick(CubeData cubeData, GameObject cubeObject)
    {
        float randomValue = Random.value;

        if (randomValue <= cubeData.SplitChance)
        {
            _cubeSpawner.SpawnCubes(cubeObject, cubeData.SplitChance, cubeData.SplitLevel);

        }

        Destroy(cubeObject);
    }
}