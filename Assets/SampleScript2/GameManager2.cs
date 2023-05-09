using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    [SerializeField] private Transform cubeTransform;//�����Transform���
    private Vector3 startLocation = new Vector3(21.906f, 18.93f, 3.837f); // ��ʼλ��

    private Vector3 endLocation = new Vector3(21.906f, 35.24f, 3.837f); // ����λ��

    float targetY = -4.7f;//ƽ̨����λ��

    private float moveDistance = 16.31f;
    private float moveDuration = 3f;

    public static GameManager2 instance;

    /*���*/
    public PlayerController player;

    /*�����ƶ���·��*/
    public List<PathCondition> pathConditions = new List<PathCondition>();
    /*�����任*/
    public List<Transform> pivots;
    /*���صķ���*/

    private void Awake()
    {
        instance = this;
    }



    // Update is called once per frame
    void Update()
    {
        foreach (PathCondition pc in pathConditions)
        {
            int count = 0;
            for (int i = 0; i < pc.conditions.Count; i++)
            {
             
             /*   if (pc.conditions[i].conditionObject.eulerAngles == pc.conditions[i].eulerAngle)
                {*/

/*ŷ����Ϊʲô��һ��*/
                  if (pc.conditions[i].conditionObject.eulerAngles== pc.conditions[i].eulerAngle)
                { 
                    count++;  
                    //Debug.Log("Count2=" + count);
                }
            }
            foreach (SinglePath sp in pc.paths)
                sp.block.possiblePaths[sp.index].active = (count == pc.conditions.Count);
        }

        if (player.walking)
            return;

        /* rotateCube����ת*/
/*����playerλ���ض�����ʱ����ʵ�ָù���*/
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int multiplier = Input.GetKey(KeyCode.RightArrow) ? 1 : -1;
            pivots[0].DOComplete();
            pivots[0].DORotate(new Vector3(0, 90 * multiplier, 0), .6f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
        }


        /*ƽ�������*/


     

    }
    public  void RotatePivot()
    {
        /*ƽ̨����*/


        /*ʹ��DOTWeen API���ö���*/

        /*  cubeTransform.DOMoveY(cubeTransform.position.y+moveDistance,moveDuration).SetEase(Ease.Linear);*/
        cubeTransform.DOMoveY(targetY, moveDuration).SetEase(Ease.Linear);/*ʹ��ƽ̨�ƶ����̶�λ��*/


    
}

}

/*���л���*/


