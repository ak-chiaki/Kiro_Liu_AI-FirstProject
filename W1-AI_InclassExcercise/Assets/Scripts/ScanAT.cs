using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{

    public class ScanAT : ActionTask
    {
        public Color scanColour;
        public int numberOfScanCirclePoints;
        public LayerMask targetMask;


        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {


        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {

            Collider[] objectsInRange = Physics.OverlapSphere(agent.transform.position, 3f, targetMask);
            foreach (Collider objectInRange in objectsInRange)
            {
                Blackboard lighthouseBlackboard = objectInRange.GetComponentInParent<Blackboard>();
                if (lighthouseBlackboard == null)
                {
                    Debug.LogError("Failed to get lighthouse blackboard off of lighthouse layered object[" + objectInRange.gameObject.name + "].");
                }
                float repairValue = lighthouseBlackboard.GetVariableValue<float>("repairValue");



            }


        }
    }
}