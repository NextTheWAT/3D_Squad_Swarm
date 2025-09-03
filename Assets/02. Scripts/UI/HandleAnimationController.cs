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
        // �����̴� ��(0~1)�� �ִϸ��̼��� normalizedTime���� ����
        handleAnimator.Play("HandleAnimation", -1, value);
    }
}