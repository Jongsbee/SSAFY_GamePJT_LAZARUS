using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;

public class FieldViewAngle : MonoBehaviour
{
    [SerializeField]
    private float viewAngle;
    [SerializeField]
    private float viewDistance;
    [SerializeField]
    private LayerMask targetMask;

    private bool lookPrey = false;
    private bool lookPredator = false;

    //private PlayerController thePlayer;
    private ThirdPersonController thePlayer;
    private NavMeshAgent nav;
    [SerializeField]
    private Animal theAnimal;
    private Animal targetAnimal;

    void Start()
    {
        //thePlayer = FindObjectOfType<PlayerController>();
        thePlayer = FindObjectOfType<ThirdPersonController>();
        nav = GetComponent<NavMeshAgent>();
    }

    public Vector3 GetTargetPos()
    {   
        if (lookPrey || lookPredator)
        {
            if (targetAnimal != null)
            {
                return targetAnimal.transform.position;
            }
            
        }
        return thePlayer.transform.position;
    }

    private Vector3 BoundaryAngle(float _angle)
    {
        _angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle* Mathf.Deg2Rad));
    }

    public bool View()
    {
        Vector3 _leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 _rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);
        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            {
                Vector3 _direction = (_targetTf.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);

                if (_angle < viewAngle * 0.5f)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, viewDistance))
                    {
                        if (theAnimal.isPredetor && _hit.transform.tag == "Animal" 
                            && _hit.transform.GetComponent<Animal>().isPrey 
                            && (!_hit.transform.GetComponent<Animal>().isDead 
                            || (_hit.transform.GetComponent<Animal>().isDead && _hit.transform.GetComponent<Animal>().currentHp > 1)))
                        {
                            lookPrey = true;
                            targetAnimal = _hit.transform.GetComponent<Animal>();
                            return true;
                        }
                        else if (_hit.transform.tag == "Animal" && _hit.transform.GetComponent<Animal>().isPredetor)
                        {
                            lookPredator = true;
                            targetAnimal = _hit.transform.GetComponent<Animal>();
                            return true;
                        }
                        else if (_hit.transform.tag == "Player")
                        {
                            Debug.Log("앗 사람이다!");
                            lookPredator = false;
                            lookPrey = false;
                            return true;
                        }
                    }
                }
            }
             //캐릭터 근처에서 달리기 시 추격
            if (thePlayer.GetSprint())
            {
                if (CalcPathLength(thePlayer.transform.position) <= viewDistance)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private float CalcPathLength(Vector3 _targetPos)
    {
        NavMeshPath _path = new NavMeshPath();
        nav.CalculatePath(_targetPos, _path);

        Vector3[] _wayPoiont = new Vector3[_path.corners.Length + 2];

        _wayPoiont[0] = transform.position;
        _wayPoiont[_path.corners.Length + 1] = _targetPos;

        float _pathLength = 0;
        for (int i = 0; i < _path.corners.Length; i++)
        {
            _wayPoiont[i + 1] = _path.corners[i]; // 웨이포인트에 경로 넣기
            _pathLength += Vector3.Distance(_wayPoiont[i], _wayPoiont[i + 1]); // 경로 길이 계산
        }
        return _pathLength;
    }
}
