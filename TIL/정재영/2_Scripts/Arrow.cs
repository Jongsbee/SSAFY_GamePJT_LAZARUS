using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    public int ArrowDamage;
    Vector3 _player;

    private void Start()
    {
        //Destroy(gameObject, 2);
    }

    public void SetUp(Vector3 _position)
    {
        _player = _position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<Rigidbody>());
        Destroy(transform.GetComponent<Collider>());
        Debug.Log(other.name);
        if (other.tag == "Animal")
        {
            other.transform.GetComponent<Animal>().Damage(ArrowDamage, _player);
        }
        transform.SetParent(other.transform, true);
        //2초 후 소멸
        //Destroy(newArrow, 2);
    }
}
