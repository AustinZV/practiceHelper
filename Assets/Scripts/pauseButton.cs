using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseButton : MonoBehaviour
{
    public Text buttonText;
    private bool paused;


    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            enabled = false;
            if (paused)
            {
                paused = false;
                GameObject.Find("Listner").GetComponent<listner>().paused = false;
                buttonText.text = "Pause Audio";
            }
            else
            {
                paused = true;
                GameObject.Find("Listner").GetComponent<listner>().paused = true;
                buttonText.text = "Continue Audio";
                
            }
            //GameObject.Find("Listner").GetComponent<listner>().restartPlaying();
        }
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
