using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public Image healthBarFill; // World space canvas'taki fill objesi

    private CharacterController characterController;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        characterController = GetComponent<CharacterController>();
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damageAmount;
        Debug.Log("Oyuncu hasar aldı! Kalan Can: " + currentHealth);

        UpdateHealthBar();

        if (currentHealth <= 0 && characterController != null)
        {
            characterController.Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}