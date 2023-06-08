using System.Collections.Generic;
using UnityEngine;

public class ItemEnums : MonoBehaviour // 아이템들에 대한 Index를 저장하는 클래스
{

    public static Dictionary<int, string> ItemDescription;
    public static Dictionary<int, int> ItemTypeDict;
    public static int[] DamageTable;

    public void Awake()
    {
        DamageTable = new int[] {100,40,40,40,100}; // 초기데미지 : 주먹 5, 곡괭이, 도끼, 활, 총 50 - 현재 이스터에그 모드
        ItemDescription = new Dictionary<int, string>
        {
            { 0, "기본적인 건축 재료입니다." },
            { 1, "고급 건축에 필요한 나무입니다." },
            { 10, "기본적인 건축 재료입니다." },
            { 11, "고급 건축에 필요한 돌입니다." },
            { (int)ItemNameIndex.MUSHROOM_1, "허기 + 10" },
            { (int)ItemNameIndex.MUSHROOM_2, "허기 + 10, HP -1" },
            { (int)ItemNameIndex.MUSHROOM_3, "허기 + 5" },
            { (int)ItemNameIndex.MEAT_LV1, "허기 + 15" },
            { (int)ItemNameIndex.MEAT_LV2, "허기 + 20" },
            { (int)ItemNameIndex.MEAT_LV3, "허기 + 30" },
            { (int)ItemNameIndex.CAMPFIRE, "흉폭한 동물들도 잠잠하게 해줍니다.(지속 : 2분)" },
            { (int)ItemNameIndex.ARROWS, "사용 : 화살20개 세트" },
            {62, "사용 : 탄알 30개 세트" },

            { (int)ItemNameIndex.CRYSTAL_1, "번개의 돌 아닙니다." },
            { (int)ItemNameIndex.CRYSTAL_2, "어디에 쓸지 모릅니다." },
            { (int)ItemNameIndex.AXE_1, "나무를 캘 때 사용합니다." },
            { (int)ItemNameIndex.AXE_2, "나무를 더 효율적으로 캘 때 사용합니다." },
            { (int)ItemNameIndex.PICKAXE_1, "돌을 캘 때 사용합니다." },
            { (int)ItemNameIndex.PICKAXE_2, "돌을 더 효율적으로 캘 때 사용합니다." },
            { (int)ItemNameIndex.BOW_1, "멀리 있는 적을 공격하는 데 사용합니다." },
            { (int)ItemNameIndex.BOW_2, "강력한 화살을 쏠 수 있습니다." },

            { (int)ItemNameIndex.RIFLE, "이거 진짜총이야" },
            { (int)ItemNameIndex.BBQ_MEAT_LV1, "허기 + 20" },
            { (int)ItemNameIndex.BBQ_MEAT_LV2, "허기 + 20 HP + 1" },
            { (int)ItemNameIndex.BBQ_MEAT_LV3, "허기 + 30 HP + 3" },
            { (int)ItemNameIndex.SOUP_MEAT_LV1, "허기 + 30  HP + 5" },
            { (int)ItemNameIndex.SOUP_MEAT_LV2, "허기 + 50  HP + 10" },
            { (int)ItemNameIndex.SOUP_MEAT_LV3, "허기 + 80  HP + 20" },

            { 72, "신비하게 빛나는 돌입니다"}
        };

        ItemTypeDict = new Dictionary<int, int>
        {
            {0, 2 }, {1, 2 }, {10, 2 }, {11, 2 }, {70, 2 }, {71, 2 }, // 재료

            {30, 3 }, {31, 3 },{32, 3 }, {40, 3 }, {41, 3 }, {42, 3 }, {200, 3 }, {201, 3 }, {202, 3 }, {210, 3 },{211, 3 }, {212, 3 }, // 음식

            {100, 0 },{101, 0 }, {110, 0 },{111, 0 },{120, 0 },{121, 0 },{140,0}, // 장비

            {60, 1 }, {61, 1 }, {62, 1 }, // 소모형

            {72, 4 } // 퀘스트

        };

    }

    public enum ItemType
    {
        Equipment = 0,
        Consumable =1,
        Ingredient = 2,
        Food = 3,
        Quest = 4, 
        ETC = 5
    }

    public enum ItemNameIndex
    {
        WOOD_LV1 = 0,
        WOOD_LV2 = 1,

        STONE_LV1 = 10,
        STONE_LV2 = 11,

        MUSHROOM_1 = 30,
        MUSHROOM_2 = 31,
        MUSHROOM_3 = 32,

        MEAT_LV1 = 40,
        MEAT_LV2 = 41,
        MEAT_LV3 = 42,

        CAMPFIRE = 60,
        ARROWS = 61,
        MAGAZINE = 62,

        CRYSTAL_1 = 70,
        CRYSTAL_2 = 71,

        AXE_1 = 100,
        AXE_2 = 101,

        PICKAXE_1 = 110,
        PICKAXE_2 = 111,

        BOW_1 = 120,
        BOW_2 = 121,

        RIFLE = 140,

        BBQ_MEAT_LV1 = 200,
        BBQ_MEAT_LV2 = 201,
        BBQ_MEAT_LV3 = 202,

        SOUP_MEAT_LV1 = 210,
        SOUP_MEAT_LV2 = 211,
        SOUP_MEAT_LV3 = 212,

        JungHaeSuk = 301,
    }

    public enum ItemNameKorIndex
    {
        목재 = 0,
        가공목재 = 1,
        나무진액 = 2,

        돌 = 10,
        가공석재 = 11,

        식용버섯 = 30,
        맛있는독버섯 = 31,
        향료버섯 = 32,

        질긴고기 = 40,
        맛좋은고기 = 41,
        고단백고기 = 42,

        모닥불 = 60,
        화살세트 = 61,
        탄알집 = 62,

        전광석 = 70,
        경화석 = 71,
        

        조잡한도끼 = 100,
        단단한도끼 = 101,

        조잡한곡괭이 = 110,
        단단한곡괭이 = 111,

        조잡한활 = 120,
        단단한활 = 121,

        라이플 = 140,

        구운질긴고기 = 200,
        구운맛좋은고기 = 201,
        구운고단백고기 = 202,

        버섯고기꼬치 = 210,
        버섯샤브샤브 = 211,
        송이버섯고깃국 = 212,

        정해석 = 301
    }

    public enum craftItems
    {
        WOOD_LV2 = 1,
        STONE_LV2 = 2,

        CAMPFIRE = 4,
        ARROW = 3,

        AXE_1 = 6,
        AXE_2 = 9,

        PICKAXE_1 = 5,
        PICKAXE_2 = 8,

        BOW_1 = 7,
        BOW_2 = 10,

        BBQ_MEAT_LV1 = 11,
        BBQ_MEAT_LV2 = 12,
        BBQ_MEAT_LV3 = 13,

        SOUP_MEAT_LV1 = 14,
        SOUP_MEAT_LV2 = 15,
        SOUP_MEAT_LV3 = 16
    }


}
