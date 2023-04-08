using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private SkullyController skullyController;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lineRenderer;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    [Range(0,50)]
    public int grapplingCounter = 10;
    public float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input Key-Mouse")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;
    private bool isAlive;
    private float raycastLength;

    private void Start()
    {
        skullyController = GetComponent<SkullyController>();
    }

    private void Update()
    {
        isAlive = GetComponent<SkullyController>().isAlive;
        if (Input.GetKeyDown(grappleKey) && grapplingCounter>0 && isAlive) StartGrapple();

        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
         if (grappling)
            lineRenderer.SetPosition(0, gameObject.transform.position);
    }

    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grappling = true;

        skullyController.freeze = true;

        RaycastHit hit;
        //bool isHittingSomething = Physics.Raycast(GetWorldRay(Camera.main), out hit, raycastLength);//new 
       //if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        if (Physics.Raycast(cam.position, GetWorldRay(Camera.main).direction, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplingCounter--;

            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(1, grapplePoint);
    }

    public static Ray GetWorldRay(Camera camera, bool useOldSystem = true)
    {
        float2 screenPos = GetBoundedScreenPosition(useOldSystem);
        float3 screenPosWithDepth = new float3(screenPos, camera.nearClipPlane);
        return camera.ScreenPointToRay(screenPosWithDepth);
    }

    public static float2 GetBoundedScreenPosition(bool useOldSystem = true)
    {
        float2 raw = GetScreenPosition(useOldSystem);
        return math.clamp(raw, new float2(0, 0), new float2(Screen.width - 1, Screen.height - 1));
    }

    public static float2 GetScreenPosition(bool useOldSystem = true)
    {
        //if (useOldSystem)
        //{
            Vector3 posn = Input.mousePosition;
            return new float2(posn.x, posn.y);
        //}
        //else
        //{
        //    return Mouse.current.position.ReadValue();
        //}
    }


    private void ExecuteGrapple()
    {
        skullyController.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        skullyController.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        skullyController.freeze = false;

        grappling = false;

        grapplingCdTimer = grapplingCd;

        lineRenderer.enabled = false;
    }

    public bool IsGrappling()
    {
        return grappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }


}


