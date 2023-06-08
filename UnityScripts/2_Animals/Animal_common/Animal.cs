using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    // 공통으로 올릴지 고민해볼 요소들
    [SerializeField] protected string animalName;
    [SerializeField] protected int animalHp;
    [SerializeField] protected float animalHunger;
    public int currentHp;
    public float currentHunger;
    public float foodFindHunger = 10f;
    public int carcassHp;
    private int carcassStack;
    
    [SerializeField] protected float thisSpeed;

    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float runSpeed;


    public Vector3 addVectorToDestination;
    public Vector3 destination;
    [SerializeField] protected float destinationRange;

    public bool isPredator = false;
    public bool isPrey = false;
    public bool isBoss = false;
    public bool isMinion = false;
    public bool isZombi = false;


    [SerializeField] public bool isHungry;
    [SerializeField] protected bool isAction;
    protected bool isWalking;
    protected bool isRunning;
    [SerializeField] public bool isChasing;
    [SerializeField] public bool isAttacking;
    public bool isDead;
    public bool isSleeping;
    private bool nowRunning = false;
    [SerializeField]  protected bool nowFire = false;


    [SerializeField] protected float walkTime;
    [SerializeField] protected float RunTime;
    [SerializeField] protected float waitTime;
    [SerializeField] protected float currentTime;

    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected AudioSource theAudio;

    [SerializeField] protected NavMeshAgent nav;
    [SerializeField] protected FieldViewAngle theViewAngle;
    public Boss theBoss;
    public Minion theMinion;

    private float thisVolume;
    [SerializeField] protected AudioClip[] sound_animal_normal;
    [SerializeField] protected AudioClip sound_animal_chase;
    [SerializeField] protected AudioClip sound_animal_attack;
    [SerializeField] protected AudioClip sound_animal_walk;
    [SerializeField] protected AudioClip sound_animal_run;
    [SerializeField] protected AudioClip sound_animal_hurt;
    [SerializeField] protected AudioClip sound_animal_dead;
    [SerializeField] protected AudioClip sound_animal_Sleep;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    

    private void Start()
    {
        thisVolume = theAudio.volume;
        isAction = true;    
        currentHp = animalHp;
        currentTime = waitTime;
        currentHunger = animalHunger;
        destination.Set(transform.position.x, transform.position.y, transform.position.z);
        FindFoot();
    }

    protected virtual void Update()
    {
        if (!isDead && (nav != null))
        {
            if (!isSleeping) 
            {
                Hunger();
                //Rotation();
            }
            Move();
            ElapseTime();
        }
    }

    private void FindFoot()
    {
        Animal _animal = GetComponent<Animal>();
        //Foot[] allFoot = GetComponentsInChildren<Foot>();
        //foreach (Foot _foot in allFoot)
        //{
        //    _foot.SetUp(_animal);
        //}
    }

    protected void Move()
    {
        if (nav.velocity.magnitude > walkSpeed + 0.3f)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", true);
            nowRunning = true;
            if (!theAudio.isPlaying)
            {
                try
                {
                    PlaySE(sound_animal_run);
                    theAudio.volume = 0.4f;
                }
                catch
                {
                    Debug.Log("달리기 브금 필요");
                }
            }
        }
        else if (nav.velocity.magnitude <= walkSpeed + 0.3f && nav.velocity.magnitude > 0.1f)
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Running", false);
            theAudio.volume = thisVolume;
            nowRunning = false;
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            theAudio.volume = thisVolume;
            nowRunning = false;
        }

        if ((isWalking || isRunning) && !isChasing)
        {   
            nav.SetDestination(destination + addVectorToDestination * 15f);
        }
        
    }

    public void Run(Vector3 _targetPos)
    {
        destination = transform.position;
        addVectorToDestination = new Vector3(transform.position.x - _targetPos.x, 0f, transform.position.z - _targetPos.z).normalized;

        if (isBoss && !isPredator)
        {
            theBoss.SendTarget(_targetPos);
        }

        currentTime = RunTime;
        isWalking = false;
        isRunning = true;
        try
        {
            nav.speed = runSpeed;
        }
        catch
        {
            //Debug.Log("이미 죽은 개체");
        }
    }

    protected void Hunger()
    {
        if (currentHunger > 0)
        {
            currentHunger -= (Time.deltaTime);
        }
        else if (currentHunger < 0)
        {
            currentHunger = 0;
        }
        if (currentHunger < foodFindHunger)
        {
            isHungry = true;
        }
        if (currentHunger >= animalHunger)
        {
            isHungry = false;
        }
    }

    protected void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
        }
        if ((currentTime <= -70f) || (currentTime <= 0 && !isAttacking && !isChasing))
        {
            currentTime = 0;
            StopAllCoroutines();
            ResetAction();
        }
    }

    protected virtual void ResetAction()
    {

        nav.ResetPath();
        isWalking = false;
        isRunning = false;
        isChasing = false;
        isAttacking = false;
        isAction = true;
        theViewAngle.lookFood = false;
        theViewAngle.lookPrey = false;
        theViewAngle.lookPredator = false;

        nav.speed = walkSpeed;
        addVectorToDestination.Set(Random.Range(-destinationRange, destinationRange), 0f, Random.Range(-destinationRange * 2f, destinationRange * 5f));
        
    }
       

    protected void Wait()
    {
        nav.speed = 0f;
        currentTime = waitTime;
        
    }

    protected void Eat()
    {
        nav.speed = 0f;
        currentTime = 15f;
        anim.SetTrigger("Eat");
    }
    protected void Peek()
    {
        nav.speed = 0f;
        anim.SetTrigger("Peek");
        currentTime = waitTime * 1.5f;
        RandomSound();
    }
    protected void Sit()
    {
        nav.speed = 0f;
        anim.SetTrigger("Sit");
        currentTime = 15f;
    }
    protected void TryWalk()
    {
        isWalking = true;
        currentTime = walkTime;
        nav.speed = walkSpeed;
    }


    public virtual void Damage(int _dmg, Vector3 _targetPos, int type) // type 추가 1- 플레이어, 2 - 모름
    {
        if (!isDead)
        {
            nowFire = false;

            nav.ResetPath();
            StopAllCoroutines();
            anim.SetTrigger("Hurt");
            if (sound_animal_hurt != null)
                PlaySE(sound_animal_hurt);
            if (isSleeping)
            {
                GetSleep(false);
                _dmg += _dmg;
            }
            
            Vector3 direction = new Vector3(_targetPos.x - transform.position.x, 0f, _targetPos.z - transform.position.z);
            float angle = Vector3.Angle(transform.forward, direction);
            if (!isRunning && angle >= 120f)
            {
                _dmg += _dmg;
            }

            currentHp -= _dmg;
            if (currentHp <= 0)
            {
                Debug.Log("무슨타입이니? " + type);
                Dead(type);
                return;
            }

            
        }
        else
        {
            currentHp -= 1;
            transform.position -= (transform.up / 10);
            carcassStack += 1;
            if (currentHp <= 0)
            {
                Revive();
                return;
            }
        }
    }

    

    protected void Dead(int type) // 으앙쥬금
    {
        if (type == 1) { UserInfo.instance.huntAnimalAPI(this.gameObject); } // 사람한테 죽은거면 로그 전송
        PlaySE(sound_animal_dead);
        anim.SetBool("Dead", true);
        currentHp = carcassHp;
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        if (isBoss)
        {
            theBoss.BossDead();
        }

        

        isDead = true;
        isAction = false;
        isRunning = false;
        isWalking = false;
        isChasing = false;
        isAttacking = false;
        ResetAction();
        StopAllCoroutines();
        nav.enabled = false;
        StartCoroutine(Carcass());
        StartCoroutine(Efflorescence());
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        rigid.isKinematic = true;
        rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

        Gathering.instance.dropMeats(this.gameObject, this.gameObject.transform.position + new Vector3(0, 2, 0));
    }

    private IEnumerator Carcass()
    {
        yield return new WaitForSeconds(2f);
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
    }

    private IEnumerator Efflorescence()
    {
        while (true) 
        {
            yield return new WaitForSeconds(6f);
            Damage(1, transform.position,2);
        }
    }

    public int GetHerd()
    {
        if (isBoss)
        {
            return theBoss.herdNum;
        }
        else if (isMinion)
        {
            return theMinion.minionHerdNum;
        }
        return -1;
    }

    public void GetSleep(bool _nowSleep)
    {
        isSleeping = _nowSleep;
        anim.SetBool("Sleeping", _nowSleep);
        theViewAngle.SleepingView(_nowSleep);
        if (_nowSleep && !isDead && !isChasing)
        {
            currentHunger = 50f;
            isHungry = false;
            ResetAction();
            PlaySE(sound_animal_Sleep);
        }
    }

    public bool GetIsRunning()
    {
        return (!isDead && nowRunning);
    }

    public void Recovery()
    {
        currentHp = animalHp;
    }

    private void Revive()
    {
        StopAllCoroutines();
        anim.SetBool("Dead", false);

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        int numOfChild = transform.childCount;
        for (int i = 0; i < numOfChild; i++)
            transform.GetChild(i).gameObject.SetActive(false);
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(180f);
        int numOfChild = transform.childCount;
        for (int i = 0; i < numOfChild; i++)
            transform.GetChild(i).gameObject.SetActive(true);

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }

        isDead = false;
        nav.enabled = true;
        rigid.isKinematic = false;
        rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

        anim.SetTrigger("Awake");
        transform.position += (transform.up / 10) * carcassStack;
        carcassStack = 0;
        currentHp = animalHp;
        currentTime = -80f;
        ElapseTime();
        if (isBoss)
        {
            theBoss.BossDead();
        }
    }

    //public void CampFire()
    //{
    //    Debug.Log("캠파");
    //    //ResetAction();
    //    //isHungry = false;
    //    //currentHunger = 10f;
    //    if (!nowFire)
    //    {
    //        StopAllCoroutines();
    //        StartCoroutine(LookFire());
    //    }
    //    //nav.SetDestination(transform.position + (Vector3.back * 2f));
    //}

    //private IEnumerator LookFire()
    //{
    //    nowFire = true;
    //    float _tempDistance = nav.stoppingDistance;
    //    nav.stoppingDistance = 10f;
    //    yield return new WaitForSeconds(1f);
    //    nav.stoppingDistance = _tempDistance;
    //    nowFire = false;
    //}

    protected void RandomSound()
    {
        int _cnt = sound_animal_normal.Length;
        int _random = Random.Range(0, _cnt);
        PlaySE(sound_animal_normal[_random]);
    }

    protected void PlaySE(AudioClip _clip)
    {
        try
        {
            theAudio.PlayOneShot(_clip);
        }
        catch 
        {
            Debug.Log("존재하지 않는 인덱스");
        }
    }   
}
