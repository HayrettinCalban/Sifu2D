using UnityEngine;

public class SimplePatrol2D : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 3.0f;

    private Transform currentTarget;
    private bool movingToB = true;
    private Vector3 initialScale; // Sprite'ın başlangıç yönünü korumak için

    void Start()
    {
        initialScale = transform.localScale; // Başlangıç scale değerini kaydet

        if (pointA == null || pointB == null)
        {
            Debug.LogError("Lütfen pointA ve pointB Transform'larını Inspector'dan atayın!", this);
            this.enabled = false;
            return;
        }

        // Başlangıç pozisyonunu A noktası olarak ayarla (Z eksenini koru veya sıfırla)
        transform.position = new Vector3(pointA.position.x, pointA.position.y, transform.position.z);
        currentTarget = pointB;
        movingToB = true;
        FaceTarget();
    }

    void Update()
    {
        if (currentTarget == null) return;

        // Hedef pozisyonunu al (Z eksenini koru)
        Vector3 targetPosition = new Vector3(currentTarget.position.x, currentTarget.position.y, transform.position.z);

        // Hedefe doğru hareket et
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Hedefe ulaşıldı mı kontrol et (Sadece X ve Y eksenlerini dikkate alarak)
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(targetPosition.x, targetPosition.y)) < 0.1f)
        {
            SwitchTarget();
        }
    }

    void SwitchTarget()
    {
        if (movingToB)
        {
            currentTarget = pointA;
            movingToB = false;
        }
        else
        {
            currentTarget = pointB;
            movingToB = true;
        }
        FaceTarget();
    }

    void FaceTarget()
    {
        if (currentTarget == null) return;

        // Sadece X eksenindeki yöne bak
        float directionX = currentTarget.position.x - transform.position.x;
        Vector3 scale = transform.localScale;

        if (Mathf.Abs(directionX) > 0.01f) // Çok küçük hareketlerde yön değiştirmemek için eşik değer
        {
            if (directionX > 0) // Sağa gidiyorsa
            {
                // Başlangıç scale'inin pozitif X değerini kullan
                scale.x = Mathf.Abs(initialScale.x);
            }
            else // Sola gidiyorsa
            {
                // Başlangıç scale'inin negatif X değerini kullan
                scale.x = -Mathf.Abs(initialScale.x);
            }
            transform.localScale = scale;
        }
        // Eğer tam dikey hareket ediyorsa (directionX ~ 0), yönü değiştirme
    }
}