using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampLight : MonoBehaviour
{

    private float fireDistance = 90f;
    
    public void SetUp(float _fireDistance)
    {
        fireDistance= _fireDistance;
    }

    private void Update()
    {
        DefenseAnimals();
    }

    private void DefenseAnimals()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, fireDistance);                           // 좀비에 대해서만 campfire메소드 호출
        for (int i = 0; i < _target.Length; i++)
        {
            if (_target[i].transform.tag == "Animal" && _target[i].transform.GetComponent<Animal>().isZombi)
            {
                _target[i].transform.GetComponent<Zombi>().CampFire();
                _target[i].transform.GetComponent<Animal>().Run(transform.position);


            }
        }
    }
}
