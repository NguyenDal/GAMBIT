using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Complete : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("plyrBody"))
        {
            Debug.Log("Procs");
            EndLevel();
        }
    }

    void EndLevel()
    {
        SceneManager.LoadScene("LevelCompleteDisplay_Level1");
    }
}
