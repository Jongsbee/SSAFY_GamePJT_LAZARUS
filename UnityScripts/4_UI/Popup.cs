// Copyright (C) 2015-2021 gamevanilla - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace UltimateClean
{
    /// <summary>
    // This class is responsible for popup management. Popups follow the traditional behavior of
    // automatically blocking the input on elements behind it and adding a background texture.
    /// </summary>
    public class Popup : MonoBehaviour
    {
        Inventory inventory;

        public int selectedCraftIndex = -1;
        public Color backgroundColor = new Color(10.0f / 255.0f, 10.0f / 255.0f, 10.0f / 255.0f, 0.6f);

        public float destroyTime = 0.5f;

        private GameObject m_background;

        private void Start()
        {
            inventory = GameObject.Find("Manager").transform.Find("ItemManager").GetComponent<Inventory>();
        }

        public void ClosePopup(GameObject gameObject) // 팝업창을 닫기
        {
            switch(gameObject.name)
            {
                case "Inventory_Base": 
                     inventory.CloseInventory();
                    break;

                case "Crafting Panel":
                    Craft.Instance.CloseCraft();
                    break;
            }
        }

        public void CraftSelect(GameObject gameObject) // 선택하기
        {
            string namae = gameObject.name; // 가져오는 게임오브젝트의 이름
            int itemIndex = int.Parse(namae.Substring(namae.Length - 3, 3)); // 아이템 인덱스를 가져온다.
            selectedCraftIndex = itemIndex; // 아이템 인덱스를 저장한다.
            Debug.Log(selectedCraftIndex);
            int objectIndex = int.Parse(namae.Substring(0, 2)); // 오브젝트가 몇번째 위치에 저장되었는지
            Craft.initCraftDescription(); // 전부 다 끄기
            UIManager.instance.craftDescriptions[objectIndex].SetActive(true);

        }

        public void CreateItemClick() // 아이템을 만든다
        {
            if (selectedCraftIndex == -1)
            {
                UIManager.instance.eventTextShow("조합하려는 아이템을 선택해주세요.");
                return;
            }
            if (Craft.Instance.craftItem(selectedCraftIndex)) // 만약 조합에 성공했다면
            {
                UIManager.instance.eventTextShow("조합에 성공하였습니다!");
                selectedCraftIndex = -1; // index를 돌려놓기
            } 
        }
    }
}
