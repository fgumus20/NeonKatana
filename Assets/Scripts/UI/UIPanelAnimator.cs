using UnityEngine;
using DG.Tweening;

public class UIPanelAnimator : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    [SerializeField] RectTransform rt;
    [SerializeField] float dur = 0.25f;

    Tween tween;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        rt = (RectTransform)transform;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        tween?.Kill();

        cg.alpha = 0f;
        rt.localScale = Vector3.one * 0.95f;

        tween = DOTween.Sequence().SetUpdate(true)
            .Append(cg.DOFade(1f, dur))
            .Join(rt.DOScale(1f, dur).SetEase(Ease.OutBack));
    }

    public void Hide()
    {
        tween?.Kill();
        gameObject.SetActive(false);
    }
}