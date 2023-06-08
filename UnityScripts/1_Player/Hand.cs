using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hand : Equipments
{
    [SerializeField] private int TreeAttackDamage;
    [SerializeField] private int StoneAttackDamage;
    [SerializeField] private int AnimalAttackDamage;


    // 때린 플레이어의 위치를 알기 위해 필요
    Vector3 _player;
    
    bool check;

    private void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tree")
        {
            Gathering.instance.cutTree(other.gameObject, TreeAttackDamage);
        }
        else if (other.tag == "Stone")
        {
            Gathering.instance.pickStone(other.gameObject, StoneAttackDamage);
        }
        else if (other.tag == "Animal")
        {
            _player = transform.root.position;
            other.transform.GetComponent<Animal>().Damage(AnimalAttackDamage, _player,1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Main_Ingame")
        {
            if (!check)
            {
                TreeAttackDamage = ItemEnums.DamageTable[0];
                StoneAttackDamage = ItemEnums.DamageTable[0];
                AnimalAttackDamage = ItemEnums.DamageTable[0];
            }
        }
        
    }
}
