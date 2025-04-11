using UnityEngine;

public class vida : MonoBehaviour
{

    [SerializeField] private int maxLife = 3;
    private int currentLife;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLife = maxLife;
    }


    public void TakeDamage(int damage)
    {
        //Debug.Log(gameObject.name + "TOOK DAMAGE !");
        currentLife -= damage;
        if(currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
