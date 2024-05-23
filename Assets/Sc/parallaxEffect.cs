using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    Vector2 startingPosition;

    Vector2 camMoveSinceStart => (Vector2) cam.transform.position-startingPosition;
    float parallaxFactor => Mathf.Abs(zDistance)/clippingPlane;

    float clippingPlane => (cam.transform.position.z+(zDistance>0?cam.farClipPlane:cam.nearClipPlane));
    float zDistance => transform.position.z - followTarget.transform.position.z;
    float StartingZ;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition= transform.position;
        StartingZ= transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition+camMoveSinceStart*parallaxFactor;
        transform.position = new Vector3 (newPosition.x, newPosition.y, StartingZ);
    }
}
