using UnityEngine;

[CreateAssetMenu(fileName = "TreeSO_", menuName = "SO/TreeSO", order = 1)]
public class TreeSO : ScriptableObject
{
    [field: SerializeField] public FruitSO FruitSO { get; private set; }
    
    [field: SerializeField, Space] public float   Fruitfulness { get; private set; }
    [field: SerializeField] public float SpawnDelta { get; private set; }
    [field: SerializeField] public float DropPower { get; private set; }
}
