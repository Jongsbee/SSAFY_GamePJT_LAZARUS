    using System.Collections.Generic;
using UnityEngine;

public class ZombiAndBGMControl : MonoBehaviour
{
    [SerializeField] public bool isEnterSomeWhere;
    [SerializeField] public bool isDay;
    [SerializeField] public bool currentDay;
    [SerializeField] List<Animal> allAnimals;
    [SerializeField] List<Zombi> allZombies;

    private int zombiCnt = 2;
    private int zombiKill = 0;

    public AudioSource BGMAudio;
    [SerializeField] protected AudioClip[] Alarms;
    [SerializeField] protected AudioClip[] dayTimes;
    [SerializeField] protected AudioClip[] nightTimes;

    private void Start()
    {
        BGMAudio = GetComponent<AudioSource>();
        foreach (Animal _animal in FindObjectsOfType(typeof(Animal)))
        {
            allAnimals.Add(_animal);
        }
        isDay = true;
        isEnterSomeWhere = false;
        currentDay = isDay;
    }

    public void DayNightChage()
    {
        isDay = !isDay;
        ZombiRise();
        if (!isEnterSomeWhere)
        {
            BGMChange();
        }
    }

    private void ZombiRise()                                        // 좀비 순회하며 활성화/비활성화 메소드 실행
    {
        if (currentDay != isDay)
        {
            if (currentDay)
            {
                BGMAudio.PlayOneShot(Alarms[1]);
                for (int i = 0; i < zombiCnt; i++)
                {
                    allZombies[i].gameObject.SetActive(true);
                    allZombies[i].GetComponent<Zombi>().Rise();
                }
            }
            else
            {
                BGMAudio.PlayOneShot(Alarms[0]);
                for (int i = 0; i < zombiCnt; i++)
                {
                    allZombies[i].gameObject.SetActive(true);
                    allZombies[i].GetComponent<Zombi>().GetDead();
                }
            }

            foreach (Animal _animal in allAnimals)                  //  동물 순회하며 수면/기상 메소드실행
            {
                if (_animal.gameObject.GetComponent<Wolf>() != null)
                {
                    Wolf wolf = _animal.gameObject.GetComponent<Wolf>();
                    wolf.GetSleep(!currentDay);
                }
                else if (_animal.gameObject.GetComponent<Monster>() != null)
                {
                    continue;
                }
                else
                {
                    _animal.GetSleep(currentDay);
                }
            }
            currentDay = isDay;
        }
    }

    public void BGMChange()                                             // 배경음악 리스트 중에 랜덤 재생
    {
        if (isDay) 
        {
            int _cnt = dayTimes.Length;
            int _random = Random.Range(0, _cnt);
            BGMAudio.clip = dayTimes[_random];
            BGMAudio.Play();
            BGMAudio.volume = 0.3f;
        }
        else
        {
            int _cnt = nightTimes.Length;
            int _random = Random.Range(0, _cnt);
            BGMAudio.clip = nightTimes[_random];
            BGMAudio.Play();
            BGMAudio.volume = 0.15f;
        }
    }

    public void SetKillCount()                                          // 좀비 깨우는 횟수 카운트
    {
        zombiKill++;
        if ((zombiKill >= ((zombiCnt + 1) * (zombiCnt + 1))) && zombiCnt < 10)
        {
            zombiCnt++;
        }
    }
}
