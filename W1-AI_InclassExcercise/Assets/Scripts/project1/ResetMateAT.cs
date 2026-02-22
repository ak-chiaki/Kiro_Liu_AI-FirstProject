using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Pig")]
    public class ResetMateAT : ActionTask
    {
        private Blackboard bb;

        protected override string OnInit()
        {
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
            if (bb != null)
            {
                bb.SetVariableValue("Happiness", 0f);
                bb.SetVariableValue("MateTarget", null);
            }

            EndAction(true);
        }
    }
}