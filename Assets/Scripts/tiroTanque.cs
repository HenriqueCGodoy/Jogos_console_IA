using Unity.VisualScripting;
using UnityEngine;

public class tiroTanque : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletVida = 2.0f;
    [SerializeField] private float bulletSpeed = 6.0f;
    [SerializeField] private int bulletDamage = 1;

    private float currentTime = 0.0f;
    public float ataqueTime = 2.0f;


    [SerializeField] private GameObject smokeParticlesPrefab;
    [SerializeField] private Transform smokeParticleSpawn;

    void Update()
    {
        if (currentTime <= ataqueTime)
        {
            currentTime = currentTime + Time.deltaTime;

        }
    }

    void FixedUpdate()
    {
        
        if ( (Input.GetKey(KeyCode.JoystickButton5) || Input.GetKey(KeyCode.Space))  && ( currentTime >= ataqueTime ) )
        {
            Fire();
            var smoke = Instantiate(smokeParticlesPrefab, smokeParticleSpawn.position, smokeParticleSpawn.rotation);
            Destroy(smoke, smoke.GetComponent<ParticleSystem>().main.startLifetime.constant);
            currentTime = 0;                        
        }
    }

    void Fire()
    {
        // Cria um Bullet a partir de BulletPrefab
        var newBullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        newBullet.GetComponent<bullet>().SetBulletSpeed(bulletSpeed);
        newBullet.GetComponent<bullet>().SetBulletDamage(bulletDamage);
        newBullet.GetComponent<bullet>().SetBulletLifeTime(bulletVida);
    }
}
