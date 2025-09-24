using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform TreeRoot;
    [SerializeField] private Garden Garden;
    
    [SerializeField] private GameObject treePref; 
    [SerializeField] private int treeCount = 6;
    [SerializeField] private LayerMask treeLayerMask;
    
    [Header("Debug data")]
    [SerializeField] private Color zoneColor;
    [SerializeField] private float deltaX = 3f;
    [SerializeField] private float deltaZ = 10f;

    private AppleTree[] appleTrees;
    
    private void Start()
    {
        StartCoroutine(GenerateMap());
    }

    private IEnumerator GenerateMap()
    {
        appleTrees = new AppleTree[treeCount];
        
        for (int i = 0; i < treeCount; i++)
        {
            Task spawnTask = SpawnEntite(treePref, i);
            yield return new WaitUntil(() => spawnTask.IsCompleted);
            // Debug.Log($"Tree {i} was spawned");
        }
        Garden.SetTrees(appleTrees);
    }
    private async Task SpawnEntite(GameObject entity, int id)
    {
        Vector3? spawnPoint = await FindFreeSpawnPoint();
        
        if (spawnPoint.HasValue)
        {
            appleTrees[id] = 
            Instantiate(entity, spawnPoint.Value, Quaternion.identity, TreeRoot)
                .GetComponent<AppleTree>();
        }
        else
        {
            Debug.LogWarning("Failed to find free spawn point");
        }
    }
    private async Task<Vector3?> FindFreeSpawnPoint(int maxAttempts = 10)
    {
        int attempts = 0;
        while (attempts < maxAttempts)
        {
            // Debug.Log("Trying to find free spawn point");
        
            Vector3 candidatePos = transform.position;
            candidatePos.x += Random.Range(-1f, 1f) * deltaX;
            candidatePos.z += Random.Range(-1f, 1f) * deltaZ;
            
            bool isOccupied = Physics.CheckCapsule(
                candidatePos,
                candidatePos + Vector3.up * 2f, 
                1.25f,
                treeLayerMask
            );
        
            if (!isOccupied)
            {
                // Debug.Log("Free space found");
                return candidatePos;
            }
        
            await Task.Yield();
            attempts++;
        }
        return null;
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = zoneColor;
        Vector3 center = transform.position + Vector3.up;

        Vector3 p1 = center + new Vector3(-deltaX, 0, -deltaZ);
        Vector3 p2 = center + new Vector3(deltaX, 0, -deltaZ);
        Vector3 p3 = center + new Vector3(deltaX, 0, deltaZ);
        Vector3 p4 = center + new Vector3(-deltaX, 0, deltaZ);
        
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
        
        Gizmos.DrawSphere(center, 0.2f); 
        Gizmos.DrawLine(p1, p3);         
        Gizmos.DrawLine(p2, p4);         
    }
}
