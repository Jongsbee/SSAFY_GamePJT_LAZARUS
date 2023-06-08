using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class FieldViewAngle : MonoBehaviour
{
    private float currentviewDistance;
    [SerializeField] public float viewDistance;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float viewAngle;

    public bool lookFood = false;
    public bool lookPrey = false;
    public bool lookPredator = false;
    public bool lookPlayer = false;

        private ThirdPersonControllerForSurvive targetPlayer;
    private NavMeshAgent nav;
    private Transform targetFood;
    private Animal theAnimal;
    private Animal targetAnimal;

    void Start()
    {   
        nav = GetComponent<NavMeshAgent>();
        theAnimal = GetComponent<Animal>();
        currentviewDistance = viewDistance;
    }

    public void SleepingView(bool _sleep)                               // 자는 동안 감지 범위 축소
    {
        if (_sleep)
        {
            currentviewDistance = viewDistance / 50;
        }
        else
        {
            currentviewDistance = viewDistance;
        }
    }

    public Vector3 GetTargetPos()                                       // 상태에 따라 플레이어나 동물의 좌표 리턴
    {
        if (lookPlayer)
        {
            try
            {
                return targetPlayer.transform.position;
            }
            catch
            {
                return transform.position;
            }
        }
        else
        {
            try
            {
                return targetAnimal.transform.position;
            }
            catch
            {
                return transform.position;
            }
        }
        
    }

    public Vector3 GetFoodPos()                                         // 발견한 타겟 음식의 좌표 리턴
    {
        return targetFood.transform.position;
    }

    
    public bool View()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);
        for (int i = 0; i < _target.Length; i++)                                                // Sphere 이내에 감지된 targetMash 레이어 오브젝트를 순회
        {
            Transform _targetTf = _target[i].transform;
            {
                Vector3 _direction = (_targetTf.position - transform.position).normalized;      // 위치와 각도 조절
                float _angle = Vector3.Angle(_direction, transform.forward);

                if (_angle < viewAngle * 0.5f)                                                  // 탐색 가능 범위 내에 있고
                {
                    RaycastHit _hit;                                                            // 탐색 가능 거리 내에 있을 떄
                    if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, currentviewDistance))
                    {                                                                           // 종류에 따라 targetAnimal, targetPlayer, targetFood 등에 할당하고 bool값 수정
                        if (_hit.transform.tag == "Player" && !StatusController.instance.isDead)
                        {
                            lookPredator = false;
                            lookPrey = false;
                            lookFood = false;
                            lookPlayer = true;
                            targetPlayer = _hit.transform.GetComponent<ThirdPersonControllerForSurvive>();
                            return true;
                        }
                        
                        else if (theAnimal.isPrey && _hit.transform.tag == "Animal" && _hit.transform.GetComponent<Animal>().isPredator)
                        {
                            lookPredator = true;
                            lookPrey = false;
                            lookFood = false;
                            lookPlayer = false;
                            targetAnimal = _hit.transform.GetComponent<Animal>();
                            return true;
                        }
                        else if (theAnimal.isPredator && _hit.transform.tag == "Animal")
                        {
                            Animal thatAnimal = _hit.transform.GetComponent<Animal>();
                            
                            if (!thatAnimal.isDead)
                            {
                                if (theAnimal.GetHerd() >= 0 && thatAnimal.GetHerd() >= 0 && (theAnimal.GetHerd() == thatAnimal.GetHerd()))
                                {
                                    return false;
                                }
                                //Debug.Log("먹잇감 발견");
                                lookPredator = false;
                                lookPrey = true;
                                lookFood = false;
                                lookPlayer = false;
                                targetAnimal = _hit.transform.GetComponent<Animal>();
                                return true;
                            }
                        }

                    }
                }
                if (_targetTf.tag == "Player" && _targetTf.gameObject.GetComponent<ThirdPersonControllerForSurvive>().GetSprint() && !StatusController.instance.isDead)
                    //캐릭터 근처에서 달리기 시 추격	
                    {
                        if (CalcPathLength(_targetTf.transform.position) <= viewDistance)                           // 실제 거리가 시야와 맞는지 여부
                        {
                            targetPlayer = _targetTf.gameObject.GetComponent<ThirdPersonControllerForSurvive>();
                            theAnimal.GetSleep(false);
                            lookPredator = false;
                            lookPrey = false;
                            lookFood = false;
                            lookPlayer = true;
                            return true;
                        }
                    }

                if (theAnimal.isPrey && theAnimal.isHungry && _targetTf.tag == "Grass")
                {
                    if (_targetTf.GetComponent<Grass>().currentHp > 1)
                    {
                        lookPredator = false;
                        lookPrey = false;
                        lookFood = true;
                        lookPlayer = false;
                        targetFood = _targetTf.GetComponent<Grass>().transform;
                        return true;
                    }
                }

                if (theAnimal.isPredator && theAnimal.isHungry && _targetTf.tag == "Animal")
                {
                    Animal _Animal = _targetTf.GetComponent<Animal>();
                    if (_Animal.isDead && _Animal.currentHp > 1)
                    {
                        lookPredator = false;
                        lookPrey = false;
                        lookFood = true;
                        lookPlayer = false;
                        targetAnimal = _Animal;
                        return true;
                    }
                }
            }
            
        }
        return false;
    }

    private float CalcPathLength(Vector3 _targetPos)                                                                // 거리계산 메소드
    {   
        if (!theAnimal.isDead && (nav != null))                                                                     
        {
            NavMeshPath _path = new NavMeshPath();
            nav.CalculatePath(_targetPos, _path);

            Vector3[] _wayPoiont = new Vector3[_path.corners.Length + 2];

            _wayPoiont[0] = transform.position;
            _wayPoiont[_path.corners.Length + 1] = _targetPos;                                                      // Path 양 끝에 오브젝트의 위치와 타겟 위치를 설정

            float _pathLength = 0;
            for (int i = 0; i < _path.corners.Length; i++)                                                          // 순회하며 거리 더하여 반환
            {
                _wayPoiont[i + 1] = _path.corners[i];
                _pathLength += Vector3.Distance(_wayPoiont[i], _wayPoiont[i + 1]);
            }
            return _pathLength;
        }
        return Mathf.Infinity;                                                                                      // 오브젝트가 죽었거나 nav가 파괴된 경우 맥스값 반환
    }
}
