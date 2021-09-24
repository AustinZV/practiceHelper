using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            enabled = false;
            GameObject.Find("Instructions").GetComponent<instructionsScript>().showInstructions();
        }
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
    
}
