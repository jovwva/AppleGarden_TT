using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BackpackPresenter backpack;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out FruitRooter apple))
        {
            if (backpack.AddFruit(apple.RootApple))
            {
                apple.RootApple.Collect();
                apple.RootApple.OnAppleDestroyed += OnAppleDestroyed;
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
