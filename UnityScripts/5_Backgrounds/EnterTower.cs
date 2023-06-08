using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTower : MonoBehaviour
{
    [SerializeField] private AudioClip towerBGM;
    private ZombiAndBGMControl BGMPlayer;
    private bool enterTower;

    void Start()
    {
        enterTower = false;
        BGMPlayer = FindObjectOfType<ZombiAndBGMControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("타워 진입");
            enterTower = !enterTower;
            BGMPlayer.isEnterSomeWhere = true;
            BGMPlayer.BGMAudio.clip = towerBGM;
            BGMPlayer.BGMAudio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("타워 탈출");
            enterTower = !enterTower;
            BGMPlayer.isEnterSomeWhere = false;
            BGMPlayer.BGMChange();
        }
    }

}
