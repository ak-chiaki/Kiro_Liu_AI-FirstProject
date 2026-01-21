using UnityEngine;

public class IndexDebug : MonoBehaviour {

     public int index = 1;

    public void LogIndex()
    {
        Debug.Log("Index = " + index);
    }
}