using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScript : MonoBehaviour
{
    public string sceneName; // ��������

    public void OnClick()
    {
        // ʹ��DoTweenʵ�ֳ����л�
        DOTween.Sequence().Append(transform.DOScale(Vector3.zero, 0.5f)).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
}
