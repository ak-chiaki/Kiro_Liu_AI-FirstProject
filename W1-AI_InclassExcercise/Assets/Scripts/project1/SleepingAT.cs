using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Pig")]
    public class SleepingAT : ActionTask
    {
        public float minSleep = 5f;
        public float maxSleep = 10f;

        public ParticleSystem zzzVfxPrefab;
        public AudioClip sleepSfx;

        private float sleepDuration;
        private float timer;

        private ParticleSystem spawnedVfx;
        private AudioSource audioSrc;
        private Blackboard bb;

        private Transform modelTf;//model transform pCube6
        private Quaternion modelOriginalRot;

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
                modelTf = agent.transform.Find("pCube6");
            }
            else
            {
                modelTf = null;
            }

            return null;
        }

        protected override void OnExecute()
        {
            if (bb != null)
            {
                bb.SetVariableValue("SleepTimer", 0f);
            }

            timer = 0f;
            sleepDuration = Random.Range(minSleep, maxSleep);

            if (modelTf != null)
            {
                modelOriginalRot = modelTf.localRotation;
                modelTf.localRotation = modelOriginalRot * Quaternion.Euler(90f, 0f, 0f);//rortate to lie down
            }

            if (zzzVfxPrefab != null && agent != null)
            {
                spawnedVfx = Object.Instantiate(zzzVfxPrefab, agent.transform.position, Quaternion.identity);
                spawnedVfx.Play();
            }

            if (sleepSfx != null && audioSrc != null)
            {
                audioSrc.PlayOneShot(sleepSfx);
            }
        }

        protected override void OnUpdate()
        {
            timer += Time.deltaTime;
            if (timer >= sleepDuration)
            {
                EndAction(true);
            }
        }

        protected override void OnStop()
        {
            if (modelTf != null)
            {
                modelTf.localRotation = modelOriginalRot;//restore original rotation
            }

            if (spawnedVfx != null)
            {
                Object.Destroy(spawnedVfx.gameObject);
            }
        }
    }
}