using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlePlayer : MonoBehaviour
{
    public float speed = 20.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float translateVer = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float translateHor = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(translateHor, 0, translateVer);
    }
}