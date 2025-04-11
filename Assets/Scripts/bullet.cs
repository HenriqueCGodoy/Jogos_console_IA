using UnityEngine;

public class bullet : MonoBehaviour
{
    private float bulletInimigoSpeed = 6.0f;
    private int bulletDamage = 1;
    private GameObject shooter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Adiciona velocidade a Bullet
        GetComponent<Rigidbody>().linearVelocity = transform.forward * bulletInimigoSpeed;
        Destroy(gameObject, 30);
    }

    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == "Inimigo" && other.gameObject != shooter)
        {
            //Debug.Log("A bala de " + shooter.name + " atingiu " + other.gameObject.name);
            other.GetComponent<vida>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

    public void SetBulletSpeed(float speed)
    {
        bulletInimigoSpeed = speed;
        // Adiciona velocidade a Bullet
        GetComponent<Rigidbody>().linearVelocity = transform.forward * bulletInimigoSpeed;
    }

    public void SetBulletDamage(int damage)
    {
        bulletDamage = damage;
    }

    public void SetShooter(GameObject obj)
    {
        shooter = obj;
    }
}
