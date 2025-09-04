using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BGMVolumeController : MonoBehaviour
{
    public AudioMixer mixer; // �ν����Ϳ��� �ͼ� ����
    public Slider slider; // �ν����Ϳ��� �����̴� ����
    public string parameterName = "BGMVolume"; // ����� �Ķ���� �̸�

    private void Start()
    {
        // �����̴� �ʱⰪ ����
        float volume;
        if (mixer.GetFloat(parameterName, out volume))
        {
            slider.value = Mathf.Pow(10, volume / 20); // ����� �ͼ� ������ �α� ������
        }

        // �����̴� �� ���� �� ���� ���� �Լ� ȣ��
        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        if (value <= 0.0001f) // 0 ����
        {
            mixer.SetFloat(parameterName, -80f); // ���� ó��
        }
        else
        {
            mixer.SetFloat(parameterName, Mathf.Log10(value) * 20f);
        }
    }
}