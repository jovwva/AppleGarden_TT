using System;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [field: SerializeField] public  FruitSO FruitData {get; private set;}
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform view;
    
    private float lifeTime;
    
    public event Action<Apple> OnAppleDestroyed;
    
    private void FixedUpdate()
    {
        lifeTime += Time.fixedDeltaTime;

        if (lifeTime >= FruitData.LifeTime)
        {
            ReleaseFruit();
        }
    }
    
    public void Collect()
    {
        rb.isKinematic = true;
        view.gameObject.SetActive(false);
    }
    
    public void AddForce(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void ReleaseFruit()
    {
        // Debug.Log("Releasing Fruit");
        OnAppleDestroyed?.Invoke(this);
        
        lifeTime = 0;
        rb.isKinematic = false;
        view.gameObject.SetActive(true);
    }
}
