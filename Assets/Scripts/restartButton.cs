using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restartButton : MonoBehaviour
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
            GameObject.Find("Listner").GetComponent<listner>().restartPlaying();
        }
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
