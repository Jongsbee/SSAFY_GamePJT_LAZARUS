using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowDrawSFX : MonoBehaviour
{

    private SFXManager _sFXManager;

    private void OnEnable()
    {
        _sFXManager = GameObject.FindObjectOfType<SFXManager>();
        _sFXManager.WeaponDrawSFX(2);
    }
}
