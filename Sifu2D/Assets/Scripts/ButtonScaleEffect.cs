using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.05f, 1.05f, 1f);
    public Vector3 pressedScale = new Vector3(0.9f, 0.9f, 1f);
    public float scaleSpeed = 10f;

    public AudioClip buttonSound; // Inspector'dan ata
    private AudioSource audioSource;

    private Vector3 originalScale;
    private bool isHovered = false;
    private bool isPressed = false;

    void Awake()
    {
        originalScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 targetScale = originalScale;
        if (isPressed)
            targetScale = pressedScale;
        else if (isHovered)
            targetScale = hoverScale;

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.unscaledDeltaTime * scaleSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        if (audioSource != null && buttonSound != null)
            audioSource.PlayOneShot(buttonSound);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}