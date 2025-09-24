using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private FruitSO fruitData;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform view;

    private float lifeTime;
    
    private void FixedUpdate()
    {
        lifeTime += Time.fixedDeltaTime;

        if (lifeTime >= fruitData.LifeTime)
        {
            DestroyFruit();
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

    private void DestroyFruit()
    {
        Destroy(this.gameObject);
    }
}
