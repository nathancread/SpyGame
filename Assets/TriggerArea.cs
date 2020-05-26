using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public int id;
 void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
        GameEvents.current.MyTriggerEnter(id);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
        GameEvents.current.MyTriggerExit(id);
        }
    }
}
