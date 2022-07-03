using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalUIManager : MonoBehaviour
{
    public static LocalUIManager instance;

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    
}
