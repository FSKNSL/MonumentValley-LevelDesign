using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ChapterScript : MonoBehaviour
{
    [Tooltip("��ת��")]
    public Transform TowerObject;
    [Tooltip("��������")]
    public float floatingDistance = 0.1f;
    [Tooltip("����ʱ��")]
    public float floatingTime = 1f;
    // Start is called before the first frame update
    void Start()
    {

        //���������ϸ�������ָ��ʱ�������
        TowerObject.DOMoveY(TowerObject.position.y + floatingDistance, floatingTime).SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        //���������¸�������ָ��ʱ�������
        TowerObject.DOMoveY(TowerObject.position.y - floatingDistance, floatingTime).SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        //���������󸡶�����ָ��ʱ�������
        TowerObject.DOMoveX(TowerObject.position.x - floatingDistance, floatingTime).SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        //���������Ҹ�������ָ��ʱ�������
        TowerObject.DOMoveX(TowerObject.position.x + floatingDistance, floatingTime).SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        //ʵ����ת���������һ���Ч��




        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            int multiplier = Input.GetKey(KeyCode.A) ? 1 : -1;
            TowerObject.DOComplete();
            TowerObject.DORotate(new Vector3(0, 90 * multiplier, 0), .6f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
        }

    }
}
