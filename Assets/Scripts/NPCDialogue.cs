using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Dialogue List Maker
[System.Serializable]
public class Dialogue
{
    public List<string> dialogueTxts;
}

[System.Serializable]
public class DialogueList
{
    public List<Dialogue> dialogueTitle;
}

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialogueP;

    [SerializeField]
    private DialogueList dialogueList = new DialogueList();
    [SerializeField]
    private Text dialogueTxt;

    bool dialogueVisible = false;

    int currentDialogueIndex,currentTxtIndex;

    // Dialogue First Call
    public void DialogueStart(int index)
    {
        dialogueVisible = true;
        currentDialogueIndex= index;
        currentTxtIndex= 0;

        dialogueP.SetActive(dialogueVisible);
        UpdateDialogue(currentDialogueIndex, currentTxtIndex);
    }

    // Change/Update Dialogue
    void UpdateDialogue(int index, int txtIndex)
    {
        if (dialogueList.dialogueTitle[index].dialogueTxts.Count == txtIndex)
        {
            dialogueVisible = false;
            DialogueEnd();
        }
        else
        {
            dialogueTxt.text = dialogueList.dialogueTitle[index].dialogueTxts[txtIndex];
        }
    }

    // Dialogue Finished
    public void DialogueEnd()
    {
        dialogueP.SetActive(dialogueVisible);
        PlayerController.isLock = dialogueVisible;
    }

    private void Update()
    {
        if (dialogueVisible)
        {
            // update on left mouse click
            if (Input.GetMouseButtonDown(0))
            {
                currentTxtIndex++;
                UpdateDialogue(currentDialogueIndex, currentTxtIndex);
            }
        }
    }
}
