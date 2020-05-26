using System.Collections;
using System;

using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    public event Action<int> onMyTriggerEnter;
    //public event Action<int> onMyHackCompleted;

    public event Action<int> onMyTriggerExit;

    public void MyTriggerEnter(int id)
    {
        if (onMyTriggerEnter != null)
        {
            onMyTriggerEnter(id);
        }
    }
    
    public void MyTriggerExit(int id)
    {
        if (onMyTriggerExit != null)
        {
            onMyTriggerExit(id);
        }
    }

}
