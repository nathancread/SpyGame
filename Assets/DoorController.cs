using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onMyTriggerEnter += OnMyTriggerOpen;
        GameEvents.current.onMyTriggerExit += OnMyTriggerExit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
