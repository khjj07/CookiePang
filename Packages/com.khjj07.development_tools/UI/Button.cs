using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class Button : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    protected Sequence ScaleSquence;
    private Vector3 Origin;
    [SerializeField]
    private Vector3 Scaled = new Vector3(1, 1, 1);
    [SerializeField]
    private Ease Easing;
    public UnityEvent OnClick;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    float lastTimeClick=0.0f;
    public virtual void Start()
    {
        Origin = transform.localScale;
        ScaleSquence = DOTween.Sequence();
    }
    public void OnEnable()
    {
        ScaleSquence.Restart();
    }

    public void OnDestroy()
    {
        ScaleSquence.Kill();
    }

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        float currentTimeClick = pointerEventData.clickTime;
        if (Mathf.Abs(currentTimeClick - lastTimeClick) < 0.75f)
        {
            transform.DOKill();
            transform.DOScale(Origin, 0.1f).SetEase(Easing)
                .OnComplete(() => { OnClick.Invoke(); });
        }
        lastTimeClick = currentTimeClick;
        
    }

    public virtual void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.DOKill();
        transform.DOScale(Scaled, 0.1f).SetEase(Easing);
        OnEnter.Invoke();
    }

    //Detect when Cursor leaves the GameObject
    public virtual void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.DOKill();
        transform.DOScale(Origin, 0.1f).SetEase(Easing);
        OnExit.Invoke();
    }
}
