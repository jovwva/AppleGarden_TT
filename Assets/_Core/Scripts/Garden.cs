using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class Garden : MonoBehaviour
{
    [SerializeField] private FruitSO appleData;
    [SerializeField] private int defaultPoolSize = 20;
    [SerializeField] private int maxPoolSize = 50;
    [SerializeField] private Transform applePoolRoot; // Для организации иерархии
    
    private ObjectPool<Apple> applePool;

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
        Apple apple = Instantiate(appleData.Prefab).GetComponent<Apple>();
        
        // Организуем иерархию для чистоты в сцене
        if (applePoolRoot != null)
        {
            apple.transform.SetParent(applePoolRoot);
        }
        
        apple.gameObject.SetActive(false);
        apple.OnAppleDestroyed += ReturnAppleToPool;
        return apple;
    }

    private void OnTakeAppleFromPool(Apple apple)
    {
        apple.gameObject.SetActive(true);
        // apple.ResetApple();
    }

    private void OnReturnAppleToPool(Apple apple)
    {
        apple.gameObject.SetActive(false);
        
        // Возвращаем в корень пула для организации
        if (applePoolRoot != null)
        {
            apple.transform.SetParent(applePoolRoot);
        }
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
    }
}