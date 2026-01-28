using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    public class IncreaseValueAT : ActionTask
    {
        public float rateOfChange;
        public BBParameter<float> currentValue;
        //public string variableName;
        // public float rateOfChange;

        // private float value;
        // private Blackboard agentBlackboard;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            //agentBlackboard = agent.GetComponent<Blackboard>();
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

           // value = agentBlackboard.GetVariableValue<float>(variableName);
          currentValue.value += rateOfChange * Time.deltaTime;
            //agentBlackboard.SetVariableValue(variableName, value);

        }

        //Called when the task is disabled.
        protected override void OnStop()
        {

        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }
    }
}
