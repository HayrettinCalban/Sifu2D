using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    private Vector2 moveDirection;

    void Start()
    {
        Destroy(gameObject, lifeTime); // 3 sn sonra yok et
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet çarptı: " + collision.collider.name + " | Tag: " + collision.collider.tag);

        if (collision.collider.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
                Debug.Log("Enemy'ye hasar verildi!");
            }
            Destroy(gameObject);
        }
        else if (!collision.collider.CompareTag("Player"))
        {
            Debug.Log("Bullet başka bir objeye çarptı ve yok oldu.");
            Destroy(gameObject);
        }
    }
}
