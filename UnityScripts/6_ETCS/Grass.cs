using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private int GrassHp = 5;
    public int currentHp;

    public bool isDead = false;
    private float respawnTime = 15f;
    private float currentTime;

    void Start()
    {
        currentHp = GrassHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Respawn();
        }
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            currentHp -=1;
            if (currentHp <= 0)
            {
                Dead();
                return;
            }
        }
    }

    private void Dead()                                                         // 일정 데미지 이후 렌더링 비활성화
    {
        isDead = !isDead;
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        
        currentTime = respawnTime;
        
    }

    private void Respawn()                                                      // 일정 시간 이후 활성화
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0) 
        {
            isDead = !isDead;
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;

        }
    }
}
