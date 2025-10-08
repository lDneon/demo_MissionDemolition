using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileLine : MonoBehaviour
{
    private LineRenderer _Line;
    private bool _drawing = true;
    private Projectile _projectile;


    // Start is called before the first frame update
    void Start()
    {
        _Line = GetComponent<LineRenderer>();
        _Line.positionCount = 1;
        _Line.SetPosition(0, transform.position);

        _projectile = GetComponentInParent<Projectile>();


    }

    // Update is called once per frame

    void FixedUpdate()
    {

        _Line.positionCount++;
        _Line.SetPosition(_Line.positionCount - 1, transform.position);
        if (_projectile != null)
        {
            if (_projectile.GetComponentInParent<Rigidbody>().IsSleeping())
            {
                _drawing = false;
                _projectile = null;
            }
        }
    }
}
