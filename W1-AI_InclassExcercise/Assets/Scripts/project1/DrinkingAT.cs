using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Pig")]
    public class DrinkingAT : ActionTask
    {
        public BBParameter<Transform> riverTarget;

        public float arriveDistance = 0.8f;
        public float drinkSeconds = 5f;
        public float maxStateTime = 20f; // prevent getting stuck

        public ParticleSystem drinkVfxPrefab;
        public AudioClip drinkSfx;

        private NavMeshAgent nav;
        private Renderer rend;
        private Color originalColor;
        private AudioSource audioSrc;

        private bool arrived;
        private float drinkTimer;
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
            drinkTimer = 0f;
            stateTimer = 0f;

            if (riverTarget.value == null)
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
                nav.SetDestination(riverTarget.value.position);
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

            if (riverTarget.value == null)
            {
                EndAction(false);
                return;
            }

            if (nav == null)
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

                    if (drinkVfxPrefab != null && agent != null)
                    {
                        spawnedVfx = Object.Instantiate(drinkVfxPrefab, agent.transform.position, Quaternion.identity);
                        spawnedVfx.Play();
                    }
                    if (drinkSfx != null && audioSrc != null)
                    {
                        audioSrc.PlayOneShot(drinkSfx);
                    }
                }
                return;
            }

            drinkTimer += Time.deltaTime;
            if (drinkTimer >= drinkSeconds)
            {
                if (bb != null)
                {
                    bb.SetVariableValue("DrinkTimer", 0f);
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