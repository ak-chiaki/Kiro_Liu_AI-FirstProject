using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{

	public class AwaitActionTask : ActionTask
	{
		public float maxWaitTime;
		public float minWaitTime;

		protected override string OnInit(){
            return null;
		}


		protected override void OnExecute()
		{

		}


		protected override void OnUpdate()
		{
			//Wait a random amount of time between two values

        }

        //Called when the task is disabled.
        protected override void OnStop(){
			
		}

		//Called when the task is paused.
		protected override void OnPause(){
			
		}
	}
}