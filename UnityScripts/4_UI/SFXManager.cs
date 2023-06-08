using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private AudioSource UISFX;                          // 각 효과음 리스트
    [SerializeField] private AudioClip[] UISFXClip;
    [SerializeField] private AudioClip[] EventSFXClip;
    [SerializeField] private AudioClip[] BowDrawClip;
    [SerializeField] private AudioClip[] BowShootClip;
    [SerializeField] private AudioClip[] GunFineClip;
    [SerializeField] private AudioClip[] GunShootClip;
    [SerializeField] private AudioClip[] HandSFXClip;
    [SerializeField] private AudioClip[] AxeSFXClip;
    [SerializeField] private AudioClip[] PickaxeSFXClip;


    private void Awake()                                // 할당
    {
        UISFX = GetComponent<AudioSource>();
    }


    public void WeaponUsingSFX(int _index)
    {
        switch (_index)
        {
            case -1:
                //RandomWeaponSFX(HandSFXClip);
                break;

            case 0:
                RandomWeaponSFX(PickaxeSFXClip);
                break;

            case 1:
                RandomWeaponSFX(AxeSFXClip);
                break;

            case 2:
                RandomWeaponSFX(BowShootClip);
                break;

            case 3:
                break;
        }
    }           // 무기 사용 시 ThirdPersonControllerForSurvive의 equipItemIndex에 따른 효과음 재생.

    public void WeaponDrawSFX(int _index)
    {
        switch (_index)
        {
            case 2:
                RandomWeaponSFX(BowDrawClip);
                break;

            case 3:
                RandomWeaponSFX(GunFineClip);
                break;

        }
    }            // 활과 총기류 조준 시 ThirdPersonControllerForSurvive의 equipItemIndex에 따른 효과음 재생.

    public void ConsumeSFX(int _index)
    {
        if (_index >= 30 || _index < 60)
        {
            if (_index >= 40 && _index < 60)
            {
                PlaySFX("EatBBQ");
            }
            else
            {
                PlaySFX("EatElse");
            }
        }
        else if (_index < 200 || _index >= 300) PickItem(_index);
        else
        {
            if (_index >= 200 && _index < 210)
            {
                PlaySFX("EatBBQ");
            }
            else if (_index >= 210 && _index < 220)
            {
                PlaySFX("EatSoup");
            }
        }
    }               // 아이템 사용 시 ItemNameIndex에 따른 효과음 재생

    private void RandomWeaponSFX(AudioClip[] _clips)
    {
        int _cnt = _clips.Length;
        int _random = Random.Range(0, _cnt);
        PlaySE(_clips[_random], false);
    } // 효과음 랜덤 재생

    public void PickItem(int _index)
    {
        if (_index >= 0 && _index < 10)
        {
            PlaySFX("PickWood");
        }
        else if (_index >= 10 && _index < 20)
        {
            PlaySFX("PickStone");
        }
        else if (_index >= 30 && _index < 40)
        {
            PlaySFX("PickMushroom");
        }
        else if (_index >= 40 && _index < 50)
        {
            PlaySFX("PickMeat");
        }
        else if (_index >= 60 && _index < 70)
        {
            PlaySFX("PickItem");
        }
        else if (_index >= 70 && _index < 80)
        {
            PlaySFX("PickCrystal");
        }
        else if (_index >= 100 && _index < 140)
        {
            PlaySFX("PickWeapon");
        }
        else if (_index >= 140 && _index < 150)
        {
            PlaySFX("PickGun");
        }
        else if (_index >= 200 && _index < 210)
        {
            PlaySFX("PickBBQ");
        }
        else if (_index >= 210 && _index < 220)
        {
            PlaySFX("PickSoup");
        }
        else
        {
            PlaySFX("JungHaeSuk");
        }
    }                 // ItemNameIndex에 따른 효과음 재생

    public void Event(int _index)
    {
        switch (_index)
        {
            case 11:
                PlaySE(EventSFXClip[1], false);
                break;

            case 16:
                PlaySE(EventSFXClip[2], false);
                break;

            case 17:
                PlaySE(EventSFXClip[3], false);
                break;

            case 18:
                PlaySE(EventSFXClip[4], false);
                break;

            case 19:
                PlaySE(EventSFXClip[5], false);
                break;

            case 20:
                PlaySE(EventSFXClip[6], false);
                break;

            case 21:
                PlaySE(EventSFXClip[7], false);
                break;

            case 25:
                break;

            case 30:
                PlaySE(EventSFXClip[8], false);
                break;

            default:
                PlaySE(EventSFXClip[0], false);
                break;
        }
    }                    // 이벤트 index에 따른 효과음 재생

    public void PlaySFX(string _sfxText)
    {
        //Debug.Log(_sfxText);
        switch ( _sfxText ) 
        {
            case "UIOpen":
                PlaySE(UISFXClip[0], false);
                break;

            case "UIClose":
                PlaySE(UISFXClip[1], false);
                break;

            case "MakeItem":
                PlaySE(UISFXClip[2], false);
                break;

            case "MakeWeapon":
                PlaySE(UISFXClip[3], false);
                break;

            case "PickMeat":
                PlaySE(UISFXClip[4], false);
                break;

            case "PickStone":
                PlaySE(UISFXClip[5], false);
                break;

            case "PickWood":
                PlaySE(UISFXClip[6], false);
                break;

            case "PickItem":
                PlaySE(UISFXClip[7], false);
                break;

            case "QuickSlot":
                PlaySE(UISFXClip[8], false);
                break;

            case "Hurt":
                PlaySE(UISFXClip[9], true);
                break;

            case "Stamina_Male":
                PlaySE(UISFXClip[10], true);
                break;

            case "Stamina_Female":
                PlaySE(UISFXClip[11], true);
                break;

            case "Hungry":
                PlaySE(UISFXClip[12], false);
                break;

            case "PickMushroom":
                PlaySE(UISFXClip[13], false);
                break;

            case "PickCrystal":
                PlaySE(UISFXClip[14], false);
                break;

            case "PickWeapon":
                PlaySE(UISFXClip[15], false);
                break;

            case "PickGun":
                PlaySE(UISFXClip[16], false);
                break;

            case "PickBBQ":
                PlaySE(UISFXClip[17], false);
                break;

            case "PickSoup":
                PlaySE(UISFXClip[18], false);
                break;

            case "Quest_0":
                PlaySE(UISFXClip[19], false);
                break;

            case "JungHaeSuk":
                PlaySE(UISFXClip[20], false);
                break;

            case "EatBBQ":
                PlaySE(UISFXClip[21], false);
                break;

            case "EatSoup":
                PlaySE(UISFXClip[22], false);
                break;

            case "EatElse":
                PlaySE(UISFXClip[23], false);
                break;
        }
    }             // 부여한 index에 따른 효과음 재생

    private void PlaySE(AudioClip _clip, bool _loop)
    {
        if (_loop)
        {
            UISFX.clip = _clip;
            UISFX.Play();
        }
        else
        {
            UISFX.PlayOneShot(_clip);
        }
       
    } // 효과음 재생
}
