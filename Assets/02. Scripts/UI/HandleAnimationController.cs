using UnityEngine;
using UnityEngine.UI;

public class HandleAnimationController : MonoBehaviour
{
    public Slider slider;
    public Animator handleAnimator;

    private void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        // 슬라이더 값(0~1)을 애니메이션의 normalizedTime으로 설정
        handleAnimator.Play("HandleAnimation", -1, value);
    }
}