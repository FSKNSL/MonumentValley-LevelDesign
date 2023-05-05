using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

[SelectionBase]
public class PlayerController2 : MonoBehaviour
{
    public bool walking = false;

    [Space]

    public Transform currentCube;
    public Transform clickedCube;
    public Transform indicator;

    [Space]

    public List<Transform> finalPath = new List<Transform>();

    private float blend;

    void Start()
    {
        RayCastDown();
    }

    void Update()
    {

        //GET CURRENT CUBE (UNDER PLAYER)

        RayCastDown();

        if (currentCube.GetComponent<Walkable>().movingGround)
        {
            transform.parent = currentCube.parent;
        }
        else
        {
            transform.parent = null;
        }

        // CLICK ON CUBE

        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit mouseHit;

            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                if (mouseHit.transform.GetComponent<Walkable>() != null)
                {
                    clickedCube = mouseHit.transform;
                    DOTween.Kill(gameObject.transform);
                    finalPath.Clear();
                    FindPath();

                    blend = transform.position.y - clickedCube.position.y > 0 ? -1 : 1;

                    indicator.position = mouseHit.transform.GetComponent<Walkable>().GetWalkPoint();
                    Sequence s = DOTween.Sequence();
                    s.AppendCallback(() => indicator.GetComponentInChildren<ParticleSystem>().Play());
                    s.Append(indicator.GetComponent<Renderer>().material.DOColor(Color.white, .1f));
                    s.Append(indicator.GetComponent<Renderer>().material.DOColor(Color.black, .3f).SetDelay(.2f));
                    s.Append(indicator.GetComponent<Renderer>().material.DOColor(Color.clear, .3f));

                }
            }
        }
    }

    void FindPath()
    {
        /*����·�����ܺ���*/

        List<Transform> nextCubes = new List<Transform>();
        List<Transform> pastCubes = new List<Transform>();

        /*foreachѭ�������оٳ������е�����Ԫ�أ�foreach����б��ʽ�ɹؼ���in����*/

        foreach (WalkPath path in currentCube.GetComponent<Walkable>().possiblePaths)
        {
            /*in�ұ���Ϊ��������in������Ǳ����������ڴ�Ÿü����е�ÿһ��Ԫ��*/
            if (path.active)
            {
                /*���cube�����ƶ�·����������ӵ�nextCubes������*/
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = currentCube;
            }
        }

        /*���currentCube��nextCubes�б�*/
        pastCubes.Add(currentCube);

        ExploreCube(nextCubes, pastCubes);
        BuildPath();
    }


    /*������е�nextcube����Ԫ��*/

    void ExploreCube(List<Transform> nextCubes, List<Transform> visitedCubes)
    {
        Transform current = nextCubes.First();
        nextCubes.Remove(current);

        if (current == clickedCube)
        {
            return;
        }
        /*���walkable�ű��Ƿ��Ѿ������ʹ���������ʹ��Ļ������������·��*/
        /*�����δ���ʣ�������ӵ�nextCube�б�������������*/
        foreach (WalkPath path in current.GetComponent<Walkable>().possiblePaths)
        {
            if (!visitedCubes.Contains(path.target) && path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = current;
            }
        }

        visitedCubes.Add(current);

        /*�ж�nextCubes�����Ƿ����Ԫ��*/
        if (nextCubes.Any())
        {
            ExploreCube(nextCubes, visitedCubes);
        }
    }

    void BuildPath()
    {
        Transform cube = clickedCube;
        while (cube != currentCube)
        {
            finalPath.Add(cube);
            if (cube.GetComponent<Walkable>().previousBlock != null)
                cube = cube.GetComponent<Walkable>().previousBlock;
            else
                return;
        }

        finalPath.Insert(0, clickedCube);

        FollowPath();
    }

    void FollowPath()
    {
        Sequence s = DOTween.Sequence();

        walking = true;

        /*��˳��Player�ƶ���ÿ�����������*/
        for (int i = finalPath.Count - 1; i > 0; i--)
        {
            float time = finalPath[i].GetComponent<Walkable>().isStair ? 1.5f : 1;

            /*ʹ�����Ի����˶�ʹ������һ���޷�Ĺ���*/
            s.Append(transform.DOMove(finalPath[i].GetComponent<Walkable>().GetWalkPoint(), .2f * time).SetEase(Ease.Linear));

            if (!finalPath[i].GetComponent<Walkable>().dontRotate)
                s.Join(transform.DOLookAt(finalPath[i].position, .1f, AxisConstraint.Y, Vector3.up));
        }

        /*��ť�����¼�*/

        if (clickedCube.GetComponent<Walkable>().isButton)
        {
            /*DOTWEEN ���϶���SEQUENCE��ʹ��*/

/*�����isButton�滻Ϊ�Լ��ļ���*/
            s.AppendCallback(() => GameManager2.instance.RotatePivot());
        }

        s.AppendCallback(() => Clear());
    }

    void Clear()
    {
        foreach (Transform t in finalPath)
        {
            t.GetComponent<Walkable>().previousBlock = null;
        }
        finalPath.Clear();
        walking = false;
    }

    public void RayCastDown()
    {
        /*ͨ��Ͷ�����飬�����õ�ǰ�����������������������������*/
        Ray playerRay = new Ray(transform.GetChild(0).position, -transform.up);

        /* RaycastHit�����ڴ洢�������ߺ��������ײ��Ϣ*/
        RaycastHit playerHit;



        if (Physics.Raycast(playerRay, out playerHit))
        {
            if (playerHit.transform.GetComponent<Walkable>() != null)
            {
                currentCube = playerHit.transform;

                if (playerHit.transform.GetComponent<Walkable>().isStair)
                {
                    /*���庯���÷�*/

                    /*����¥�ݺ�ƽ����˶�����*/
                    DOVirtual.Float(GetBlend(), blend, .1f, SetBlend);
                }
                else
                {
                    DOVirtual.Float(GetBlend(), 0, .1f, SetBlend);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Ray ray = new Ray(transform.GetChild(0).position, -transform.up);
        Gizmos.DrawRay(ray);
    }

    float GetBlend()
    {       /*��ȡ��Ϸ������ָ��������ֵ*/
        return GetComponentInChildren<Animator>().GetFloat("Blend");
    }
    void SetBlend(float x)
    {
        /*������ֵ�ŵ���������Ӱ�����*/
        GetComponentInChildren<Animator>().SetFloat("Blend", x);
    }

}
