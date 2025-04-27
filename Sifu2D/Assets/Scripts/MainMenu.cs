using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Oyunun sahnesini yükler
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        // Oyundan çıkar
        Application.Quit();
        Debug.Log("Oyun kapandı!"); // Editörde çalışmaz ama build alınca çalışır.
    }
}
