using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private int ArrowDamage = 30;
    Vector3 _player;

    private void Start()
    {
        ArrowDamage = ItemEnums.DamageTable[3];
    }

    // 플레이어 포지션 가져오기
    public void SetUp(Vector3 _position)
    {
        _player = _position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<Rigidbody>());
        Destroy(transform.GetComponent<Collider>());
        //Debug.Log(other.name);
        if (other.tag == "Animal")
        {
            other.transform.GetComponent<Animal>().Damage(ArrowDamage, _player,1);
        }
        else if (other.tag == "Tree")
        {
            Gathering.instance.cutTree(other.gameObject, 30);
            Debug.Log("맞았다!!");
        }
        else if (other.tag == "Stone")
        {
            Debug.Log("돌맞았는디?");
            Gathering.instance.pickStone(other.gameObject, 30);
        }

        Destroy(gameObject, 1);
        //transform.SetParent(other.transform, true);
        //2초 후 소멸
        //Destroy(newArrow, 2);
        //transform.Translate(Vector3.back * 3f);
        //transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
