using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x > 8.7f) // Destroy when out of bounds
        {
            Destroy(gameObject);
        }
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Debug.Log("HIT  ENEMY");
            Destroy(gameObject);
        }
    }
}
