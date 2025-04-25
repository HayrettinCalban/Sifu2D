using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackCooldown = 0.5f; // Saldırı bekleme süresi
    public float attackRange = 1f; // Saldırı menzili
    public int attackDamage = 20; // Verilen hasar
    public LayerMask enemyLayers; // Düşmanların bulunduğu katman

    public Transform attackPoint; // Saldırı noktası

    private bool canAttack = true; // Saldırı yapabilir mi?

    void Update()
    {
        // Sol tık kontrolü
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false; // Saldırı sırasında başka saldırıyı engelle

        // Saldırı yapılan düşmanları bul
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Düşmanlara hasar ver
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
        }

        yield return new WaitForSeconds(attackCooldown); // Saldırı bekleme süresi
        canAttack = true; // Tekrar saldırı yapılabilir
    }

    // Saldırı menzilini sahnede göster
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
