using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class LableSwitch : MonoBehaviour
{
    void Start()
    {
        var n = GetComponent<SpriteResolver>();
        n.SetCategoryAndLabel("Gowth", "b");
    }
}