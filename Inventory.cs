using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Warning warning");
            return;
        }

        instance = this;
    }
    #endregion

    public Transform itemGroup;
    public CameraMovement movement;

    public GameObject inventoryUI;

    public int inventorySpace = 29;
    public InventorySlot[] slots;

    public List<Fish> fishList;

    private void Start()
    {
        slots = itemGroup.GetComponentsInChildren<InventorySlot>();

    }

    private void Update()
    {
        if(Input.GetButtonDown("Close/Open Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

        if (inventoryUI.activeSelf == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            movement.enabled = true;
        } else
        {
            Cursor.lockState = CursorLockMode.None;
            movement.enabled = false;
        }
    }

    public void Add(Fish newFish)
    {
        if(fishList.Count >= inventorySpace)
        {
            Debug.Log("Inventory full");
            return;
        } else
        {
            fishList.Add(newFish);
            slots[fishList.Count - 1].AddItem(newFish);
        }
    }

    public string GetName(int index)
    {
        return fishList[index].fishName;
    }
}
