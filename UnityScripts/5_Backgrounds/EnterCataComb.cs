using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCataComb : MonoBehaviour
{
    [SerializeField] private AudioClip catacombBGM;
    private ZombiAndBGMControl BGMPlayer;
    private bool enterCatacomb;

    void Start()
    {
        enterCatacomb = false;
        BGMPlayer = FindObjectOfType<ZombiAndBGMControl>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (enterCatacomb)
            {
                enterCatacomb = !enterCatacomb;
                BGMPlayer.isEnterSomeWhere = false;
                BGMPlayer.BGMChange();
            }
            else
            {
                enterCatacomb = !enterCatacomb;
                BGMPlayer.isEnterSomeWhere = true;
                BGMPlayer.BGMAudio.clip = catacombBGM;
                BGMPlayer.BGMAudio.Play();
            }
        }
    }
}
