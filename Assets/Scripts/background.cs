using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    public float endScale;
    public float speed;

    private Camera cam;
    private int counter;
    private Renderer rend;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
        counter = 0;
        rend = GetComponent<Renderer>();
        color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        rend.material.color = color;
        transform.localScale = new Vector3(0f, 0f, 1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        transform.localScale += new Vector3(0.1f*speed, 0.1f*speed, 0);
        transform.position += new Vector3(0, 0, 0.01f);
        if (transform.localScale.x > endScale)
        {
            cam.backgroundColor = color;
            Destroy(gameObject);
        }
        //
        
        //rend.material.color = new Color(Random.Range(0f, 1f), 0.3F, 0.4F, 0.5F);
    }
}
