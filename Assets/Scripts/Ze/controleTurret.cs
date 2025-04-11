using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleTurret : MonoBehaviour
{
    public float girar = 60.0f;
    public Transform axis;

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        float rotate = Input.GetAxis("HorizontalRight") * girar * Time.deltaTime;

        if (Input.GetKey(KeyCode.L))
        {
            rotate = 1 * girar * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.J))
        {
            rotate = -1 * girar * Time.deltaTime;
        }

        axis.transform.Rotate(0, rotate, 0);
    }
}