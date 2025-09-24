using UnityEngine;

[CreateAssetMenu(fileName = "FruitSO_", menuName = "SO/FruitSo", order = 1)]
public class FruitSO : ScriptableObject
{
    [field: SerializeField] public Transform Prefab { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public float LifeTime { get; private set; }
}
