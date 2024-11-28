using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private static DeathManager instance;
    private int deathCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
            deathCount = PlayerPrefs.GetInt("Death", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void IncrementDeathCount()
    {
        if (instance == null) return;

        instance.deathCount++;
        PlayerPrefs.SetInt("Death", instance.deathCount);
        Debug.Log($"Death count updated: {instance.deathCount}");
    }

    public static void ResetDeathCount()
    {
        if (instance == null) return;

        instance.deathCount = 0;
        PlayerPrefs.SetInt("Death", instance.deathCount);
        Debug.Log("Death count reset.");
    }

    private void OnApplicationQuit()
    {
        ResetDeathCount();
    }
}
