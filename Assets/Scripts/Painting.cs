using UnityEngine;

public class Painting
{
    private static MaterialPropertyBlock _propBlock;

    public void SetRandomColor(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Renderer>(out var renderer))
        {
            if (_propBlock == null)
            {
                _propBlock = new MaterialPropertyBlock();
            }

            renderer.GetPropertyBlock(_propBlock);

            _propBlock.SetColor("_BaseColor", Random.ColorHSV());

            renderer.SetPropertyBlock(_propBlock);
        }
    }
}