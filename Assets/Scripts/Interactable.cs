using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMassage;
    //public bool useEvents;
    //[SerializeField]
    // Start is called before the first frame update

    //public virtual string OnLook() 
    //{
    //    return promptMassage;
    //}

    // Update is called once per frame
    public void BaseInteract() 
    {
        //if (useEvents)
          //  GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    protected virtual void Interact() 
    {

    }
}
