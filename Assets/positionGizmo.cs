using UnityEngine;
using System.Collections;

public class positionGizmo : MonoBehaviour {

    public float radius = 0.25f;
    public Color color = Color.red;

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }

}
