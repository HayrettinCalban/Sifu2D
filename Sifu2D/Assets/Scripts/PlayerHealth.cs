using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; // Coroutine için

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
            StartCoroutine(ReloadSceneAfterDelay());
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthBar();
    }

    IEnumerator ReloadSceneAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Update()
    {
        // Oyuncu çok aşağıya düşerse öl ve sahneyi yeniden başlat
        if (transform.position.y < -10f)
        {
            if (characterController != null)
                characterController.Die();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}