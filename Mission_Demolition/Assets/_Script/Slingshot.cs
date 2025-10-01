using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour


{

    public GameObject launchPoint;
    public GameObject projectilePrefab;
    public float velocityMult = 10f;

    //fields set dynamically
    [Header("set Dynamically")]
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    private void Awake()
    {
        //similar to start but called befre start
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    private void OnMouseEnter()
    {
        print("Mouse Entered");
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        print("Mouse Exit");
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        aimingMode = true;

        projectile = Instantiate(projectilePrefab) as GameObject;
        //start it at the launch point
        projectile.transform.position = launchPos;
        //set it to isKinematic to true for now
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }


    private void Update()
    {

        if (!aimingMode) return;
        //get the current mouse position in 2D screen coords
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos;
        //limit mouseDelta to the radius of the slingshot sphere
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {

            // the mouse has been released 
            aimingMode = false;
            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            FollowCam.POI = projectile;
            projRB.isKinematic = false;
            projRB.velocity = -mouseDelta * velocityMult;
            projectile = null;
        }
    }
}
