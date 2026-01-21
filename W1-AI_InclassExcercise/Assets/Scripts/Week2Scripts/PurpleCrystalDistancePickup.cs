using UnityEngine;
using NodeCanvas.Framework;

public class PurpleCrystalDistancePickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupRadius = 1.2f;   
    public int subRep = 10;

    [Header("References")]
    public Transform player;            
    public CrystalSpawner spawner;     

    private void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > pickupRadius) return;

        var bb = player.GetComponent<Blackboard>();
        if (bb != null)
        {
            float rep = bb.GetVariableValue<float>("reputation");
            rep = Mathf.Clamp(rep - subRep, 0f, 100f);
            bb.SetVariableValue("reputation", rep);
        }

        if (spawner != null) spawner.OnPurpleCollected();

        Destroy(gameObject);
    }
}