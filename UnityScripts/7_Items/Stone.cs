using UnityEngine;

public class Stone : MonoBehaviour // 돌들에 들어가는 Script
{
    public int stoneHp; // 돌 Hp
    public string stoneName; // 돌 이름
    public int index; // index
    public bool waitForRespawn; // 리스폰 확인

    private void Awake()
    {
        stoneName = this.gameObject.name;
    }
    // Start is called before the first frame update
    void Start()
    {
        updateStoneHP();

    }

    // 돌 Hp 다시 채우기
    public void updateStoneHP()
    {
        if (gameObject.name.Contains("StoneSmall")) // 돌의 경우
        {
            index = int.Parse(stoneName.Substring(18, 2)) / 2; // 1,2 는 작은돌이라 1 & 3,4 는 큰돌이라 2
            stoneHp = Gathering.instance.stoneHpArr[index]; // 맨 뒤 두글자 숫자를 가져온 후, int 로 바꾸어 index를 한다
        }
        else // 크리스탈인 경우
        {
            index = 2;
            stoneHp = Gathering.instance.stoneHpArr[index]; // 크리스탈의 체력은 100
        }
    }

}
