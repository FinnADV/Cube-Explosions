using UnityEngine;
using UnityEngine.InputSystem;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _cubeLayer;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private Camera _mainCamera;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _cubeLayer))
            {
                if (hit.collider.TryGetComponent(out CubeData cubeData))
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

            Physics.SyncTransforms();
        }

        if (cubeObject.TryGetComponent(out Explosion explosion))
        {
            explosion.Explode();
        }

        Destroy(cubeObject);
    }
}