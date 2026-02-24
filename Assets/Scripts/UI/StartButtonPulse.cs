using UnityEngine;
using DG.Tweening;

public class StartButtonPulse : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] private RectTransform target;

    [Header("Pulse")]
    [SerializeField] private float scaleUp = 1.07f;
    [SerializeField] private float scaleDur = 0.55f;

    [Header("Float")]
    [SerializeField] private float floatY = 6f;
    [SerializeField] private float floatDur = 1.2f;

    private Vector2 _startAnchoredPos;
    private Sequence _seq;

    private void Awake()
    {
        if (!target) target = transform as RectTransform;
        _startAnchoredPos = target.anchoredPosition;
    }

    private void OnEnable()
    {
        Play();
    }

    private void OnDisable()
    {
        Stop();
    }

    public void Play()
    {
        Stop();

        target.localScale = Vector3.one;
        target.anchoredPosition = _startAnchoredPos;

        _seq = DOTween.Sequence().SetUpdate(true);

        _seq.Append(target.DOScale(scaleUp, scaleDur).SetEase(Ease.OutQuad));
        _seq.Append(target.DOScale(1f, scaleDur).SetEase(Ease.InQuad));

        target.DOAnchorPosY(_startAnchoredPos.y + floatY, floatDur)
              .SetEase(Ease.InOutSine)
              .SetLoops(-1, LoopType.Yoyo)
              .SetUpdate(true);

        _seq.SetLoops(-1);
    }

    public void Stop()
    {
        if (_seq != null) _seq.Kill();
        DOTween.Kill(target);
    }
}