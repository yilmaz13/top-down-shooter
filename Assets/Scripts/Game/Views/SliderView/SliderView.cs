using DG.Tweening;
using TMPro;
using UnityEngine;

public class SliderView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sliderInside;
    [SerializeField] private SpriteRenderer _sliderOutside;

    [SerializeField] private TextMeshPro _valueText;
    [SerializeField] private TextMeshPro _maxValueText;

    public void SetSliderValue(float ration)
    {
        _sliderInside.transform.DOScaleX(ration, 0.1f);      
    }  

    public void Initialize(Color sliderInsideColor)
    {       
        SetSliderValue(1);
        _sliderInside.color = sliderInsideColor;
        Show();
    }

    public void UpdateValue(float value, float maxValue)
    {
        if (value == 0)       
            Hide();        
        else        
            SetSliderValue(value / maxValue);
    }

    public void TrackParent(Vector3 vector3)
    {
        transform.position = vector3;
    }

    public void LookAtPosition(Transform pos)
    {
        if (pos == null)
        {
            Debug.LogWarning("SliderView: target is null.");
            return;
        }

        var lookDirection = pos.transform.position;
        lookDirection.y = transform.position.y;
        transform.LookAt(lookDirection, Vector3.up);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}
