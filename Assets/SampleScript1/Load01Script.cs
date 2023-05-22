using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Load01Script : MonoBehaviour
{
    public TMP_Text text;
    public GameObject button;
    public GameObject button2;/*��˸*/


    void Start()
    {
        text.color = new Color(1f, 1f, 1f, 0f); // �������ֳ�ʼ͸����Ϊ0
        button.SetActive(false); // ���ذ�ť
        button2.SetActive(false);

        StartCoroutine(FadeIn()); // ��������   
        // ÿ��1��ִ��һ��
        InvokeRepeating("ToggleVisibility", 0f, 1f);

    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1f); // �ȴ�1��
        text.DOFade(1f, 1f); // ��������
        yield return new WaitForSeconds(1f); // �ȴ�1��
        button.SetActive(true); // ��ʾ��ť
        button2.SetActive(true);
    }
    private void ToggleVisibility()
    {
        // ��������ǿɼ��ģ���������
        if (button2.activeSelf)
        {
            button2.SetActive(false);
        }
        // ������������صģ�����ʾ��
        else
        {
            button2.SetActive(true);
        }
    }


    public void FadeOut()
    {
        button.SetActive(false); // ���ذ�ť
        button2.SetActive(false);
        CancelInvoke("ToggleVisibility");/*ֹͣ����*/
        text.DOFade(0f, 1f).OnComplete(() => { // �������֣����ڶ�������ʱִ�лص�����
            // �ڻص������лָ�����������ɫ
            Camera.main.backgroundColor = new Color(0.35f, 0.35f, 0.35f, 1f);
        });
        // �ڵ������ֵ�ͬʱ����������������ɫ
        Camera.main.DOColor(new Color(0f, 0f, 0f, 1f), 1f);


    }
}
