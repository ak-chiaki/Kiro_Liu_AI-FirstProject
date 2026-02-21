using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Pig")]
    public class StatsAT : ActionTask
    {
        public float happinessGainPerSec = 1f;
        public float wanderingSatietyDrainPerSec = 0.25f;

        protected override void OnExecute() { }

        protected override void OnUpdate()
        {
            Blackboard bb;

            if (agent != null)
            {
                bb = agent.GetComponent<Blackboard>();
            }
            else
            {
                bb = null;
            }

            if (bb == null)
            {
                return;
            }

            float dt = Time.deltaTime;

            bb.SetVariableValue("EatTimer", bb.GetVariableValue<float>("EatTimer") + dt);
            bb.SetVariableValue("DrinkTimer", bb.GetVariableValue<float>("DrinkTimer") + dt);
            bb.SetVariableValue("SleepTimer", bb.GetVariableValue<float>("SleepTimer") + dt);

            float h = bb.GetVariableValue<float>("Happiness");
            h = Mathf.Min(100f, h + happinessGainPerSec * dt);
            bb.SetVariableValue("Happiness", h);

            bool isWandering = bb.GetVariableValue<bool>("IsWandering");
            if (isWandering)
            {
                float sBefore = bb.GetVariableValue<float>("Satiety");
                float sAfter = Mathf.Max(0f, sBefore - wanderingSatietyDrainPerSec * dt);
                bb.SetVariableValue("Satiety", sAfter);

                float consumed = sBefore - sAfter;
                if (consumed > 0f)
                {
                    float accum = bb.GetVariableValue<float>("PoopAccum");
                    bb.SetVariableValue("PoopAccum", accum + consumed);
                }
            }
        }
    }
}