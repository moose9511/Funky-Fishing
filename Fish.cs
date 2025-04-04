using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "Inventory/Fish")]
public class Fish : ScriptableObject
{
    public Sprite icon;

    public string fishName;
    public List<string> possibleFishNames;
    
    public void PickName()
    {
        if (possibleFishNames.Count > 0)
        {
            int chosenName = Random.Range(0, possibleFishNames.Count);
            fishName = possibleFishNames[chosenName];
        } else
        {
            fishName = "idk";
        }
    }
} 
