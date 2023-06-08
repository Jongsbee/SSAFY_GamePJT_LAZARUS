using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusSFXManager : MonoBehaviour
{

    AudioSource _staminaSFX;
    AudioSource _hungerSFX;
    AudioSource _hpSFX;
    

    [SerializeField] AudioClip[] statusSFXs;

    // Start is called before the first frame update
    void Start()
    {
        _staminaSFX = transform.GetChild(0).GetComponent<AudioSource>();                        // 소리가 겹치는 상황을 고려해 세 차일드오브젝트로 나눔
        _hungerSFX = transform.GetChild(1).GetComponent<AudioSource>();
        _hpSFX = transform.GetChild(2).GetComponent<AudioSource>();



        if (GameObject.FindWithTag("Player").gameObject.name.Contains("Man"))                   // 성별대로 SFX 할당
        {
            _staminaSFX.clip = statusSFXs[0];
            _staminaSFX.Play();
        }
        else
        {
            _staminaSFX.clip = statusSFXs[1];
            _staminaSFX.Play();
        }
        _staminaSFX.volume = 0f;

        _hpSFX.clip= statusSFXs[3];
        _hpSFX.volume = 0f;
        _hpSFX.Play();

        StartCoroutine(CheckHunger());
    }

    void Update()
    {
        if (!(StatusController.instance.isDead))                                                // StatusController에 따른 볼륨와 피치 조절
        {
            _staminaSFX.volume = (100 - StatusController.instance.GetCurrentSP()) / 200;
            _hpSFX.volume = 1 - StatusController.instance.GetCurrentHPRate();
            _hpSFX.pitch = 0.5f + (1 - StatusController.instance.GetCurrentHPRate());
        }
    }

    IEnumerator CheckHunger()
    {
        while (!(StatusController.instance.isDead))
        {
            yield return new WaitForSeconds(10f);
            if (StatusController.instance.GetCurrentHunger() <= 20f) _hungerSFX.PlayOneShot(statusSFXs[2]);
        }
    }
}
