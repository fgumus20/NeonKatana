using UnityEngine;
using DG.Tweening;

public class UIIntroAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    [SerializeField] private Vector2 startOffset = new Vector2(0, -300);
    [SerializeField] private float duration = 0.35f;
    [SerializeField] private Ease easeIn = Ease.OutCubic;
    [SerializeField] private Ease easeOut = Ease.InCubic;

    private Vector2 _basePos;
    private Tween _tw;

    void Awake()
    {
        if (!target) target = (RectTransform)transform;
        _basePos = target.anchoredPosition;
    }

    public void PlayIn()
    {
        _tw?.Kill();
        target.anchoredPosition = _basePos + startOffset;
        target.gameObject.SetActive(true);

        _tw = target.DOAnchorPos(_basePos, duration)
            .SetEase(easeIn)
            .SetUpdate(true);
    }

    public void PlayOut(System.Action onDone = null)
    {
        _tw?.Kill();
        _tw = target.DOAnchorPos(_basePos + startOffset, duration * 0.8f)
            .SetEase(easeOut)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                target.gameObject.SetActive(false);
                onDone?.Invoke();
            });
    }
}