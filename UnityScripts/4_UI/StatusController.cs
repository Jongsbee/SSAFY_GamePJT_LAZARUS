using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class StatusController : MonoBehaviour
{
    public static StatusController instance { get; private set; }
    // 각 상태에 해당하는 상수 설정

    // 체력, 스테미나, 배고픔 등의 상태 변수 설정
    [SerializeField]
    private float maxHp;  // 총 체력
    [SerializeField]
    private float currentHp;  // 현재 체력

    [SerializeField]
    private float maxSp;  // 총 스테미나
    [SerializeField]
    private float currentSp;  // 현재 스테미나

    [SerializeField]
    private float spIncreaseSpeed;  // 스테미나 회복 속도

    [SerializeField]
    private float spRechargeTime;  // 스테미나가 차는데 걸리는 시간
    private float currentSpRechargeTime;  // 현재 스테미나 차는데 걸린 시간

    private bool spUsed;  // 스테미나 사용 여부

    [SerializeField]
    private int dp;  // 방어력
    private int currentDp;  // 현재 방어력

    [SerializeField]
    private float maxHungry;  // 총 배고픔 수치
    [SerializeField]
    private float currentHungry;  // 현재 배고픔 수치

    private float hungryDecreaseTime;  // 배고픔이 줄어드는 시간
    private float currentHungryDecreaseTime;  // 배고픔이 줄어드는데 걸린 시간

    private float hpDecreaseTime;
    private float currentHpDecreaseTime;
    private Animator animator;

    [SerializeField]
    public bool isDead;  // 죽음 여부
    public bool isDown;  // 기절 여부

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(instance);
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHp = 1000;
        maxSp = 100;
        maxHungry = 100;

        spIncreaseSpeed = 2f; // 프레임당 차는 스테미너
        spRechargeTime = 3f; // 몇초 후에 다시 스테미너가 차는지
        hungryDecreaseTime = 2f;
        hpDecreaseTime = 3.0f; // 3초마다 체력이 1씩 단다.
        // 게임 시작 시 현재 상태를 초기 상태로 설정
        currentHp = maxHp;
        currentSp = maxSp;
        currentHungry = maxHungry;
        isDead = false;
        animator = GameObject.Find("Survival_Girl").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 매 프레임마다 배고픔 감소, 스테미나 회복 등의 함수 호출
        if(!isDead)
        {
            Hungry();
            updateSpRechargeTime();
            SPRecover();
            GaugeUpdate();
        }
    }

    private void updateSpRechargeTime()
    {
        // 스테미나가 사용되었을 경우, 스테미나 재충전 시간을 증가시킴
        if (spUsed) // 스테미너가 사용된 다음 일정시간이 지나면 스태미너 재충전
        {
            if(currentSpRechargeTime < spRechargeTime)
            {
                currentSpRechargeTime += Time.deltaTime;
            }
            else
            {
                spUsed = false;
            }
        }
    }

    private void SPRecover()
    {
        // 스테미나가 사용중이 아니고, 현재 스테미나가 총 스테미나보다 작을 경우 스테미나 회복
        if (!spUsed && currentSp < maxSp)
        {
            currentSp += spIncreaseSpeed;
        }
    }

    private void Hungry()
    {
        // 배고픔 수치가 0보다 클 경우, 배고픔 감소 시간을 증가시키고, 
        // 배고픔 감소 시간이 배고픔이 줄어드는 시간보다 클 경우 배고픔 수치를 감소시킴
        if (currentHungry > 0)
        {
            if(currentHungryDecreaseTime <= hungryDecreaseTime)
            {
                currentHungryDecreaseTime += Time.deltaTime; // currentHungryDecreaseTime 에 초당 1초씩 더한다.
            }
            else // 일정 시간이 지난 후마다
            {
                currentHungry -= 0.5f;  // 배고픔 수치를 감소
                currentHungryDecreaseTime = 0; // currentHungryDecreaseTime 을 초기화
            }
        }
        else
        {
            // 배고픔 수치가 0이 될 경우 출력 - 3초마다 체력 1씩 단다.
            if (currentHpDecreaseTime < hpDecreaseTime)
            {
                currentHpDecreaseTime += Time.deltaTime;
            }
            else
            {
                DecreaseHP(1);
                currentHpDecreaseTime = 0;
            }

            //Debug.Log("배고픔 수치가 0이 되었습니다.");
        }
    }

    private void GaugeUpdate() // UI 게이지 업데이트 함수. 현재 상태값을 최대 상태값으로 나눈 비율로 표시
    {
        UIManager.instance.hud[0].fillAmount = currentHp / maxHp;
        UIManager.instance.hud[1].fillAmount = currentHungry / maxHungry;
        UIManager.instance.hud[2].fillAmount = currentSp / maxSp;
        
    }

    public void IncreaseHP(int _count) // HP 증가 함수. 현재 체력에 증가량을 더하되, 최대 체력을 초과하지 않게 함
    {
        if(currentHp + _count < maxHp)
        {
            currentHp += _count;
        }
        else
        {
            currentHp = maxHp;
        }
    }

    // HP 감소 함수. 현재 체력에 감소량을 빼고, 체력이 0 이하가 되면 죽음 상태로 변경
    public void DecreaseHP(int _count)
    {
        currentHp -= _count;

        if(currentHp <= 0)
        {
            isDead = true;
            // TODO: 죽는 애니메이션 추가

            GaugeUpdate();
            animator.SetTrigger("Die");
            UserInfo.instance.gameClearAPI(false); // 게임클리어 실패
            Fade _fade = FindObjectOfType<Fade>();
            if (_fade != null) _fade.FadeImage(1, false);
        }
    }

    // 배고픔 증가 함수. 현재 배고픔에 증가량을 더하되, 최대 배고픔을 초과하지 않게 함
    public void IncreaseHungry(int _count)
    {
        if(currentHungry + _count < maxHungry)
        {
            currentHungry += _count;
        }
        else
        {
            currentHungry = maxHungry;
        }
    }

    // 배고픔 감소 함수. 현재 배고픔에 감소량을 빼되, 0 미만으로 내려가지 않게 함
    public void DecreaseHungry(int _count)
    {
        if(currentHungry - _count < 0)
        {
            currentHungry = 0;
        }
        else
        {
            currentHungry -= _count;
        }
    }

    //Defense Point will be update.

    public bool DecreaseStamina(float _count)
    {
        spUsed = true; // 스태미너를 사용중
        currentSpRechargeTime = 0;

        if(currentSp - _count > 0)
        {
            currentSp -= _count;
            return true;
        }
        else
        {
            return false;
        }
    }

    // 현재 스테미나를 반환하는 함수
    public float GetCurrentSP()
    {
        return currentSp;
    }

    public float GetCurrentHP()
    {
        return currentHp;
    }

    public float GetCurrentHPRate()
    {
        return currentHp / maxHp;
    }

    public float GetCurrentHunger()
    {
        return currentHungry;
    }

    // 종섭 추가 - 공격 받은 후, 일정시간은 무적상태
    public IEnumerator cannotAttackForSeconds(float seconds)
    {
        isDown = true;
     
        yield return new WaitForSeconds(seconds);

        isDown = false;
    }

}
