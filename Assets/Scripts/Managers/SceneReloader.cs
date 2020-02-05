using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    [SerializeField] private GameEvent OnGameStarted;

    [SerializeField] private GlobalFlag wasGamePlayed;

    private void Start()
    {
        if (wasGamePlayed.Flag)
        {
            OnGameStarted.Raise();
        }
    }
    
    public void ReloadScene()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationQuit()
    {
        wasGamePlayed.SetFlag (false);
        Debug.Log ("QUIT");
    }
}