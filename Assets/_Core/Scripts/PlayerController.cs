using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    [Space, SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed    = 10f;

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical   = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        
        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        
        if (movement.magnitude > 1f)
            movement.Normalize();

        movement *= moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + movement);
    }
}
