using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public GameObject brute;
    public Vector3 Spawn = new Vector3(7, -1, 0);
    public int ID;
    public ProgressBar progressbar;
    private bool activated = false;
    private bool spawnedEnemies = false;
    public GameObject hackingEffect;
    private GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onMyTriggerEnter += OnMyTriggerOpen;
        GameEvents.current.onMyTriggerExit += OnMyTriggerExit;
        //progressbar = new ProgressBar();

    }

    // Update is called once per frame
    void OnMyTriggerOpen(int id)
    {
        if (id == this.ID)
        {
            effect = Instantiate(hackingEffect, gameObject.transform.position, Quaternion.identity);
            activated = true;
            StartCoroutine(Tick(id));
            if (id == 3 && spawnedEnemies == false)
            {
                GameObject enemy1 = Instantiate(brute, new Vector3(Spawn.x - 1, Spawn.y + 6, Spawn.z), Quaternion.identity);
                GameObject enemy2 = Instantiate(brute, new Vector3(Spawn.x + 1, Spawn.y + 6, Spawn.z), Quaternion.identity);
                spawnedEnemies = true;
            }
        }

    }
    void OnMyTriggerExit(int id)
    {
        if (id == this.ID)
        {
            if (effect)
            {
                Destroy(effect);
            }
            activated = false;
            StopCoroutine(Tick(id));
        }
    }
    IEnumerator Tick(int id)
    {
        while (activated)
        {
            float progress = progressbar.IncrimentProgress(.01f, this.ID);
            //if (progress >= 1f &&this.ID == id)
            if (progressbar.GetProgressFromId(id) >= 1f)
            {
                if (effect)
                {
                    Destroy(effect);
                }
                activated = false;
                StopCoroutine(Tick(id));
            }
            yield return new WaitForSeconds(.01f);

        }


    }
}
