using UnityEngine;

public class InvisibleBounds : MonoBehaviour
{
    [SerializeField] private Vector3 _size = new Vector3(10f, 5f, 10f);

    [SerializeField] private float _thickness = 0.5f;

    [SerializeField] private bool _clickThroughWalls = true;

    private const float _bounciness = 0.5f;

    private const string _wallMaterialName = "WallMaterial";
    private const string _ignoreRaycastLayer = "Ignore Raycast";

    private void Start()
    {
        CreateBox();
    }

    private void CreateBox()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        PhysicsMaterial wallMaterial = new PhysicsMaterial(_wallMaterialName);

        wallMaterial.bounciness = _bounciness;

        float halfHeight = _size.y / 2f;
        float halfWidth = _size.x / 2f;
        float halfDepth = _size.z / 2f;

        CreateWall("Floor", new Vector3(0f, -halfHeight, 0f), new Vector3(_size.x, _thickness, _size.z), wallMaterial);
        CreateWall("Ceiling", new Vector3(0f, halfHeight, 0f), new Vector3(_size.x, _thickness, _size.z), wallMaterial);
        CreateWall("Left", new Vector3(-halfWidth, 0f, 0f), new Vector3(_thickness, _size.y, _size.z), wallMaterial);
        CreateWall("Right", new Vector3(halfWidth, 0f, 0f), new Vector3(_thickness, _size.y, _size.z), wallMaterial);
        CreateWall("Front", new Vector3(0f, 0f, halfDepth), new Vector3(_size.x, _size.y, _thickness), wallMaterial);
        CreateWall("Back", new Vector3(0f, 0f, -halfDepth), new Vector3(_size.x, _size.y, _thickness), wallMaterial);
    }

    private void CreateWall(string wallName, Vector3 localPosition, Vector3 wallScale, PhysicsMaterial wallMaterial)
    {
        GameObject wallObject = new GameObject(wallName);

        wallObject.transform.parent = transform;
        wallObject.transform.localPosition = localPosition;

        if (_clickThroughWalls)
        {
            wallObject.layer = LayerMask.NameToLayer(_ignoreRaycastLayer);
        }

        BoxCollider boxCollider = wallObject.AddComponent<BoxCollider>();

        boxCollider.size = Vector3.one;
        boxCollider.isTrigger = false;
        boxCollider.material = wallMaterial;

        wallObject.transform.localScale = wallScale;

        Rigidbody wallRigidbody = wallObject.AddComponent<Rigidbody>();

        wallRigidbody.isKinematic = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _size);
    }
}