using UnityEngine;

public class CrystalSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject yellowPrefab;
    public GameObject purplePrefab;

    [Header("Spawn Area")]
    public float halfRange = 1.5f; 
    public float groundY = 0.5f;

    [Header("Center")]
    public Transform player; 

    [Header("Read only")]
    public Transform yellowInstance;
    public Transform purpleInstance;

    private void Start()
    {
        EnsureTwoCrystals();
    }

    public void OnYellowCollected()
    {
        yellowInstance = null;
        EnsureTwoCrystals();
    }

    public void OnPurpleCollected()
    {
        purpleInstance = null;
        EnsureTwoCrystals();
    }

    private void EnsureTwoCrystals()
    {
        if (yellowInstance == null)
            yellowInstance = SpawnYellow();
        if (purpleInstance == null)
            purpleInstance = SpawnPurple();
    }

    private Transform SpawnYellow()
    {
        Vector3 pos = RandomPos();
        GameObject go = Instantiate(yellowPrefab, pos, Quaternion.identity);

        var pickup = go.GetComponent<YellowCrystalDistancePickup>();
        if (pickup != null)
        {
            pickup.player = player;
            pickup.spawner = this;
        }

        return go.transform;
    }

    private Transform SpawnPurple()
    {
        Vector3 pos = RandomPos();
        GameObject go = Instantiate(purplePrefab, pos, Quaternion.identity);

        var pickup = go.GetComponent<PurpleCrystalDistancePickup>();
        if (pickup != null)
        {
            pickup.player = player;
            pickup.spawner = this;
        }

        return go.transform;
    }

    private Vector3 RandomPos()
    {
        float x = transform.position.x + Random.Range(-halfRange, halfRange);
        float z = transform.position.z + Random.Range(-halfRange, halfRange);
        return new Vector3(x, groundY, z);
    }
}