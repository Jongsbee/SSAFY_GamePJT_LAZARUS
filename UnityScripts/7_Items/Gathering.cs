using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Gathering : MonoBehaviour // 채집을 하는 스크립트
{
    public static Gathering instance { get; private set; }

    public int[] treeHpArr, stoneHpArr, treeDropCnt, stoneDropCnt, weaponDamageArr; // 각각 나무 Hp, 돌 Hp, 나무 드랍개수, 돌 드랍개수, 칼 데미지 배열
    public readonly int MAX_TREE_TYPE = 10; // 0 - 얇은나무, 1 - 좀두꺼운, 2 - 두꺼운, 3 - 왕두꺼운
    public readonly int MAX_STONE_TYPE = 3; // 0 - 작은돌, 1 - 큰돌, 2 - 크리스탈, 루비 사파이어 등등?

    [SerializeField] ParticleSystem[] Impacts;

    public float respawnTime; // 나무 리스폰 시간

    private void Awake()
    {
        respawnTime = 180f; // 리스폰타임 : 3분
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            instance = this;
        }

        treeHpArr = new int[] {10, 20 ,50,100,200}; // 나무체력 - 1,2,3,5,8 개
        treeDropCnt = new int[] { 1, 2, 3, 5, 8};
        stoneHpArr = new int[] {10, 50, 100};   // 돌체력 - 1,2개, 1개
        stoneDropCnt = new int[] {1, 3, 1};

        weaponDamageArr = new int[]
        {
            15,30,15,30,10,20,50 // 각각 곡괭, 강화곡괭, 도끼, 강화도끼, 활, 강화활, 총
        };
    }


    // 나무를 자르는 메서드
    public void cutTree(GameObject treeObject, int damage) 
    {
        Tree tree = treeObject.GetComponent<Tree>(); // 해당 오브젝트로 부터 Tree 가져오기
        tree.treeHp -= damage; // 데미지 만큼 Tree 깎기
        Instantiate(Impacts[0], tree.transform.position + (Vector3.up * 2f), quaternion.identity); // 효과
        if (tree.treeHp <= 0) // 나무 Hp 가 0밑으로 떨어지면 - 나무를 베면
        {
            StartCoroutine(DisableAndDrop(tree.gameObject, 1));  // 잡았을 때 나무 아이템 나오기
            Instantiate(Impacts[1], tree.transform.position + (Vector3.up * 2f), quaternion.identity); // 효과 2
        }
    }

    // 돌을 캐는메서드
    public void pickStone(GameObject stoneObject, int damage) 
    {
        Stone stone = stoneObject.GetComponent<Stone>(); // 해당 오브젝트의 컴포넌트를 불러온다.
        stone.stoneHp -= damage; // 해당하는 돌의 HP를 깎는다
        Instantiate(Impacts[2], stone.transform.position + Vector3.up, quaternion.identity); // 돌때리는 이펙트 1
        if (stone.stoneHp <= 0) // 체력이 0 이하로 떨어졌을때
        {
            StartCoroutine(DisableAndDrop(stoneObject, 2));
            Instantiate(Impacts[3], stone.transform.position + Vector3.up, quaternion.identity); // 돌때리는 이펙트 2
        }
    }

    private IEnumerator DisableAndDrop(GameObject ingredientObj, int type) // type 에 따라서 tree, stone 을 가르게 한다.
    {
        if(type == 1) // 나무인 경우
        {
           
            ingredientObj.GetComponent<Respawn>().isWaitingRespawn = true;  // 리스폰 대기 = true : 캐릭터 100f 거리 안에서 해당 bool 이 true 인 친구들은 다시 화성화 시키지 않는다.
            ingredientObj.SetActive(false);
            DropItems(0,ingredientObj.transform.position + new Vector3(0, 2, 0), treeDropCnt[ingredientObj.GetComponent<Tree>().index]); // 아이템을 드롭한다 - 위치는 해당 오브젝트보다 2미터위에서 떨어지기
            ingredientObj.GetComponent<Tree>().updateTreeHP(); // 나무의 HP를 다시 채우기

            // 일정 시간 후에 나무를 다시 활성화
            yield return new WaitForSeconds(respawnTime);
            ingredientObj.GetComponent<Respawn>().isWaitingRespawn = false;

        }
        else if (type == 2) // 돌인경우
        {
            
            int _itemIndex;
            if (ingredientObj.name.Contains("Blue"))
            {
                _itemIndex = 70;
            }
            else if(ingredientObj.name.Contains("Purple"))
            {
                _itemIndex = 71;
            }
            else
            {
                _itemIndex = 10;
            }
            ingredientObj.GetComponent<Respawn>().isWaitingRespawn = true;
            ingredientObj.SetActive(false);
            DropItems(_itemIndex, ingredientObj.transform.position + new Vector3(0, 2, 0), stoneDropCnt[ingredientObj.GetComponent<Stone>().index]); // 여기서부터 시작
            ingredientObj.GetComponent<Stone>().updateStoneHP();

            // 일정 시간 후에 나무를 다시 활성화
            yield return new WaitForSeconds(respawnTime);
            ingredientObj.GetComponent<Respawn>().isWaitingRespawn = false;
        }

    }

    public void dropMeats(GameObject animalObject , Vector3 position)
    {
        string animalName = animalObject.name;
        int itemIndex;
        int count;
        // 현재 animal 에 대한 index 가 없어, switch 문으로 설정
        if (animalName.Contains("WolfBoss")) { itemIndex = 41; count = 3; } // 고기가 3개
        else if (animalName.Contains("ZombiBearBoss")) { itemIndex = -1; count = 1; }
        else if (animalName.Contains("Boar")) { itemIndex = 41; count = 2; }
        else if (animalName.Contains("Wolf_Grey")) { itemIndex = 41; count = 1; }
        else if (animalName.Contains("Wolf_Black")) { itemIndex = 41; count = 1; }
        else if (animalName.Contains("StagBoss")) { itemIndex = 40; count = 3; }
        else if (animalName.Contains("Stag")) { itemIndex = 40; count = 1; }
        else if (animalName.Contains("DeerBoss")) { itemIndex = 40; count = 3; }
        else if (animalName.Contains("Deer_DarkBrown")) { itemIndex = 40; count = 1; }
        else if (animalName.Contains("Deer_Brown")) { itemIndex = 40; count = 1; }
        else if (animalName.Contains("BearMinion")) { itemIndex = 42; count = 1; }
        else if (animalName.Contains("BearBoss")) { itemIndex = 42; count = 3; }
        else if (animalName.Contains("Bear")) { itemIndex = 42; count = 1; }
        
        else return;
        Debug.Log($"name : {animalName}, itemIndex : {itemIndex}, count : {count}");

         
        if(itemIndex == -1) // 좀비베어 보스라면
        {
            GameObject purifyKeyItem = Resources.Load<GameObject>("Crystal_01_Red_18");
            Instantiate(purifyKeyItem, animalObject.transform.position + new Vector3(0, 2, 0), quaternion.identity); // 열쇠를 떨어뜨리기
        }
        else
        {
            GameObject gameObj = Instantiate(ActionController.instance.itemInfoDictionary[itemIndex].itemObject, position, Quaternion.identity); // 해당 개수를 가진 아이템 떨구기
            gameObj.GetComponent<Item>().itemCount = count; // 아이템의 갯수 설정해주기
        }

    }

    // 아이템 떨어뜨리기
    public void DropItems(int itemIndex, Vector3 position, int count)
    {
        GameObject gameObj = Instantiate(ActionController.instance.itemInfoDictionary[itemIndex].itemObject, position, Quaternion.identity); // 해당 개수를 가진 아이템 떨구기
        gameObj.GetComponent<Item>().itemCount = count; // 아이템의 갯수 설정해주기
    }

}
