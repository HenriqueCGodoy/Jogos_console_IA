using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class inimigocontrole : MonoBehaviour
{
    [SerializeField] private int damage = 15;
    [SerializeField] private float speed;
    //[SerializeField] private GameObject player;
    private NavMeshAgent agent;
    //private Rigidbody rb;
    //[SerializeField] float frontDetectionRange;
    //[SerializeField] float backDetectionRange;
    private enum States { Walking, Attacking };
    [SerializeField] private States currentState;
    private enum Destinations { Player, Pos1, Pos2 };
    private Destinations currentDestinaton;
    [SerializeField] private Vector3 position1 = Vector3.zero;
    [SerializeField] private Vector3 position2 = Vector3.zero;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        //Set speed to nav mesh component
        agent.speed = speed;

        //Set default state
        currentState = States.Walking;

        //Set walking positions to the length of the platform if both are zero
        if (position1 == Vector3.zero && position2 == Vector3.zero)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                Transform platform = hit.transform;
                float maxX = platform.position.x + platform.localScale.x / 2;
                float minX = platform.position.x - platform.localScale.x / 2;
                float newY = platform.position.y + platform.localScale.y / 2;
                position1 = new Vector3(minX, newY, 0);
                position2 = new Vector3(maxX, newY, 0);
            }
        }

        //Target first walking position
        agent.SetDestination(position1);
        currentDestinaton = Destinations.Pos1;

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case States.Walking:
                WalkingBehaviour();
                break;
            case States.Attacking:
                AttackingBehaviour();
                break;
            default:
                Debug.Log("INVALID STATE ON ENEMY: " + gameObject.name);
                break;
        }
    }

    void WalkingBehaviour()
    {
        //Transitions
        if (false)
        {

        }
        //State logic
        else
        {
            if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance + 1.0f)
            {
                transform.Rotate(0, 180, 0);
                if (currentDestinaton == Destinations.Pos1)
                {
                    agent.SetDestination(position2);
                    currentDestinaton = Destinations.Pos2;
                }
                else
                {
                    agent.SetDestination(position1);
                    currentDestinaton = Destinations.Pos1;
                }
            }
        }
    }

    void AttackingBehaviour()
    {
        //Transitions
        if (false)
        {

        }
        //State logic
        else
        {

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<vida>().TakeDamage(damage);
        }
    }

}
