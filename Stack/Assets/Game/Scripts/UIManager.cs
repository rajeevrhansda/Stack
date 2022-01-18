using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private DontDestroyAudio _dontDestroyAudio;
    [SerializeField] private GameObject pausemenu;//
    [SerializeField] private Button leaderboard;//
    [SerializeField] private Button removeads;//
    [SerializeField] private Button settings;//
    [SerializeField] private Button videoAd;//
    private Interstitial _interstitial;//
    private GameManager _gamemanager;//
    bool paid = false;
    public bool vibrateAllow;
    public bool soundaAllow;
    [SerializeField] Text vibrateText;
    [SerializeField] Text soundText;


    private bool visibleStart;
    private bool visibleRestart;

    private RawImage background;
    private CanvasGroup canvas;

    [SerializeField] private Text ctaStart;
    [SerializeField] private Text ctaRestart;

    [SerializeField] private TitleManager title;
    [SerializeField] private ScoreManager score;

    [Header("On \"Tap To Start\" game event:")]
    [SerializeField] private UnityEvent start = new UnityEvent();

    [Header("On \"Tap To Restart\" game event:")]
    [SerializeField] private UnityEvent restart = new UnityEvent();

    private void Update()
    {

    }

    private void Awake()
    {

        videoAd.gameObject.SetActive(false);//
        leaderboard.interactable = false;//
        removeads.interactable = false;//
        settings.interactable = false;//
        pausemenu.SetActive(false);//
        _dontDestroyAudio = FindObjectOfType<DontDestroyAudio>();//


        background = transform.GetChild(0).GetComponent<RawImage>();
        canvas = transform.GetComponent<CanvasGroup>();

        ctaRestart.color = ColorManager.TRANSPARENT;
        ctaStart.color = ColorManager.TRANSPARENT;

        Texture2D texture = new Texture2D(1, 2);

        texture.SetPixels(new Color[] {
            ColorManager.SOLID_BLACK,
            ColorManager.TRANSPARENT
        });


        SetSound();
        SetVibrate();

        texture.Apply();
        CheckPaid();
    }
    private void Start()
    {
        StartCoroutine(Initialize());
        _interstitial = FindObjectOfType<Interstitial>();//
        _gamemanager = FindObjectOfType<GameManager>();//

    }
    public IEnumerator Initialize()
    {
        StartCoroutine(title.Initialize());
        yield return new WaitForSeconds(2.5f);
        background.CrossFadeAlpha(0.0f, 1.0f, false);
        StartCoroutine(ShowStart(true));
    }
    private IEnumerator ShowStart(bool initial = false)
    {
        yield return new WaitForSeconds(1.0f + System.Convert.ToInt32(!initial) * 0.5f);

        StartCoroutine(FadeCTA(ctaStart));
        if (!initial) title.FadeIn();

        canvas.interactable = true;
        visibleStart = true;

        leaderboard.interactable = true;//
        settings.interactable = true;//
        if (paid)
        {
            removeads.interactable = false;//
        }
        else
        {
            removeads.interactable = true;//true
        }
    }
    public void OnGameOver()
    {
        StartCoroutine(ShowRestart());

        _interstitial.ShowInterstitial();//
    }
    private IEnumerator ShowRestart()
    {
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(FadeCTA(ctaRestart));
        score.ShowBest();

        leaderboard.interactable = true;//
        settings.interactable = true;//
        if (paid)
        {
            removeads.interactable = false;//
        }
        else
        {
            removeads.interactable = true;//true
        }
        videoAd.gameObject.SetActive(true);//


        yield return new WaitForSeconds(0.5f);
        canvas.interactable = true;
        visibleRestart = true;
    }
    private void HideRestart()
    {
        videoAd.gameObject.SetActive(false);//
        _gamemanager.SwitchAllowedFalse();//

        visibleRestart = false;
        canvas.interactable = false;

        StartCoroutine(FadeCTA(ctaRestart, false));

        StartCoroutine(ShowStart(false));
        restart.Invoke();
        score.Hide();
    }
    private IEnumerator HideStart()
    {
        title.FadeOut();
        visibleStart = false;
        canvas.interactable = false;

        StartCoroutine(FadeCTA(ctaStart, false));
        yield return new WaitForSeconds(1.0f);

        score.ShowCurrent();
        start.Invoke();
    }

    private IEnumerator FadeCTA(Text cta, bool visible = true, float duration = 0.5f)
    {
        float startTime = Time.time;
        float endTime = Time.time + duration;

        Color32 target = visible ? ColorManager.SOLID_WHITE : ColorManager.TRANSPARENT;
        Color32 current = visible ? ColorManager.TRANSPARENT : ColorManager.SOLID_WHITE;

        while (Time.time < endTime)
        {
            float time = (Time.time - startTime) / duration;
            cta.color = Color32.Lerp(current, target, time);
            yield return null;
        }

        cta.color = target;
    }
    public void PrivacyPolicy()//
    {
        Application.OpenURL("https://docs.google.com/document/d/1TQHdQuegtTo3ZZPROrz5yPYBcMNUJ1zr7Ip3kZOFEFg/edit?usp=sharing");
    }
    public void RateUS()//
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.HansdaCorp.Stack");
    }


    public void ShowPauseMenu()
    {
        pausemenu.SetActive(true);
        leaderboard.interactable = false;
        removeads.interactable = false;
        settings.interactable = false;
    }
    public void HidePauseMenu()
    {
        pausemenu.SetActive(false);
        leaderboard.interactable = true;
        if (paid)
        {
            removeads.interactable = false;//
        }
        else
        {
            removeads.interactable = true;//true
        }
        settings.interactable = true;
    }
    public void GameOverMenu()
    {
        videoAd.gameObject.SetActive(true);
        canvas.interactable = true;
    }
    public void GameOverMenuFalse()/////////////////////////////////////////////
    {
        StartCoroutine(FadeCTA(ctaRestart, false));
        score.ShowBest();

        videoAd.gameObject.SetActive(false);

        leaderboard.interactable = false;
        removeads.interactable = false;
        settings.interactable = false;

        visibleStart = false;
        canvas.interactable = false;

    }
    public void StatGame()
    {
        if (canvas.interactable)
        {
            if (visibleStart)
            {
                StartCoroutine(HideStart());
            }

            else if (visibleRestart)
            {
                HideRestart();

                _interstitial.RequestInterstitial();
                _interstitial.LoadAd();
            }
        }
    }

    public void VibrateSwitch()
    {

        if (vibrateAllow)
        {
            vibrateAllow = false;
            PlayerPrefs.SetInt("Vibrate", 0);
            vibrateText.text = "Vibrate Off";

        }
        else
        {
            vibrateAllow = true;
            PlayerPrefs.SetInt("Vibrate", 1);
            vibrateText.text = "Vibrate On";



        }
    }
    public void SoundSwitch()
    {


        if (soundaAllow)
        {
            soundaAllow = false;
            PlayerPrefs.SetInt("Sound", 0);
            _dontDestroyAudio.MUTEON();
            soundText.text = "Sound Off";




        }
        else
        {
            soundaAllow = true;
            PlayerPrefs.SetInt("Sound", 1);
            _dontDestroyAudio.MUTEOFF();
            soundText.text = "Sound On";




        }
    }
    public void SetSound()
    {
        int value = PlayerPrefs.GetInt("Sound", 1);
        if (value == 1)
        {
            soundaAllow = true;
            soundText.text = "Sound On";



        }
        else
        {
            soundaAllow = false;
            soundText.text = "Sound Off";



        }


    }
    public void SetVibrate()
    {
        int value = PlayerPrefs.GetInt("Vibrate", 1);
        if (value == 1)
        {
            soundaAllow = true;
            vibrateText.text = "Vibrate On";


        }
        else
        {
            soundaAllow = false;
            vibrateText.text = "Vibrate Off";



        }
    }

    void CheckPaid()
    {
        int value = PlayerPrefs.GetInt("paid", 0);
        if(value == 1)
        {
            paid = true;
        }
        else
        {
            paid = false;
        }
    }
}