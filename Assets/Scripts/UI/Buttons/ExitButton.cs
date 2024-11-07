using UnityEngine;
public class ExitButton : MonoBehaviour
{
    public static bool clicked = false;


    void LateUpdate()
    {
        clicked = false;
    }

    public void Click()
    {
        clicked = true;
        Application.Quit();
    }

}

