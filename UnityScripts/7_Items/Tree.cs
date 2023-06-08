using UnityEngine;

public class Tree : MonoBehaviour // 각각의 나무들에 들어갈 Script들
{
    public int treeHp;
    public string treeName;
    public int index;
    public bool waitForRespawn;

    private void Awake()
    {
        waitForRespawn = false;
        treeName = this.gameObject.name;
    }
    // Start is called before the first frame update
    void Start()
    {
        updateTreeHP();

    }

    // 나무의 피를 채우는 메서드
    public void updateTreeHP()
    {
        if (gameObject.name.Contains("Small")) // 작은 나무는 
        {
            index = int.Parse(treeName.Substring(18, 2)) % 3;
            treeHp = Gathering.instance.treeHpArr[index]; // 맨 뒤 두글자 숫자를 가져온 후, int 로 바꾸어 index를 한다
        }
        else if(gameObject.name.Contains("Large"))
        {
            index = 3;
            treeHp = Gathering.instance.treeHpArr[index]; // 큰나무 체력은 100
        }else
        {
            index = 4;
            treeHp = Gathering.instance.treeHpArr[index]; // 왕큰나무 체력은 200
        }
    }

}
