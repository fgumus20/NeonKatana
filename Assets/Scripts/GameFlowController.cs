using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Level;

public class GameFlowController : MonoBehaviour
{
    [Header("Roots")]
    [SerializeField] private GameObject hudRoot;
    [SerializeField] private GameObject controlsRoot;

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

        hudRoot.SetActive(false);
        controlsRoot.SetActive(false);

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

        hudRoot.SetActive(true);
        controlsRoot.SetActive(true);

        LevelManager.Instance.BeginLevel();
    }

    private void OnLevelCompleted()
    {
        Time.timeScale = 0f;

        hudRoot.SetActive(false);
        controlsRoot.SetActive(false);

        winPanel.SetActive(true);
        losePanel.SetActive(false);
        startPanel.SetActive(false);
    }

    private void OnGameOver()
    {
        Time.timeScale = 0f;

        hudRoot.SetActive(false);
        controlsRoot.SetActive(false);

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
        Debug.Log("a");
        Retry();
    }
}