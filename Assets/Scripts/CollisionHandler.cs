using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    string hitTag;

    void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Start":
            Debug.Log("Hit Start");
            break;

            case "Finish":
            Debug.Log("Hit Finished");
            break;
            
            default:
            Debug.Log("Hit obstacle");
            break;
        }
    }
}
