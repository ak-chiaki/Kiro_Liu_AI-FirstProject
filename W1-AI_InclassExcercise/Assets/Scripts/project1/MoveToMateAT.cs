using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Pig")]
    public class MoveToMateAT : ActionTask
    {
        public float arriveDistance = 3f;
        public float maxStateTime = 20f;

        private NavMeshAgent nav;
        private Blackboard bb;

        private float stateTimer;

        protected override string OnInit()
        {
            if (agent != null)
            {
                nav = agent.GetComponent<NavMeshAgent>();
                bb = agent.GetComponent<Blackboard>();
            }
            else
            {
                nav = null;
                bb = null;
            }

            return null;
        }

        protected override void OnExecute()
        {
            stateTimer = 0f;

            if (nav == null || bb == null)
            {
                EndAction(false);
                return;
            }

            GameObject otherPig = FindOtherPig();
            if (otherPig == null)
            {
                EndAction(false);
                return;
            }

            bb.SetVariableValue("MateTarget", otherPig);

            nav.isStopped = false;
            nav.ResetPath();
            nav.SetDestination(otherPig.transform.position);
        }

        protected override void OnUpdate()
        {
            stateTimer += Time.deltaTime;
            if (stateTimer >= maxStateTime)
            {
                EndAction(true);
                return;
            }

            if (nav == null || bb == null)
            {
                EndAction(false);
                return;
            }

            GameObject targetObj = bb.GetVariableValue<GameObject>("MateTarget");

            if (targetObj == null)
            {
                EndAction(false);
                return;
            }

            nav.SetDestination(targetObj.transform.position);

            if (nav.pathPending)
            {
                return;
            }

            float dist = Vector3.Distance(agent.transform.position, targetObj.transform.position);
            if (dist <= arriveDistance)
            {
                nav.isStopped = true;
                EndAction(true);
            }
        }

        protected override void OnStop()
        {
            if (nav != null)
            {
                nav.isStopped = true;
                nav.ResetPath();
            }
        }

        private GameObject FindOtherPig()
        {
            GameObject[] pigs = GameObject.FindGameObjectsWithTag("Pig");
            if (pigs == null || pigs.Length == 0)
            {
                return null;
            }

            for (int i = 0; i < pigs.Length; i++)
            {
                if (pigs[i] != null && pigs[i] != agent.gameObject)
                {
                    return pigs[i];
                }
            }

            return null;
        }
    }
}