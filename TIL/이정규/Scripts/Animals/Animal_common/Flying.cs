using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flying : MonoBehaviour
{
    [SerializeField]
    protected string animalName;
    [SerializeField]
    protected int animalHp;
    protected int currentHp;
    [SerializeField]
    protected float walkSpeed;
    [SerializeField]
    protected float runSpeed;

    private float applySpeed;

    private Vector3 direction;
    protected Vector3 destination;
    [SerializeField]
    protected float destinationRange;

    protected bool isAction;
    protected bool isWalking;
    protected bool isRunning;
    protected bool isChasing;
    protected bool isAttacking;
    protected bool isDead;


    [SerializeField]
    protected float walkTime;
    [SerializeField]
    protected float RunTime;
    [SerializeField]
    protected float waitTime;
    protected float currentTime;

    [SerializeField]
    protected Animator anim;
    [SerializeField]
    protected Rigidbody rigid;
    [SerializeField]
    protected CapsuleCollider capCol;
    protected AudioSource theAudio;
    protected NavMeshAgent nav;
    protected FieldViewAngle theViewAngle;

    [SerializeField]
    protected AudioClip[] sound_animal_normal;
    [SerializeField]
    protected AudioClip sound_animal_hurt;
    [SerializeField]
    protected AudioClip sound_animal_dead;


    void Start()
    {
        theViewAngle = GetComponent<FieldViewAngle>();
        nav = GetComponent<NavMeshAgent>();
        theAudio = GetComponent<AudioSource>();
        currentTime = waitTime;
        currentHp = animalHp;
        isAction = true;
    }
    protected virtual void Update()
    {
        if (!isDead)
        {
            Move();
            Rotation();
            ElapseTime();
        }

    }
    protected void Move()
    {
        if (isWalking || isRunning)
        {
            rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
            //nav.SetDestination(transform.position + destination * 5f);
        }
    }

    protected void Rotation()
    {
        if (isWalking || isRunning)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }

    protected void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && !isChasing && !isAttacking)
            {
                ResetAction();
            }
        }
    }

    protected virtual void ResetAction()
    {
        isWalking = false;
        isRunning = false;
        isAction = true;

        applySpeed = walkSpeed;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        destination.Set(Random.Range(-destinationRange, destinationRange), 0f, Random.Range(-destinationRange * 2f, destinationRange * 5f));
        direction.Set(0f, Random.Range(0f, 360f), 0f);
        RandomAction();
    }

    private void RandomAction()
    {

        //RandomSound();
        int _random = Random.Range(0, 2);

        if (_random == 0)
        {
            Wait();
        }
        else if (_random == 1)
        {
            TryWalk();
        }
    }

    protected void Wait()
    {
        currentTime = waitTime;
    }

    protected void TryWalk()
    {
        isWalking = true;
        currentTime = walkTime;
        applySpeed = walkSpeed;
        anim.SetBool("Walking", isWalking);
    }


    public virtual void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {

            currentHp -= _dmg;
            Debug.Log(currentHp);
            if (currentHp <= 0)
            {
                Dead(_targetPos);
                return;
            }
            
            if (!isDead)
            {
                Run(_targetPos);
            }
            //PlaySE(sound_animal_hurt);
            anim.SetTrigger("Hurt");
        }
    }

    public void Run(Vector3 _targetPos)
    {
        destination = new Vector3(transform.position.x - _targetPos.x, 0f, transform.position.z - _targetPos.z).normalized;

        currentTime = RunTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;
        anim.SetBool("Running", isRunning);
    }

    protected void Dead(Vector3 _targetPos)
    {
        //PlaySE(sound_animal_dead);
        isDead = true;
        isRunning = false;
        isWalking = false;

        //rigid.AddForce(new Vector3(transform.position.x - _targetPos.x, 0f, transform.position.z - _targetPos.z).normalized * 0.01f);

        rigid.constraints = ~RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionY & ~RigidbodyConstraints.FreezePositionZ;

        anim.SetTrigger("Dead");
        StopAllCoroutines();
        //rigid.drag = Mathf.Infinity;
        //rigid.mass = Mathf.Infinity;
    }

    protected void RandomSound()
    {
        int _random = Random.Range(0, 3);
        PlaySE(sound_animal_normal[_random]);
    }

    protected void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }

}
