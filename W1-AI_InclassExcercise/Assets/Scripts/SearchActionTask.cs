using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{

	public class SearchActionTask : ActionTask
	{

		protected override string OnInit()
		{
			return null;
		}

		protected override void OnExecute()
		{
            //Choose a random destination on the navmesh
            //Set the path to that position

        }

		protected override void OnUpdate()
		{
			//When they have arrived then end the task

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