using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    public PlayerMovement player;
    private Slider slider;
    private int currentID=0;
    private Transform[] indicators;
    public float fillSpeed = 0.51f;
    //public ParticleSystem particalSystem;
    private float[] targetProgress = {0f};
    private float[] SavedProgress = {0f};
    private int completedObjectives = 0;
    private int totalObjectives = 0;


    // Start is called before the first frame update
    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        Transform indicator = gameObject.transform.Find("Indicator");
        //indicator.GetComponent<Image>().color = Color.black;
        indicators = indicator.GetComponentsInChildren<Transform>();
        targetProgress = System.Array.ConvertAll(new float[indicators.Length], v => 0f);
        SavedProgress = System.Array.ConvertAll(new float[indicators.Length], v => 0f);
        totalObjectives = indicators.Length-1;
    }

    void Update()
    {
        slider.value = SavedProgress[currentID];
        if (SavedProgress[currentID] <= targetProgress[currentID])
        {
            SavedProgress[currentID] += fillSpeed * Time.deltaTime;
        }
        if(slider.value == 1f)
        {
            if(SavedProgress[currentID]<2f)
            {
                FillCircle();
            }
            SavedProgress[currentID]=2f;
        }
    }

    public float IncrimentProgress(float newProgress, int id)
    {
        currentID = id;
        targetProgress[currentID] = slider.value + newProgress;
        return slider.value;
    }
    public void Set(float percent)
    {
        slider.value = 0f;
    }
    public float GetProgressFromId(int id)
    {
        return SavedProgress[id];
    }
    void FillCircle()
    {
        print(completedObjectives +" "+totalObjectives);
        indicators[currentID+1].GetComponent<Image>().color = new Color32(0,173,34,255);
        completedObjectives++;
        if(completedObjectives == totalObjectives)
        {
            player.wincondition = true;
        }
    }

}
