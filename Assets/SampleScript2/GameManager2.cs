using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    [SerializeField] private Transform cubeTransform;//�����Transform���
    [SerializeField] private Transform cubeTransform1;//�����Transform���
    [SerializeField] private Transform cubeTransform2;//�����Transform���
    [SerializeField] private Transform cubeTransform3;//�����Transform���
    [SerializeField] private Transform cubeTransform4;//�����Transform���
    [SerializeField] private Transform cubeTransform5;//�����Transform���


    private Vector3 startLocation = new Vector3(21.906f, 18.93f, 3.837f); // ��ʼλ��

    private Vector3 endLocation = new Vector3(21.906f, 35.24f, 3.837f); // ����λ��

    float targetY = -4.7f;//ƽ̨����λ��


    public PlayerController2 playerController2;
/*���playerController2�ű�*/

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

      

 if(!playerController2.currentCube.GetComponent<Walkable>().movingGround)
        {
   if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int multiplier = Input.GetKey(KeyCode.RightArrow) ? 1 : -1;
            pivots[0].DOComplete();
            pivots[0].DORotate(new Vector3(0, 90 * multiplier, 0), .6f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
        }
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

     public  void  RisePivot()
    {
/*����Ŀ���ƶ�λ��*/
        Vector3 targetPosition1 = new Vector3(cubeTransform1.position.x, -19.69f, cubeTransform1.position.z);
        Vector3 targetPosition2 = new Vector3(cubeTransform2.position.x, -19.69f, cubeTransform2.position.z);
        Vector3 targetPosition3 = new Vector3(cubeTransform3.position.x, -19.69f, cubeTransform3.position.z);
        Vector3 targetPosition4 = new Vector3(cubeTransform4.position.x, -19.69f, cubeTransform4.position.z);
        Vector3 targetPosition5 = new Vector3(cubeTransform5.position.x, -19.69f, cubeTransform5.position.z);

        /*ʹ��Dotweenʵ�����ٻ�������*/
        cubeTransform1.DOMove(targetPosition1, 1.5f).SetEase(Ease.Linear).SetDelay(0.5f);
        cubeTransform2.DOMove(targetPosition2, 1.5f).SetEase(Ease.Linear).SetDelay(1f);
        cubeTransform3.DOMove(targetPosition3, 1.5f).SetEase(Ease.Linear).SetDelay(1.5f);
        cubeTransform4.DOMove(targetPosition4, 1.5f).SetEase(Ease.Linear).SetDelay(2f);
        cubeTransform5.DOMove(targetPosition5, 1.5f).SetEase(Ease.Linear).SetDelay(2.5f);


    }
    public  void RisePivot2()
    {

    }
    public void RisePivot3()
    {

    }
}

/*���л���*/


