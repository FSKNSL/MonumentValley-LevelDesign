using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCanvas : MonoBehaviour
{
    public GameObject CanvasOn;//����򿪻���
    public GameObject CanvasOff;//����رջ���

    public void changeCanvas()//�����л������ķ���
    {
        CanvasOn.SetActive(true);//ʵ�ִ򿪻���
        CanvasOff.SetActive(false);//ʵ�ֹرջ���
    }
}