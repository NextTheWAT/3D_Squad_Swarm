using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroTimeline : MonoBehaviour
{
    private PlayableDirector director;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();

        // ������ �� UI ���¸� None���� �����ؼ� ��� UI�� ��Ȱ��ȭ
        UIManager.Instance.ChangeState(UIState.None);
    }

    void OnEnable()
    {
        // ��ũ��Ʈ�� Ȱ��ȭ�� �� �̺�Ʈ ���
        director.stopped += OnTimelineFinished;
    }

    void OnDisable()
    {
        // ��ũ��Ʈ�� ��Ȱ��ȭ�� �� �̺�Ʈ ����
        director.stopped -= OnTimelineFinished;
    }

    // Ÿ�Ӷ����� ������ �� ȣ��Ǵ� �޼���
    private void OnTimelineFinished(PlayableDirector director)
    {
        SceneManager.LoadScene(2); // Test : Ÿ�Ӿ� ���ӿ����� �ҷ�����

        // UI�Ŵ����� ���� �� �������� �ε����� ������
        //int sceneNumber = UIManager.Instance.SelectedStageIndex;

        //switch (sceneNumber)
        //{
        //    case 1:
        //        Debug.Log("�������� 1 ����");
        //        // SceneManager.LoadScene("Stage1Scene");
        //        break;
        //    case 2:
        //        Debug.Log("�������� 2 ����");
        //        // SceneManager.LoadScene("Stage2Scene");
        //        break;
        //    case 3:
        //        Debug.Log("�������� 3 ����");
        //        // SceneManager.LoadScene("Stage3Scene");
        //        break;
        //}
    }
}
