using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Level;
using UnityEngine.UIElements;

public class GameFlowController : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private UIIntroAnimator hudIntro;
    [SerializeField] private UIIntroAnimator controlsIntro;

    [Header("Panels")]
    [SerializeField] private UIPanelAnimator startPanelAnim;
    [SerializeField] private UIPanelAnimator winPanelAnim;
    [SerializeField] private UIPanelAnimator losePanelAnim;

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

        startPanelAnim.Show();
        winPanelAnim.Hide();
        losePanelAnim.Hide();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        startPanelAnim.Hide();
        winPanelAnim.Hide();
        losePanelAnim.Hide();

        hudIntro.PlayIn();
        controlsIntro.PlayIn();

        LevelManager.Instance.BeginLevel();
    }

    private void OnLevelCompleted()
    {
        Time.timeScale = 0f;

        //hudIntro.PlayOut();
        //controlsIntro.PlayOut();

        winPanelAnim.Show();
        losePanelAnim.Hide();
        startPanelAnim.Hide();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0f;

        hudIntro.PlayOut();
        controlsIntro.PlayOut();

        losePanelAnim.Show();
        winPanelAnim.Hide();
        startPanelAnim.Hide();
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