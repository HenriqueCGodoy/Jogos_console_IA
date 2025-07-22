using UnityEngine;

public class bolaMove : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 15f;
    float rotateHorizontal = 0f;
    float rotateVertical = 0f;

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        if ((Input.GetAxis("HorizontalRight") != 0) || Input.GetAxis("VerticalRight") != 0)
        {
            rotateHorizontal = Input.GetAxis("HorizontalRight") * rotationSpeed * Time.deltaTime;
            rotateVertical = Input.GetAxis("VerticalRight") * rotationSpeed * Time.deltaTime;
        }
        else
        {
            if (Input.GetKey(KeyCode.L))
                rotateHorizontal = 1 * rotationSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.J))
                rotateHorizontal = -1 * rotationSpeed * Time.deltaTime;
            else
                rotateHorizontal = 0;

            if (Input.GetKey(KeyCode.I))
                rotateVertical = 1 * rotationSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.K))
                rotateVertical = -1 * rotationSpeed * Time.deltaTime;
            else
                rotateVertical = 0;
        }
        transform.Translate(moveHorizontal, 0, moveVertical);
        transform.Rotate(rotateVertical, rotateHorizontal, 0);
       
    }
}
