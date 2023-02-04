using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private GameObject[] canvases;

    private void Start()
    {
        foreach (var item in canvases)
        {
            item.SetActive(true);
        }
    }
}