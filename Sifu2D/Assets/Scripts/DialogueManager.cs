using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TMP ekle
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI dialogueText; // TMP kullan
    public GameObject dialoguePanel; // Diyalog paneli

    [Header("Dialogue Data")]
    public List<string> dialogueLines; // Diyalog cümleleri

    private int currentLineIndex = 0; // Şu anki diyalog satırı
    private static bool dialogueShown = false; // Sadece ilk sahne yüklemede true olacak

    void Start()
    {
        if (!dialogueShown)
        {
            StartDialogue();
            dialogueShown = true;
        }
        else
        {
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
        }
    }

    public void StartDialogue()
    {
        if (dialogueLines.Count > 0)
        {
            dialoguePanel.SetActive(true);
            currentLineIndex = 0;
            DisplayLine();
            Time.timeScale = 0f; // Oyunu durdur
        }
    }

    void DisplayLine()
    {
        if (currentLineIndex < dialogueLines.Count)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    public void NextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Count)
        {
            DisplayLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = ""; // Yazıyı temizle
        Time.timeScale = 1f; // Oyunu devam ettir
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialoguePanel.activeSelf)
        {
            NextLine();
        }
    }

    public void RestartScene()
    {
        Time.timeScale = 1f; // Oyun durduysa tekrar başlat
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
