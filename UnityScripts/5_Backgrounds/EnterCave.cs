using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCave : MonoBehaviour
{
    [SerializeField] private AudioClip caveBGM;
    private ZombiAndBGMControl BGMPlayer;
    private bool enterCave;

    void Start()
    {
        enterCave = false;
        BGMPlayer = FindObjectOfType<ZombiAndBGMControl>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (enterCave)
            {
                enterCave = !enterCave;
                BGMPlayer.isEnterSomeWhere = false;
                BGMPlayer.BGMChange();
            }
            else
            {
                enterCave = !enterCave;
                BGMPlayer.isEnterSomeWhere = true;
                BGMPlayer.BGMAudio.clip = caveBGM;
                BGMPlayer.BGMAudio.Play();
            }
        }
    }
}
