using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Pig")]
    public class MateEffectAT : ActionTask
    {
        public float effectSeconds = 3f;
        public float jumpHeight = 1f;
        public float jumpSpeed = 8f;

        public ParticleSystem heartVfxPrefab;
        public AudioClip mateSfx;

        private Blackboard bb;
        private AudioSource audioSrc;

        private Transform modelTf;
        private Vector3 modelOriginalLocalPos;

        private ParticleSystem spawnedVfx;
        private float timer;

        protected override string OnInit()
        {
            if (agent != null)
            {
                bb = agent.GetComponent<Blackboard>();

                audioSrc = agent.GetComponent<AudioSource>();
                if (audioSrc == null)
                {
                    audioSrc = agent.gameObject.AddComponent<AudioSource>();
                }

                modelTf = agent.transform.Find("pCube6");
            }
            else
            {
                bb = null;
                audioSrc = null;
                modelTf = null;
            }

            return null;
        }

        protected override void OnExecute()
        {
            timer = 0f;

            if (modelTf != null)
            {
                modelOriginalLocalPos = modelTf.localPosition;
            }

            if (heartVfxPrefab != null && agent != null)
            {
                spawnedVfx = Object.Instantiate(heartVfxPrefab, agent.transform.position, Quaternion.identity);
                spawnedVfx.Play();
            }

            if (mateSfx != null && audioSrc != null)
            {
                audioSrc.PlayOneShot(mateSfx);
            }
        }

        protected override void OnUpdate()
        {
            timer += Time.deltaTime;

            if (modelTf != null)
            {
                float y = Mathf.Abs(Mathf.Sin(Time.time * jumpSpeed)) * jumpHeight;
                Vector3 p = modelOriginalLocalPos;
                p.y = modelOriginalLocalPos.y + y;
                modelTf.localPosition = p;
            }

            if (timer >= effectSeconds)
            {
                EndAction(true);
            }
        }

        protected override void OnStop()
        {
            if (modelTf != null)
            {
                modelTf.localPosition = modelOriginalLocalPos;
            }

            if (spawnedVfx != null)
            {
                Object.Destroy(spawnedVfx.gameObject);
            }
        }
    }
}