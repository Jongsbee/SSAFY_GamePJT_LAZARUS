using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimController : MonoBehaviour
{
    public Transform aimTarget; // 발사체가 부딫힐 위치
    public float maxDistance = 100f; 
    public float rotateSpeed = 5f; 
    public GameObject player;

    public CinemachineVirtualCamera virtualCamera; 
    private RaycastHit hitInfo; // 충돌한 오브젝트를 가져온다.

    
    
    // 방향 벡터, 플레이어로부터 실제 발사되는 방향.
    Vector3 lookDirection;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // 카메라 중앙에서 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       
        // ray가 maxDistance 거리에 다른 GameObject가 잡히면 true
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance))
        {
            aimTarget.position = hitInfo.point;
        }
        else // ray에 다른 GameObject가 잡히지 않으면 aimTarget의 위치를 카메라의 위치에서 정방향으로 maxDistance 거리만큼
        {
            aimTarget.position = virtualCamera.transform.position + virtualCamera.transform.forward * maxDistance;
        }

        // 방향 벡터를 구하기 위해 y축은 0 으로 간다.
        lookDirection = aimTarget.position - transform.position;
        lookDirection.y = 0f;
        //플레이어의 로테이션 값도 같이 바꿔준다.
        Quaternion Local_NewRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotateSpeed * Time.deltaTime);
        player.transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x, Local_NewRotation.eulerAngles.y, player.transform.localEulerAngles.z);
    }


   
}