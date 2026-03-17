using UnityEngine;

public class CubeData : MonoBehaviour
{
    [SerializeField] private double _splitChance = 1.0;
    [SerializeField] private int _splitLevel = 0;

    public double SplitChance
    { 
        get { return _splitChance; }
        set { _splitChance = value; }
    }

    public int SplitLevel
    {
        get { return _splitLevel; }
        set { _splitLevel = value; }
    }
}