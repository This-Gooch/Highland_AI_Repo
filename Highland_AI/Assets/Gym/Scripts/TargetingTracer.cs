using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSGameplay.Cards;
using NSGameplay;

/// <summary>
/// This traces a targeting arc from the Unit/card/etc targeting to
/// the target.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class TargetingTracer : MonoBehaviour {

    public static TargetingTracer instance;

    public int _LineRendererNumberOfPositions = 5;
    public float _ArcHeight = 10f;

    private LineRenderer lr;

    private LayerMask targetableMask = 0;
    private Transform origin = null;
    private Vector3 target = Vector3.zero;
    private float tileOffset = 0f;

    [System.NonSerialized]
    public IEffector targeterEntity;
    private Unit unitSpendingUtility;

    public float tileOffsetSpeed = 5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        lr = GetComponent<LineRenderer>();

    }

	// Update is called once per frame
	void Update ()
    {
        if (origin != null)
        {
            Vector3[] positions = new Vector3[_LineRendererNumberOfPositions];
            //Cast a ray to the mouse position.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;            
            if (Physics.Raycast(ray, out hit, 1000f, targetableMask))
            {
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
                {
                    target = hit.collider.GetComponent<ITargetable>().GetTargetLocation();
                    if (Input.GetButtonDown("Select"))
                    {
                        //If we found a valid target.
                        Debug.Log("Click a unit to attack");
                        unitSpendingUtility.info.utility -= targeterEntity.Use(unitSpendingUtility.info.utility, hit.collider.GetComponent<ITargetable>());
                    }
                }
                else
                {
                    target = hit.point;
                }
            }
            //Calculate the positions            
            Vector3 direction =  target - origin.position;
            float length = Vector3.Distance(origin.position, target);
            direction.Normalize();

            for (int i = 0; i < _LineRendererNumberOfPositions; ++i)
            {
                float deltaX = length * i / _LineRendererNumberOfPositions;
                positions[i] = origin.position + (direction * deltaX);
                positions[i] += Vector3.up * AdvanceMath.Calculate_deltaY_Arc(length, _ArcHeight, deltaX);
            }

            lr.numPositions = positions.Length;
            lr.SetPositions(positions);
            tileOffset -= tileOffsetSpeed * Time.deltaTime;
            lr.material.mainTextureOffset = new Vector2(tileOffset, 0f);
        }
	}

    public void SetTarget(Vector3 t)
    {
        target = t;
    }

    public void SetOrigin(Transform t, LayerMask mask, IEffector targeter, Unit u = null)
    {
        unitSpendingUtility = u;
        targeterEntity = targeter;
        targetableMask = mask;
        origin = t;
    }

    public void Close()
    {
        unitSpendingUtility = null;
        targeterEntity = null;
        tileOffset = 0f;
        targetableMask = 0;
        origin = null;
        target = Vector3.zero;
        lr.numPositions = 0;
    }
    
}
