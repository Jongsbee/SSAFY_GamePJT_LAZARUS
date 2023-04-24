using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    public int ArrowDamage;

    private void Start()
    {
        Destroy(gameObject, 10);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<Rigidbody>());
        Destroy(transform.GetComponent<Collider>());
        Debug.Log(other.name);
        if (other.tag == "Animal")
        {
            other.transform.GetComponent<Animal>().Damage(ArrowDamage, -transform.position);
        }
    }
}
