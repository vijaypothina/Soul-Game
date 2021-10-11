using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out MuryotaisuController muryotaisuController))
        {
            muryotaisuController.Interactable = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out MuryotaisuController muryotaisuController))
        {
            if (muryotaisuController.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                muryotaisuController.Interactable = null;
            }
        }
    }
    
    public void Interact(MuryotaisuController muryotaisuController)
    {
        muryotaisuController.DialogueUI.ShowDialogue(dialogueObject);
    }
}
