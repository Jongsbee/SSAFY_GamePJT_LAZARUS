using UnityEngine;

public class PickAxe : Equipments
{
    [SerializeField] private int TreeAttackDamage;
    [SerializeField] private int StoneAttackDamage;
    [SerializeField] private int AnimalAttackDamage;


    // 때린 플레이어의 위치를 알기 위해 필요
    Vector3 _player;
    private void Start()
    {
        TreeAttackDamage = 1;
        StoneAttackDamage = ItemEnums.DamageTable[1];
        AnimalAttackDamage = ItemEnums.DamageTable[1];
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tree")
        {
            Gathering.instance.cutTree(other.gameObject, TreeAttackDamage);
        }
        else if (other.tag == "Stone")
        {
            Gathering.instance.pickStone(other.gameObject, StoneAttackDamage);
        }
        else if (other.tag == "Animal")
        {
            _player = transform.root.position;
            other.transform.GetComponent<Animal>().Damage(AnimalAttackDamage, _player,1);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
