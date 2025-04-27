using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // Eğer başka bir MusicManager varsa yok et, yoksa devam et
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Bu objeyi sahneler arasında yok etme
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
