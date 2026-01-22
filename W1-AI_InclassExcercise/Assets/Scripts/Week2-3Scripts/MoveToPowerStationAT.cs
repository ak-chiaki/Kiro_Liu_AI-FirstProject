using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class MoveToPowerStationAT : ActionTask {

        public BBParameter<Transform> powerStation;
        public BBParameter<float> moveSpeed;
        public BBParameter<float> reachDistance;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            Vector3 to = powerStation.value.position - agent.transform.position;
            to.y = 0f;

            if (to.magnitude <= reachDistance.value) return;

            agent.transform.position += to.normalized * moveSpeed.value * Time.deltaTime;
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}