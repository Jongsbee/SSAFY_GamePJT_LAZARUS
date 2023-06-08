using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habitatant : MonoBehaviour
{
    Animal thisAnimal;

    [SerializeField] private int animalRole = 3;
    [SerializeField] private bool isMinionHabitat;

    [SerializeField] private float habitatRange;
    [SerializeField] private bool inHabitat = true;
    [SerializeField] private Vector3 habitat;


    private void Start()
    {
        thisAnimal= GetComponent<Animal>();
        if (thisAnimal.isPredator)
        {
            animalRole = 0;
        }
        else if  (thisAnimal.isPrey)
        {
            animalRole = 1;
        }
        else 
        {
            animalRole = 2;
        }

        if (thisAnimal.isMinion) 
        {
            isMinionHabitat = true;
            habitatRange = GetComponent<Minion>().minionHerdRange;
        }
        else
        {
            habitat = transform.position;
        }    
        
    }

    private void Update()                                   // habitat으로부터 habitatRange만큼 떨어졌는지 판단
    {
        
        if (inHabitat)
        {
            if (isMinionHabitat && !GetComponent<Minion>().bossDead)
            {
                habitat = GetComponent<Minion>().thisBoss.transform.position;
                if (Vector3.Distance(transform.position, GetComponent<Minion>().thisBoss.transform.position) >= habitatRange)
                {
                    inHabitat = false;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, habitat) >= habitatRange)
                {
                    inHabitat = false;
                }
            }
        }
        else
        {
            BackHome();
            CheckHome();
        }
    }

    private void CheckHome()                                // minion의 경우 25f, 거주지역의 경우 1f 이내 접근 시 Inhabitat 상태 변경
    {
        if (!inHabitat)
        {
            if (isMinionHabitat)
            {
                if (Vector3.Distance(transform.position, habitat) <= 25f)
                {
                    inHabitat = true;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, habitat) <= 1f)
                {
                    inHabitat = true;
                    habitat = transform.position;
                }
            }
        }       
    }

    private void BackHome()                                 // habitat으로 위치 설정 후 이동
    {
        StopAllCoroutines();
        thisAnimal.Recovery();

        switch (animalRole)
        {
            case 0:
                GetComponent<Predator>().Chase(habitat);
                break;
            case 1:
                GetComponent<Prey>().BackToBoss(habitat);
                break;
            case 2:
                GetComponent<Neutral>().Chase(habitat);
                break;
            case 3:
                GetComponent<Monster>().Chase(habitat);
                break;
        }
    }
}
