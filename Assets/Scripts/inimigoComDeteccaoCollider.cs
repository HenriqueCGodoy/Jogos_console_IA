using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(vida))]
public class inimigoComDeteccaoCollider : MonoBehaviour
{
    public Transform alvo;
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

    private List<Transform> enemiesDetected;

    private bool canFire = false;
    private bool turnTowardTarget = false;

    //-------- Bullet/Fire ------------------------

    public GameObject bulletInimigoPrefab;
    public Transform bulletInimigoSpawn;
    public float bulletInimigoSpeed = 6.0f;
    public int bulletDamage = 1;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    private Ray los;
    private RaycastHit los_hit;
    private bool hasLos = false;

    [SerializeField] Vector3 rayStartPos;
    [SerializeField] Vector3 rayEndPos;

    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        detectionCollider.radius = detectionRange;
        enemiesDetected = new List<Transform>();
        currentState = States.Patrol;
    }


    void Update()
    {
        if(enemiesDetected.Count > 0 && enemiesDetected[0] == null)
        {
            enemiesDetected.Clear();
            SetBoolsToFalse();
            currentState = States.Patrol;
        }
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
        if (canFire)
        {
            InvokeRepeating("FireInimigo", 0.2f, 3f);
        }
        else
        {
            CancelInvoke("FireInimigo");
        }

        if(turnTowardTarget)
        {
            RotateAgent();
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
        if (enemiesDetected.Count > 0)
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
        if (enemiesDetected.Count == 0)
        {
            currentState = States.Patrol;
        }
        else
        {
            alvo = enemiesDetected[0];
            if(alvo != null)
            {
                agente.SetDestination(alvo.position);
            
                if (Vector3.Distance(transform.position, alvo.position) <= firingDistance)
                {
                    currentState = States.Fire;
                }
            }
        }
    }

    private void FireBehaviour()
    {
        if(CheckRaycastHit() == "Inimigo")
        {
            hasLos = true;
        }
        else
        {
            hasLos = false;
        }
        if (Vector3.Distance(transform.position, alvo.position) > firingDistance || !hasLos)
        {
            currentState = States.Follow;
            canFire = false;
            turnTowardTarget = false;
        }
        else
        {
            agente.isStopped = true;
            canFire = true;
            turnTowardTarget = true;
        }
    }

    private void SetBoolsToFalse()
    {
        alvo = null;
        CancelInvoke("FireInimigo");
        canFire = false;
        turnTowardTarget = false;
        hasLos = false;
    }

    private String CheckRaycastHit()
    {
        rayStartPos = new Vector3(transform.position.x, 0.5f, transform.position.z);
        rayEndPos = new Vector3(alvo.transform.position.x - transform.position.x, 0.5f, alvo.transform.position.z - transform.position.z);
        los = new Ray(rayStartPos, rayEndPos);
        if (Physics.Raycast(los, out los_hit))
        {
            //Debug.Log(los_hit.collider.gameObject.name);
            return los_hit.collider.gameObject.tag;
        }
        else
        {
            return "no_collision";
        }
    }

    private void RotateAgent()
    {
        Vector3 newDir = alvo.position - transform.position;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Inimigo")
        {
            enemiesDetected.Add(other.transform);
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Inimigo")
    //    {
    //        enemiesDetected.Remove(other.transform);
    //    }
    // }

}
