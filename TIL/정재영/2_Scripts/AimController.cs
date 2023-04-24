using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimController : MonoBehaviour
{
    public Transform aimTarget; // Ȱ ���� ��ġ�� ��Ÿ���� ������Ʈ
    public Transform lookTarget; // ĳ���Ͱ� �ٶ� ��ġ�� ��Ÿ���� ������Ʈ
    public float maxDistance = 100f; // ����ĳ��Ʈ�� Ž���� �ִ� �Ÿ�
    public float rotateSpeed = 5f; // ȸ�� �ӵ�
    public GameObject player;

    public CinemachineVirtualCamera virtualCamera; // �ó׸ӽ� ���� ī�޶�
    private RaycastHit hitInfo; // ����ĳ��Ʈ���� ��ȯ�� ����

    

    //Ȱ ��� ����, ĳ���� ȸ�� ����
    Vector3 lookDirection;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.Find("Player_Test");
    }

    private void Update()
    {
        // ����ĳ��Ʈ�� ���� ���� ��ġ ���
        if (Physics.Raycast(virtualCamera.transform.position, virtualCamera.transform.forward, out hitInfo, maxDistance))
        {
            aimTarget.position = hitInfo.point;
            //Debug.Log(aimTarget.position);
        }

        // ĳ���� ȸ������ ����Ͽ� ����
        lookDirection = aimTarget.position - transform.position;
        lookDirection.y = 0f;
        Quaternion Local_NewRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotateSpeed * Time.deltaTime);
        player.transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x, Local_NewRotation.eulerAngles.y, player.transform.localEulerAngles.z);
    }


   
}