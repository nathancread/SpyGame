using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int ID;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onMyHackCompleted += OnMyHackCompleted;
    }

    void OnMyHackCompleted(int id)
    {
        if (id == this.ID)
        {
            print("time to open door");
            gameObject.SetActive(false);
        }
    }
}
