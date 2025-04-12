using System;
using System.Diagnostics;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(vida))]
public class inimigoComDeteccaoCollider : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private NavMeshAgent agente;

    [SerializeField] NavMeshSurface surface;
    [SerializeField] SphereCollider detectionCollider;
    [SerializeField] float detectionRange = 20.0f;
    [SerializeField] float firingDistance = 10.0f;
    [SerializeField] float patrolDistance = 10.0f;
    [SerializeField] float turnRate = 10.0f;
    private NavMeshHit hit;
    private Vector3 novoDestino;

    private enum States {Patrol, Follow, Fire};
    [SerializeField] private States currentState;

    private bool turnTowardTarget = false;

    public GameObject bulletInimigoPrefab;
    public Transform bulletInimigoSpawn;
    public float bulletInimigoSpeed = 6.0f;
    public int bulletDamage = 1;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    [SerializeField] Vector3 rayStartPos;
    [SerializeField] Vector3 rayEndPos;

    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        detectionCollider.radius = detectionRange;
        target = null;
        currentState = States.Patrol;
    }

    void Update()
    {
        switch(currentState)
        {
            case States.Patrol:
                PatrolBehaviour();
                break;
            case States.Follow:
                FollowBehaviour();
                break;
            case States.Fire:
                FireBehaviour();
                break;
        }
    }

    void FixedUpdate()
    { 
        if (target != null)
        {
            if (turnTowardTarget)
            {
                RotateAgent();
            }
        }
    }

    void FireInimigo()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            // Cria um Bullet a partir de BulletPrefab
            GameObject newBullet = Instantiate(bulletInimigoPrefab, bulletInimigoSpawn.position, bulletInimigoSpawn.rotation);
            bullet bulletScript = newBullet.GetComponent<bullet>();
            bulletScript.SetBulletDamage(bulletDamage);
            bulletScript.SetBulletSpeed(bulletInimigoSpeed);
            bulletScript.SetShooter(gameObject);
        }
    }

    private void PatrolBehaviour()
    {
        if (target != null)
        {
            currentState = States.Follow;
        }
        else
        {
            if (Vector3.Distance(agente.destination, transform.position) <= agente.stoppingDistance + 0.1f)
            {
                novoDestino = NovaPosicaoAleatoria();
                agente.SetDestination(novoDestino);
            }
        }
    }

    private void FollowBehaviour()
    {
        if (target == null)
        {
            currentState = States.Patrol;
        }
        else
        {
            agente.SetDestination(target.transform.position);
            
            if (Vector3.Distance(transform.position, target.transform.position) <= firingDistance)
            {
                    currentState = States.Fire;
            }
        }
    }

    private void FireBehaviour()
    {
        if(target == null)
        {
            SetBoolsToFalse();
            currentState = States.Patrol;
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > firingDistance)
            {
                currentState = States.Follow;
                CancelInvoke("FireInimigo");
                turnTowardTarget = false;
                agente.isStopped = false;
            }
            else
            {
                agente.isStopped = true;
                InvokeRepeating("FireInimigo", 0.2f, 3f);
                turnTowardTarget = true;
            }
        }
    }

    private void SetBoolsToFalse()
    {
        target = null;
        CancelInvoke("FireInimigo");
        turnTowardTarget = false;
        agente.isStopped = false;
    }

    private void RotateAgent()
    {
        Vector3 newDir = target.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(newDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnRate);
    }

    private Vector3 NovaPosicaoAleatoria()
    {
        Vector2 random2DDirection = UnityEngine.Random.insideUnitCircle;
        Vector2 random2DPosition = random2DDirection * patrolDistance;
        Vector3 finalPosition = new Vector3(transform.position.x + random2DPosition.x, transform.position.y, transform.position.z + random2DPosition.y);
        NavMesh.SamplePosition(finalPosition, out hit, 5, 1);
        Vector3 finalMeshPosition = hit.position;
        return finalMeshPosition;
    }

    void OnTriggerStay(Collider other)
    {
        if (target == null && other.gameObject.tag == "Inimigo")
        {
            target = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Inimigo" && other.gameObject == target.gameObject)
        {
            target = null;
        }
    }

}
