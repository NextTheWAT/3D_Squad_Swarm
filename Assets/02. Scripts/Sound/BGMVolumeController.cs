using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BGMVolumeController : MonoBehaviour
{
    public AudioMixer mixer; // 인스펙터에서 믹서 연결
    public Slider slider; // 인스펙터에서 슬라이더 연결
    public string parameterName = "BGMVolume"; // 노출된 파라미터 이름

    private void Start()
    {
        // 슬라이더 초기값 설정
        float volume;
        if (mixer.GetFloat(parameterName, out volume))
        {
            slider.value = Mathf.Pow(10, volume / 20); // 오디오 믹서 볼륨은 로그 스케일
        }

        // 슬라이더 값 변경 시 볼륨 조절 함수 호출
        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        // 볼륨 값(선형)을 오디오 믹서에 맞는 로그 스케일로 변환
        mixer.SetFloat(parameterName, Mathf.Log10(value) * 20);
    }
}