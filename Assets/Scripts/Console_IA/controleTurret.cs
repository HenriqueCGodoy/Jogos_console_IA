using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleTurret : MonoBehaviour
{
    public float girar = 60.0f;
    public Transform turretAxis;
    public Transform cannonAxis;

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        //Controller
        float rotateTurret = Input.GetAxis("HorizontalRight") * girar * Time.deltaTime;
        float rotateCannon = -Input.GetAxis("VerticalRight") * girar * Time.deltaTime;

        //Keyboard
        if (Input.GetKey(KeyCode.L))
        {
            rotateTurret = 1 * girar * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.J))
        {
            rotateTurret = -1 * girar * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.I))
        {
            rotateCannon = -1 * girar * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            rotateCannon = 1 * girar * Time.deltaTime;
        }

        turretAxis.transform.Rotate(0, rotateTurret, 0);
        cannonAxis.transform.Rotate(rotateCannon, 0, 0);
    }
}