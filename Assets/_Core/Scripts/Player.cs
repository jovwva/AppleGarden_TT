using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BackpackPresenter backpack;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out Apple apple))
        {
            if (backpack.AddFruit(apple))
            {
                apple.Collect();
                apple.OnAppleDestroyed += OnAppleDestroyed;
            }
            else
            {
                Debug.Log("Рюкзак полон! Нельзя добавить больше фруктов.");
            }
        }
    }

    private void OnAppleDestroyed(Apple collectedFruit)
    {
        collectedFruit.OnAppleDestroyed -= OnAppleDestroyed;
        backpack.RemoveFruit(collectedFruit);
    }
}
