using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Returnscript : MonoBehaviour
{
    public string sceneName; // 场景名称

    public void OnClick()
    {
        // 使用DoTween实现场景切换
        DOTween.Sequence().Append(transform.DOScale(Vector3.zero, 0.5f)).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
}
