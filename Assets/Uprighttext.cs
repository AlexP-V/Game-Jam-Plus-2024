using UnityEngine;

public class UprightText : MonoBehaviour
{
    //public Transform target; // The object the text should follow

    void LateUpdate()
    {
       // if (target != null)
       // {
            // Set the position to match the target
            //transform.position = target.position;

            // Keep the rotation identity (upright)
            transform.rotation = Quaternion.identity;
       // }
    }
}

