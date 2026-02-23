using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Level;

public class GameFlowController : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private UIIntroAnimator hudIntro;
    [SerializeField] private UIIntroAnimator controlsIntro;

    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void OnEnable()
    {
        GameEvents.LevelCompleted += OnLevelCompleted;
        GameEvents.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameEvents.LevelCompleted -= OnLevelCompleted;
        GameEvents.GameOver -= OnGameOver;
    }

    private void Start()
    {
        ShowStart();
    }

    public void ShowStart()
    {
        Time.timeScale = 0f;

        hudIntro.PlayOut();
        controlsIntro.PlayOut();

        startPanel.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        startPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        hudIntro.PlayIn();
        controlsIntro.PlayIn();

        LevelManager.Instance.BeginLevel();
    }

    private void OnLevelCompleted()
    {
        Time.timeScale = 0f;

        hudIntro.PlayOut();
        controlsIntro.PlayOut();

        winPanel.SetActive(true);
        losePanel.SetActive(false);
        startPanel.SetActive(false);
    }

    private void OnGameOver()
    {
        Time.timeScale = 0f;

        hudIntro.PlayOut();
        controlsIntro.PlayOut();

        losePanel.SetActive(true);
        winPanel.SetActive(false);
        startPanel.SetActive(false);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        DG.Tweening.DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Next()
    {
        Retry();
    }
}