using UnityEngine;
using TMPro;
using DG.Tweening;

public class WaveBannerController : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private RectTransform rt;
    [SerializeField] private TMP_Text label;

    [Header("Timing")]
    [SerializeField] private float inDur = 0.22f;
    [SerializeField] private float hold = 0.55f;
    [SerializeField] private float outDur = 0.18f;

    private Vector2 _basePos;
    private Tween _tween;

    void Awake()
    {
        if (!cg) cg = GetComponent<CanvasGroup>();
        if (!rt) rt = (RectTransform)transform;

        _basePos = rt.anchoredPosition;
        cg.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void ShowWave(int wave, int total)
    {
        gameObject.SetActive(true);
        label.text = $"WAVE {wave}/{total}";

        _tween?.Kill();

        cg.alpha = 0f;
        rt.anchoredPosition = _basePos + new Vector2(0, 90);
        rt.localScale = Vector3.one * 0.95f;

        _tween = DOTween.Sequence()
            .SetUpdate(true)
            .Append(cg.DOFade(1f, inDur))
            .Join(rt.DOAnchorPos(_basePos, inDur).SetEase(Ease.OutCubic))
            .Join(rt.DOScale(1f, inDur).SetEase(Ease.OutBack))
            .AppendInterval(hold)
            .Append(cg.DOFade(0f, outDur))
            .Join(rt.DOAnchorPos(_basePos + new Vector2(0, -50), outDur).SetEase(Ease.InCubic))
            .OnComplete(() => gameObject.SetActive(false));
    }
}