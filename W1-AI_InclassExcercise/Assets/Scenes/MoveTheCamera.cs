using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {

	public class MoveTheCamera : ActionTask {

        public Transform cameraTransform;
        public float moveSpeed = 3f;


        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {

			if (cameraTransform == null)
			{
				return "Camera Transform is not exsisted.";
			}
                return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
	
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            cameraTransform.position +=
                cameraTransform.forward * moveSpeed * Time.deltaTime;
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}