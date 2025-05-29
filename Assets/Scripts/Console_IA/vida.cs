using System.Collections;
using UnityEngine;

public class vida : MonoBehaviour
{

    [SerializeField] private int maxLife = 3;
    [SerializeField] private int currentLife;
    [SerializeField] private float InvincibilityTime = 2f;
    private bool isInvincible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLife = maxLife;
    }


    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentLife -= damage;
            if (currentLife <= 0)
            {
                //Death code
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(InvicibilityCoroutine());
            }
        }
    }

    IEnumerator InvicibilityCoroutine()
    {
        isInvincible = true;
        //Frescura começa
        Material gameObjectMaterial = gameObject.GetComponent<MeshRenderer>().material;
        Color origColor = gameObjectMaterial.color;
        gameObjectMaterial.color = Color.yellow;
        //Frescura acaba
        yield return new WaitForSeconds(InvincibilityTime);
        isInvincible = false;
        //Ferscura começa de novo
        gameObjectMaterial.color = origColor;
        //Frescura acaba de novo
    }
}
