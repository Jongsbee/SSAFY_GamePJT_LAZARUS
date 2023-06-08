using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testScript : MonoBehaviour
{

    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Show);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Show()
    {
        Debug.Log("Click");
    }
}
