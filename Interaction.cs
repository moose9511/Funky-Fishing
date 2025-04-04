using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public GameObject Text;
    public TextMeshProUGUI description;
    public GameObject Camera;

    public float interactionDistance;
    private RaycastHit[] hits;
    public static Collider lastHit;

    public void Update()
    {
        hits = Physics.RaycastAll(Camera.transform.position, Camera.transform.forward, interactionDistance);
        lastHit = getInteractable();

        if(lastHit != null && lastHit.tag == "Interactable")
        {
            Text.SetActive(true);
        } else
        {
            Text.SetActive(false);
        }
       
        
        
    }

    public Collider getInteractable()
    {
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Interactable")
            {
                if (hit.collider.GetComponent<Description>().descriptions[FishManager.day - 1] != null)
                    description.text = hit.collider.GetComponent<Description>().descriptions[FishManager.day - 1];

                return hit.collider;
            } 
        }

        return null;
    }
}
