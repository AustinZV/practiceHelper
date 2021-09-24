using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Input;
using UnityEngine.UI;
using TMPro;

public class instructionsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            var listner = GameObject.Find("Listner").GetComponent<listner>();
            if (listner.parts != null && !listner.paused)
            {
                gameObject.GetComponent<TextMeshPro>().color = new Color(0, 0, 0, 0);
            }
        }
    }

    public void showInstructions()
    {
        gameObject.GetComponent<TextMeshPro>().color = Color.black;
    }
}
