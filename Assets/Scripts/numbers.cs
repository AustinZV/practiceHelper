using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Windows.Input;

public class numbers : MonoBehaviour
{
    public TMP_Text selfText;

    private int id;
    private int total;
    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("space") && !GameObject.Find("Listner").GetComponent<listner>().paused)
        {
            if (counter + id == total)
            {
                Destroy(gameObject);
            }
            counter++;
        }
        
    }

    public void nameNum(int i, int numTotal)
    {
        selfText.text = i.ToString();
        id = i;
        total = numTotal;
    }
}
