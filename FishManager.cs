using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static int day = 1;

    public LayerMask water;

    bool isBobberInWater = false;
    bool isFishBiting = false;

    public int chanceSeconds = 8;

    float bobberRadius = .16f;

    public List<Fish> fishTypes;

    public GameObject inventoryObject;

    void FixedUpdate()
    {
        //checks if bobber exists
        if(Fishing.currentBobber != null && !isFishBiting)
        {
            //does a fishing check if bobber is near water
            isBobberInWater = Physics.CheckSphere(Fishing.currentBobber.transform.position, bobberRadius, water);
            if (isBobberInWater)
            {
                //has a random chance to catch fish
                int chance = Random.Range(1, chanceSeconds * 60);
                if (chance == 1)
                {
                    isFishBiting = true;
                }
            }
            //when the fish is biting
        } else if(isFishBiting)
        {
            //checks if bobber is touching the water and jumps if it is
            if(Fishing.currentBobber != null && Physics.CheckSphere(Fishing.currentBobber.transform.position, bobberRadius, water))
            {
                float bobberJumpForce = Random.Range(10f, 50f);
                Fishing.currentBobber.GetComponent<Rigidbody>().AddForce(0f, bobberJumpForce, 0f);
            }

            //checks if player caught the fish
            if(Fishing.attemptedCatch)
            {
                
                Inventory.instance.Add(pickRandomFish());
                ActionScreenController.PlayAnimation();

                isFishBiting = false;
                Fishing.SetAttemptedCatch(false);
            }
        } else
        {
            isBobberInWater = false;
            Fishing.SetAttemptedCatch(false);
        }
    }

    //picks random fish type and makes a new one
    private Fish pickRandomFish()
    {
        int chosenType = Random.Range(0, fishTypes.Count);
        Fish chosenFish = Instantiate(fishTypes[chosenType], inventoryObject.transform);
        chosenFish.PickName();
        

        return chosenFish;
    }

}
