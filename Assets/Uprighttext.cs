using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    void LateUpdate()
    {
        // Reset the rotation to ensure the text stays upright
        transform.rotation = Quaternion.identity;
    }
}


