
using UnityEngine;

public class EnterLab : MonoBehaviour
{
    [SerializeField] private AudioClip labBGM;
    private ZombiAndBGMControl BGMPlayer;
    private bool enterLab;

    void Start()
    {
        enterLab = false;
        BGMPlayer = FindObjectOfType<ZombiAndBGMControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enterLab = !enterLab;
            BGMPlayer.isEnterSomeWhere = true;
            BGMPlayer.BGMAudio.clip = labBGM;
            BGMPlayer.BGMAudio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enterLab = !enterLab;
            BGMPlayer.isEnterSomeWhere = false;
            BGMPlayer.BGMChange();
        }
    }

}
