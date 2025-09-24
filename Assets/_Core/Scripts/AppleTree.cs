using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AppleTree : MonoBehaviour
{
    [SerializeField] private TreeSO treeData;
    [SerializeField] private Transform spawnPoint;

    private float spawnDelay;
    private float lastSpawnTime;
    
    private int GetSign => Random.value > 0.5f ? 1 : -1;
    
    private void Awake()
    {
        ResetData();
    }

    private void FixedUpdate()
    {
        lastSpawnTime += Time.fixedDeltaTime;

        if (lastSpawnTime >= spawnDelay)
        {
            ResetData();
            CreateFruit();
        }
    }

    public void CreateFruit()
    {
        Apple fruit = Instantiate(treeData.FruitSO.Prefab).GetComponent<Apple>();
        
        fruit.transform.localPosition = spawnPoint.position;
        fruit.AddForce(GetDropDirection(), treeData.DropPower);
    }
    
    private Vector3 GetDropDirection()
    {
        Vector3 direction = Vector3.up;
        direction.x = Random.Range(.5f, .25f) * GetSign;
        direction.z = Random.Range(.5f, .25f) * GetSign;
        
        return direction;
    }
    private void ResetData()
    {
        lastSpawnTime = 0;
        spawnDelay = treeData.Fruitfulness + Random.Range(-treeData.SpawnDelta, treeData.SpawnDelta);
    }
}
