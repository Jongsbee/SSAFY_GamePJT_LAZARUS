using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAllChildrenOfSelectedGameObject : MonoBehaviour
{
    public GameObject parentGameObject;
    public void DisableAllChildren()
    {
        // 첫번째 오브젝트를 제외하고 전부 해제한다.
        for(int i = 0; i < parentGameObject.transform.childCount; i++)
        {
            var child = parentGameObject.transform.GetChild(i).gameObject;

            if(child != null)
            {
                child.SetActive(false);
            }
        }
    }
}
