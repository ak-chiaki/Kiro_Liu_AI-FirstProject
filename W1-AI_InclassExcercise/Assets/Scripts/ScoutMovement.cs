using UnityEngine;
using UnityEngine.AI;

public class ScoutMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform leadScout;
    public float homePod;
    public float patrolRadius = 3f;          
    public float returnRadius = 1.5f;       
    public float sampleMaxDistance = 2f;     
    public float arriveThreshold = 0.3f;     


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
