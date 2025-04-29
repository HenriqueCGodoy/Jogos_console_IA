using UnityEngine;

public class movimentoTanque : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20.0f;
    [SerializeField] private float rotateSpeed = 20.0f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float rotate = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;

        transform.Translate(0, 0, move);
        transform.Rotate(0, rotate, 0);
    }
}
