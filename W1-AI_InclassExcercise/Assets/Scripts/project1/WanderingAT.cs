using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Pig")]
    public class WanderingAT : ActionTask
    {
        public float wanderRadius = 6f;
        public float repathDistance = 0.5f;

        public float playerStaySeconds = 2f;
        public LayerMask groundMask = ~0;

        private NavMeshAgent nav;
        private Blackboard bb;
        private Camera cam;

        private bool isPlayerCommand = false;
        private bool isStaying = false;
        private float stayTimer = 0f;
        private Vector3 commandPos;

        protected override string OnInit()
        {
            if (agent != null)
            {
                nav = agent.GetComponent<NavMeshAgent>();
            }
            else
            {
                nav = null;
            }

            if (agent != null)
            {
                bb = agent.GetComponent<Blackboard>();
            }
            else
            {
                bb = null;
            }

            cam = Camera.main;

            return null;
        }

        protected override void OnExecute()
        {
            if (bb != null)
            {
                bb.SetVariableValue("IsWandering", true);
            }

            if (nav != null)
            {
                if (nav.isOnNavMesh) 
                {
                    nav.isStopped = false;
                }
            }
        }

        protected override void OnUpdate()
        {
            // player command has highest priority and player control
            if (cam != null && Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 500f, groundMask))
                {
                    commandPos = hit.point;
                    isPlayerCommand = true;
                    isStaying = false;
                    stayTimer = 0f;

                    if (nav != null)
                    {
                        if (nav.isOnNavMesh) 
                        {
                            nav.isStopped = false;
                            nav.SetDestination(commandPos);
                        }
                    }
                }
            }

            if (isPlayerCommand)
            {
                if (!isStaying)
                {
                    if (nav != null)
                    {
                        if (!nav.pathPending &&
                            nav.remainingDistance <= nav.stoppingDistance + 0.05f)
                        {
                            if (nav.isOnNavMesh) 
                            {
                                nav.isStopped = true;
                            }
                            isStaying = true;
                            stayTimer = 0f;
                        }
                    }
                }
                else
                {
                    stayTimer += Time.deltaTime;
                    if (stayTimer >= playerStaySeconds)
                    {
                        if (nav != null)
                        {
                            if (nav.isOnNavMesh) 
                            {
                                nav.isStopped = false;
                            }
                        }

                        isPlayerCommand = false;
                        isStaying = false;
                        stayTimer = 0f;
                    }
                }

                return;
            }

            // random wandering
            if (nav != null)
            {
                if (!nav.isOnNavMesh) 
                {
                    return;
                }

                if (!nav.pathPending &&
                    nav.remainingDistance <= nav.stoppingDistance + repathDistance)
                {
                    Vector3 random = Random.insideUnitSphere * wanderRadius;
                    random += nav.transform.position;

                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(random, out hit, wanderRadius, NavMesh.AllAreas))
                    {
                        nav.SetDestination(hit.position);
                    }
                }
            }
        }

        protected override void OnStop()
        {
            if (bb != null)
            {
                bb.SetVariableValue("IsWandering", false);
            }

            if (nav != null)
            {
                if (nav.isOnNavMesh) 
                {
                    nav.ResetPath();
                    nav.isStopped = true;
                }
            }

            isPlayerCommand = false;
            isStaying = false;
            stayTimer = 0f;
        }
    }
}