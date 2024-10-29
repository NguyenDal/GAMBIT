using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{

    public TMP_Dropdown level;

    public int level1 = 0;
    public int level2 = 1;
    public int level3 = 2;
    
    public void LoadSceneByName()
    {
        if(level.value == level1)
        {
            SceneManager.LoadScene("Level1");
        }

        else if(level.value == level2)
        {
            SceneManager.LoadScene("level2");
        }

        else if(level.value == level3)
        {
            SceneManager.LoadScene("level3");
        }
    }
}
