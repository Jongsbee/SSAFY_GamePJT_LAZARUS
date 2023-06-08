using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    // 이 스크립트는 장비에 따른 각각의 애니메이션마다 다른 처리를 위해 만든 스크립트이다.
    // 예를 들면 도끼, 근접, 활과 애니메이션에 따라 콜라이더 적용이 다르기 때문에 이렇게 정함

    public enum Type { Melee, Range, Item};
    public Type type;

    public float rate;
    public BoxCollider meleeArea; // 콜라이더 enable
    

    

    // 피격음
    public AudioClip audioHit;

    private void Awake()
    {
        //  TODO: 차이가 없을시 하나로 합치기
        if (type == Type.Melee)
        {
            meleeArea = GetComponent<BoxCollider>();
        }
        else if (type == Type.Range)
        {
            meleeArea = GetComponent<BoxCollider>();
        }

    }


   

    // 손을 장착했을 때 애니메이션
    public void Punch_Start_Animation()
    {
        meleeArea.enabled = true;
    }

    public void Punch_End_Animation()
    {
        meleeArea.enabled = false;
    }


    // 도끼를 장착했을 때 애니메이션
    public void Axe_Start_Animation()
    {
        meleeArea.enabled = true;
    }

    public void Axe_End_Animation()
    {
        meleeArea.enabled = false;
    }



}
