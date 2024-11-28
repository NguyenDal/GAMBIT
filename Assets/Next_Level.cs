using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickExample : MonoBehaviour
{
    [SerializeField]  
    public string level;
    public Button LevelButton;

    void Start()
    {
        
        LevelButton.onClick.AddListener(NextLevel);

    }

    void NextLevel()
    {
        SceneManager.LoadScene(level);
    }
}