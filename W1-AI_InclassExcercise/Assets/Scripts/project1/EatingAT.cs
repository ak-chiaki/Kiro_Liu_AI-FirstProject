using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Pig")]
    public class EatingAT : ActionTask
    {
        public BBParameter<Transform> foodTarget;

        public float arriveDistance = 0.8f;
        public float eatSeconds = 5f;
        public float maxStateTime = 20f; // prevent getting stuck

        public ParticleSystem eatVfxPrefab;
        public AudioClip eatSfx;

        private NavMeshAgent nav;
        private Renderer rend;
        private Color originalColor;
        private AudioSource audioSrc;

        private bool arrived;
        private float eatTimer;
        private float stateTimer;
        private ParticleSystem spawnedVfx;

        private Blackboard bb;

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

            if (nav == null)
            {
                return "No NavMeshAgent on Agent.";
            }

            if (agent != null)
            {
                rend = agent.GetComponentInChildren<Renderer>();
            }
            else
            {
                rend = null;
            }

            if (rend != null)
            {
                originalColor = rend.material.color;
            }

            if (agent != null)
            {
                audioSrc = agent.GetComponent<AudioSource>();
            }
            else
            {
                audioSrc = null;
            }

            if (audioSrc == null && agent != null)
            {
                audioSrc = agent.gameObject.AddComponent<AudioSource>();
            }

            if (agent != null)
            {
                bb = agent.GetComponent<Blackboard>();
            }
            else
            {
                bb = null;
            }

            return null;
        }

        protected override void OnExecute()
        {
            arrived = false;
            eatTimer = 0f;
            stateTimer = 0f;

            if (foodTarget.value == null)
            {
                EndAction(false);
                return;
            }

            if (rend != null)
            {
                rend.material.color = Color.green;
            }

            if (nav != null)
            {
                nav.isStopped = false;
                nav.ResetPath();
                nav.SetDestination(foodTarget.value.position);
            }
        }

        protected override void OnUpdate()
        {
            stateTimer += Time.deltaTime;
            if (stateTimer >= maxStateTime)
            {
                EndAction(true);
                return;
            }

            if (foodTarget.value == null)
            {
                EndAction(false);
                return;
            }

            if (!arrived)
            {
                if (nav.pathPending)
                {
                    return;
                }

                if (nav.remainingDistance <= Mathf.Max(nav.stoppingDistance, arriveDistance))
                {
                    arrived = true;
                }

                if (arrived)
                {
                    nav.isStopped = true;

                    if (eatVfxPrefab != null && agent != null)
                    {
                        spawnedVfx = Object.Instantiate(eatVfxPrefab, agent.transform.position, Quaternion.identity);
                        spawnedVfx.Play();
                    }
                    if (eatSfx != null && audioSrc != null)
                    {
                        audioSrc.PlayOneShot(eatSfx);
                    }
                }
                return;
            }

            eatTimer += Time.deltaTime;
            if (eatTimer >= eatSeconds)
            {
                if (bb != null)
                {
                    bb.SetVariableValue("Satiety", 100f);
                    bb.SetVariableValue("EatTimer", 0f);
                }
                EndAction(true);
            }
        }

        protected override void OnStop()
        {
            if (nav != null)
            {
                nav.isStopped = false;
                nav.ResetPath();
            }
            if (spawnedVfx != null)
            {
                Object.Destroy(spawnedVfx.gameObject);
            }
            if (rend != null)
            {
                rend.material.color = originalColor;
            }
        }
    }
}