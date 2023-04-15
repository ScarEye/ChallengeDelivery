using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Animator anim;
    public Rigidbody2D rb;

    GameController controller;
    InventoryController inventoryController;
    NPCDialogue npcDialogue;

    Vector2 movement;
    
    GameObject colliderObj = null;


    public static bool isLock;

    private void Start()
    {
        controller = GameObject.FindObjectOfType<GameController>();
        inventoryController = GameObject.FindObjectOfType<InventoryController>();
        npcDialogue = GameObject.FindObjectOfType<NPCDialogue>();
        isLock = false;
    }
    private void Update()
    {
        // controls lock
        if (!isLock)
        {
            // get Horizontal movement value
            movement.x = Input.GetAxisRaw("Horizontal");
            // get Vertical movement value
            movement.y = Input.GetAxisRaw("Vertical");

            // animate the character
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);

            // pressed E
            if (Input.GetKeyDown(KeyCode.E))
            {
                // interact object not null
                if (colliderObj != null)
                {
                    // Player is in bed collider or near bed
                    if (colliderObj.tag == "Bed")
                    {
                        StartCoroutine(controller.NextDay());
                    }
                    // Player is in Mushrooms collider or near Mushrooms
                    else if (colliderObj.tag == "Mushrooms")
                    {
                        if (inventoryController.slotIndex == inventoryController.hoeSlot)
                        {
                            int hoeN = int.Parse(inventoryController.items[inventoryController.slotIndex].transform.GetChild(2).GetComponent<Text>().text);
                            if (hoeN > 0)
                            {
                                Destroy(colliderObj);
                                inventoryController.AddItem(inventoryController.mushroomSlot,1);
                                anim.SetTrigger("PickMushroom");
                            }

                        }
                    }
                    // Player is in Hoe collider or near Hoe
                    else if (colliderObj.tag == "Hoe")
                    {
                        Destroy(colliderObj);
                        inventoryController.AddItem(inventoryController.hoeSlot,0);
                    }
                    // Player is in NPC collider or near NPC
                    else if (colliderObj.tag == "NPC")
                    {
                        DialogueInit();
                    }
                }
            }
        }

    }

    private void FixedUpdate()
    {
        // move player with x-axis and y-axis
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliderObj = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliderObj = null;
    }

    // dialogue start
    public void DialogueInit()
    {
        isLock = true;

        int mushroomsN = int.Parse(inventoryController.items[inventoryController.mushroomSlot].transform.GetChild(2).GetComponent<Text>().text);
        int hoeN = int.Parse(inventoryController.items[inventoryController.hoeSlot].transform.GetChild(2).GetComponent<Text>().text);

        if (mushroomsN >= 5)
        {
            npcDialogue.DialogueStart(2);
            for (int i=0;i<5;i++)
            {
                inventoryController.RemoveItem(inventoryController.mushroomSlot);
            }
            controller.AddMoney(100);
        }
        else if (hoeN > 0)
        {
            npcDialogue.DialogueStart(1);
        }
        else
        {
            npcDialogue.DialogueStart(0);
        }
    }
}
