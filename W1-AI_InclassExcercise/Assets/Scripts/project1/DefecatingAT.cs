using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Pig")]
    public class DefecatingAT : ActionTask
    {
        public GameObject poopPrefab;
        public float poopDestroySeconds = 180f;
        public AudioClip poopSfx;

        private Renderer rend;
        private Color originalColor;
        private AudioSource audioSrc;

        private float timer;

        private Blackboard bb;

        protected override string OnInit()
        {
            if (agent != null)
            {
                rend = agent.GetComponentInChildren<Renderer>();
            }
            else
            {
                rend = null;
            }

            if (rend != null)
            {
                originalColor = rend.material.color;
            }

            if (agent != null)
            {
                audioSrc = agent.GetComponent<AudioSource>();
            }
            else
            {
                audioSrc = null;
            }

            if (audioSrc == null && agent != null)
            {
                audioSrc = agent.gameObject.AddComponent<AudioSource>();
            }

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
            timer = 0f;

            if (rend != null)
            {
                rend.material.color = Color.red;
            }

            if (poopPrefab != null && agent != null)
            {
                Vector3 pos = agent.transform.position - agent.transform.forward * 0.3f;
                GameObject poop = Object.Instantiate(poopPrefab, pos, Quaternion.identity);
                Object.Destroy(poop, poopDestroySeconds);
            }

            if (poopSfx != null && audioSrc != null)
            {
                audioSrc.PlayOneShot(poopSfx);
            }

            if (bb != null)
            {
                float accum = bb.GetVariableValue<float>("PoopAccum");
                accum = Mathf.Max(0f, accum - 30f);
                bb.SetVariableValue("PoopAccum", accum);
            }
        }

        protected override void OnUpdate()
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                EndAction(true);
            }
        }

        protected override void OnStop()
        {
            if (rend != null)
            {
                rend.material.color = originalColor;
            }
        }
    }
}