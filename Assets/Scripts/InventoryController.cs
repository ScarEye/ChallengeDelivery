using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private GameObject itemSelector;

    public List<GameObject> items;
    public List<Sprite> invenotryObjs;

    public int slotIndex = 0;

    // slots in which you want to add the inventory
    public int hoeSlot = 0, mushroomSlot = 1;

    // Start is called before the first frame update
    void Start()
    {
        itemSelector.transform.position = items[slotIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // update selected slot on scroll
        float scrollV = Input.GetAxis("Mouse ScrollWheel");
        if (scrollV<0)
        {
            if(slotIndex<items.Count-1)
                slotIndex++;
        }
        else if (scrollV > 0)
        {
            if (slotIndex > 0)
                slotIndex--;
        }
        itemSelector.transform.position = items[slotIndex].transform.position;
    }

    // add items in inventory slot
    public void AddItem(int index, int spriteIndex)
    {
        if (int.Parse(items[index].transform.GetChild(2).GetComponent<Text>().text)==0)
        {
            items[index].transform.GetChild(1).gameObject.SetActive(true);
            items[index].transform.GetChild(2).gameObject.SetActive(true);
            items[index].transform.GetChild(1).GetComponent<Image>().sprite = invenotryObjs[spriteIndex];
        }
        items[index].transform.GetChild(2).GetComponent<Text>().text = (int.Parse(items[index].transform.GetChild(2).GetComponent<Text>().text) + 1).ToString();
    }

    // set current slot index
    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }

    // remove item in inventory slot
    public void RemoveItem(int index)
    {
        if (int.Parse(items[index].transform.GetChild(2).GetComponent<Text>().text) > 0)
        {
            items[index].transform.GetChild(2).GetComponent<Text>().text = (int.Parse(items[index].transform.GetChild(2).GetComponent<Text>().text) - 1).ToString();
            if (int.Parse(items[index].transform.GetChild(2).GetComponent<Text>().text) == 0)
            {
                items[index].transform.GetChild(1).gameObject.SetActive(false);
                items[index].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
}
