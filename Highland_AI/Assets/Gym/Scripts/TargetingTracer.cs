using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This traces a targeting arc from the Unit/card/etc targeting to
/// the target.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class TargetingTracer : MonoBehaviour {

    public int _LineRendererNumberOfPositions = 5;
    public float _ArcDegrees = 30f;

    private LineRenderer lr;

    private Transform targeter = null;
    private Transform target = null;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (targeter != null)
        {
            lr.material = new Material(Shader.Find("Sprites/Default"));

            // Set some positions
            Vector3[] positions = new Vector3[_LineRendererNumberOfPositions];

            for (int i = 0; i < _LineRendererNumberOfPositions; ++i)
            {

            }

            lr.numPositions = positions.Length;
            lr.SetPositions(positions);
        }
	}

    public void SetTargeter(Transform t)
    {
        targeter = t;
    }


    
}
