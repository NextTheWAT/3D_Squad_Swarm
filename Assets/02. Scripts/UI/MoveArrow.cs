using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrow : MonoBehaviour
{
    private Vector3 originalPosition;
    private float timer;

    private void OnEnable()
    {
        // 시작 시 현재 위치를 원래 위치로 저장
        originalPosition = transform.position;
        timer = 0f;
    }

    void Update()
    {
        // 타임스케일과 관계없이 시간을 누적하려면 아래와 같이 사용해야 합니다.
        timer += Time.unscaledDeltaTime;

        // 위쪽 방향으로 매 프레임 0.1씩 이동
        transform.position += Vector3.up * 0.03f;

        // 타이머가 3초를 초과하면
        if (timer >= 1f)
        {
            // 원래 위치로 되돌리기
            transform.position = originalPosition;
            timer = 0f;
        }
    }
}
