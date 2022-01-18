using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    private PlayGames _playgames;
    [HideInInspector]
    public int current {
        set {
            score = value;
            int h_score = PlayerPrefs.GetInt("highscore", 0);//
            if (h_score < score)//
            {
                PlayerPrefs.SetInt("highscore", score);//
                _playgames.AddScoreToLeaderboard(PlayerPrefs.GetInt("highscore"));//
            }
            //best = Mathf.Max(value, best);

            bestText.text = $"Best: {PlayerPrefs.GetInt("highscore")}";//
           
            currentText.text = score.ToString();
        }

        get {
            return score;
        }
    }

    private Text currentText;
    private Text bestText;

    private int score = 0;
    private int best = 0;

    private void Awake()
    {
        _playgames = FindObjectOfType<PlayGames>();//
        bestText = transform.GetChild(1).GetComponent<Text>();
        currentText = transform.GetChild(0).GetComponent<Text>();
        currentText.color = bestText.color = ColorManager.TRANSPARENT;
    }

    public void ShowCurrent()
    {
        StartCoroutine(Fade(currentText));
    }

    public void ShowBest()
    {
        StartCoroutine(Fade(bestText));
    }

    public void Hide()
    {
        StartCoroutine(Fade(currentText));
        StartCoroutine(Fade(bestText));
    }

    private IEnumerator Fade(Text score)
    {
        float startTime = Time.time;
        float endTime = Time.time + 0.5f;

        Color target = GetTargetColor(score.color.a);
        Color current = GetCurrentColor(score.color.a);

        while (Time.time < endTime)
        {
            float time = (Time.time - startTime) / 0.5f;
            score.color = Color.Lerp(current, target, time);
            yield return null;
        }

        score.color = target;
    }

    private Color GetTargetColor(float alpha) =>
        alpha == 0.0f ? ColorManager.SOLID_WHITE : ColorManager.TRANSPARENT;

    private Color GetCurrentColor(float alpha) =>
        alpha == 0.0f ? ColorManager.TRANSPARENT : ColorManager.SOLID_WHITE;
}
