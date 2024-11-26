using UnityEngine;
using UnityEngine.SceneManagement;
public class Level_Complete : MonoBehaviour

{

    
    public TimerScript timerScript;

    [SerializeField]
    public string levelcomplete;
    void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("plyrBody"))
        {
            EndLevel();
        }
    }

    void EndLevel()
    {
        timerScript.OnSceneChangede();
        SceneManager.LoadScene("LevelCompleteDisplay_Level1");
    }
}
