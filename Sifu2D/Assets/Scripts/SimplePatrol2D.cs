using UnityEngine;

public class SimplePatrol2D : MonoBehaviour
{
    public int stepCount = 5; // Sağa ve sola kaç adım gitsin (Inspector'dan ayarlanır)
    public float stepSize = 1f; // Her adımda kaç birim gitsin (Inspector'dan ayarlanır)
    public float speed = 3.0f;

    private int direction = 1; // 1: sağa, -1: sola
    private Vector3 startPosition;
    private Vector3 initialScale;

    void Start()
    {
        startPosition = transform.position;
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Hedef pozisyonu hesapla
        Vector3 targetPosition = startPosition + Vector3.right * direction * stepSize * stepCount;

        // Hareket et
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Hedefe ulaştıysa yön değiştir
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            direction *= -1;
            FaceDirection();
        }
    }

    void FaceDirection()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(initialScale.x) * direction;
        transform.localScale = scale;
    }
}