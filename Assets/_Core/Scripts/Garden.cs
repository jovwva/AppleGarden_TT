using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class Garden : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles;
    
    [SerializeField] private FruitSO appleData;
    [SerializeField] private int defaultPoolSize = 20;
    [SerializeField] private int maxPoolSize = 50;
    [SerializeField] private Transform applePoolRoot; // Для организации иерархии
    
    private ObjectPool<Apple> applePool;
    private int lastPSIndex = 0;
    
    private void Awake()
    {
        InitializePool();
    }

    public void SetTrees(AppleTree[] appleTrees)
    {
        foreach (var tree in appleTrees)
        {
            RegisterTree(tree);
        }
    }

    private void InitializePool()
    {
        applePool = new ObjectPool<Apple>(
            createFunc: CreateApple,
            actionOnGet: OnTakeAppleFromPool,
            actionOnRelease: OnReturnAppleToPool,
            actionOnDestroy: OnDestroyApple,
            collectionCheck: true,
            defaultCapacity: defaultPoolSize,
            maxSize: maxPoolSize
        );

        // Предварительно создаем объекты в пуле
        PrewarmPool();
    }

    private void PrewarmPool()
    {
        List<Apple> prewarmedApples = new List<Apple>();
        for (int i = 0; i < defaultPoolSize; i++)
        {
            prewarmedApples.Add(applePool.Get());
        }
        
        foreach (var apple in prewarmedApples)
        {
            applePool.Release(apple);
        }
    }

    private Apple CreateApple()
    {
        Apple apple = Instantiate(appleData.Prefab, applePoolRoot).GetComponent<Apple>();
        
        apple.gameObject.SetActive(false);
        apple.OnAppleDestroyed += ReturnAppleToPool;
        return apple;
    }

    private void OnTakeAppleFromPool(Apple apple)
    {
        apple.gameObject.SetActive(true);
    }

    private void OnReturnAppleToPool(Apple apple) 
    {
        apple.gameObject.SetActive(false);
    }

    private void OnDestroyApple(Apple apple)
    {
        apple.OnAppleDestroyed -= ReturnAppleToPool;
        Destroy(apple.gameObject);
    }

    private void RegisterTree(AppleTree tree)
    {
        tree.Initialize(this);
    }

    public Apple GetApple()
    {
        return applePool.Get();
    }

    private void ReturnAppleToPool(Apple apple)
    {
        applePool.Release(apple);
        ShowParticle(apple.transform.localPosition);
    }

    private void ShowParticle(Vector3 position)
    {
        particles[lastPSIndex].transform.localPosition = position;
        particles[lastPSIndex].Play();

        lastPSIndex++;
        if (lastPSIndex >= particles.Length)
        {
            lastPSIndex = 0;
        }
    }
}