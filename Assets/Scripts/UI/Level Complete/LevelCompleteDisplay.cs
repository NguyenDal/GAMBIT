using System.Collections;
using System.Collections.Generic;
using trial;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteDisplay : MonoBehaviour
{
    public Image[] stars;
    public Sprite filledStar;

    //taken
    [SerializeField] float EnlargeScale = 1.5f;
    [SerializeField] float ShrinkScale = 1f;
    [SerializeField] float EnlargeDuration = 0.25f;
    [SerializeField] float ShrinkDuration = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        //int starCount = PlayerPrefs.GetInt("StarCount", 0);
        // DisplayStars(starCount);
       setNumberOfStarsToDisplay();

    }

    //taken
    public void ShowStars(int numberOfStars)
    {
        StartCoroutine(ShowStarsRoutine(numberOfStars));
    }

    //taken
    private IEnumerator ShowStarsRoutine(int numberOfStars)
    {
        foreach (Image star in stars)
        {
            star.transform.localScale = Vector3.zero;
        }

        for (int i = 0; i < numberOfStars; i++)
        {
            yield return StartCoroutine(EnlargeAndShrinkStar(stars[i]));
        }
    }

    //taken
    private IEnumerator EnlargeAndShrinkStar(Image star)
    {
        yield return StartCoroutine(ChangeStarScale(star, EnlargeScale, EnlargeDuration));
        yield return StartCoroutine(ChangeStarScale(star, ShrinkScale, ShrinkDuration));
    }

    //taken
    private IEnumerator ChangeStarScale(Image star, float targetScale, float duration)
    {
        Vector3 initialScale = star.transform.localScale;
        Vector3 finalScale = new Vector3(targetScale, targetScale, targetScale);

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            star.transform.localScale = Vector3.Lerp(initialScale, finalScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        star.transform.localScale = finalScale;
    }

    public void setNumberOfStarsToDisplay()
    {
        int starsToAward = 0;
        //if player had completed a level, award 1 star
        if (PlayerPrefs.GetInt("LevelCompleted") == 1)
        {
            starsToAward++;
            Debug.Log("Stars: " + starsToAward);
        }

        if (PlayerPrefs.GetInt("PlayerCollecdtedAllPickUps") == 1)
        {
            starsToAward++;
            Debug.Log("Stars: " + starsToAward);
        }
        DisplayStars(starsToAward);
        ShowStars(starsToAward);
    }


    private void DisplayStars(int starCount)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (i < starCount)
            {
                stars[i].sprite = filledStar;
                stars[i].color = new Color(stars[i].color.r, stars[i].color.g, stars[i].color.b, 255f);
            }
            else
            {
                stars[i].sprite = null;
            }

            PlayerPrefs.SetInt("LevelCompleted", 0);
            PlayerPrefs.SetInt("PlayerCollecdtedAllPickUps", 0);
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNextLevelButton(string NextLevelSceneName)
    {
        // Load the next level or main menu
        SceneManager.LoadScene("NextLevelSceneName");
    }
}
