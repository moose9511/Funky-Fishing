using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    Collider hit;
    public string ID;

    bool isDescribing;
    public GameObject interacter;

    public string secondaryDescription;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            hit = GetCollider();
            if(hit != null && hit.GetComponent<Description>().ID == ID)
            {
                Interact();
            }
        }
    }

    private Collider GetCollider()
    {
        return Interaction.lastHit;
    }
    public IEnumerator InteractDescription(float seconds)
    {
        if (!isDescribing)
        {
            isDescribing = true;

            string ogDescription = interacter.GetComponent<Description>().descriptions[FishManager.day - 1];

            interacter.GetComponent<Description>().descriptions[FishManager.day - 1] = secondaryDescription;

            yield return new WaitForSeconds(seconds);

            GetComponent<Description>().descriptions[FishManager.day - 1] = ogDescription;
            isDescribing = false;
        }

    }

    protected virtual void Interact() { }
}
