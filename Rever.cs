using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rever : MonoBehaviour
{
    [SerializeField] private GameObject[] toBeRevealed; //this is an object that should have children and should be interactable
    public void Reveal()
    {
        foreach (var item in toBeRevealed)
        {
            item.SetActive(item.activeSelf != true);
        }
        
    }
}
