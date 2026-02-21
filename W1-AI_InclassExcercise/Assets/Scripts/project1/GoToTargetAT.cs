using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Pig")]
    public class GoToTargetAT : ActionTask
    {
        public BBParameter<Transform> target;
        public float arriveDistance = 0.6f;

        private NavMeshAgent nav;

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

            return null;
        }

        protected override void OnExecute()
        {
            if (target.value == null)
            {
                EndAction(false);
                return;
            }

            nav.isStopped = false;
            nav.SetDestination(target.value.position);
        }

        protected override void OnUpdate()
        {
            if (target.value == null)
            {
                EndAction(false);
                return;
            }

            if (nav.pathPending)
            {
                return;
            }

            if (nav.remainingDistance <= Mathf.Max(nav.stoppingDistance, arriveDistance))
            {
                EndAction(true);
            }
        }

        protected override void OnStop()
        {
            if (nav != null)
            {
                nav.ResetPath();
                nav.isStopped = true;
            }
        }
    }
}