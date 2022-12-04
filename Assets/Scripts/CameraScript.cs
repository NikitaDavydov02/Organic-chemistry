using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    float _rotY;
    private Vector3 _offset;
    private float distance;
    public float maxYOffset;
    public float minYOffset;
    public float zoomSpeed;
    public float maxDistance;
    public float minDistance;
    [SerializeField]
    public Vector3 targetPoint;
    public float horSpeed;
    public float vertSpeed;
    public float pointSpeed;
    // Use this for initialization
    void Start () {
        _rotY = transform.eulerAngles.y;
        _offset = new Vector3(0, -5, 10);
        distance = _offset.magnitude;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.PageUp))
            targetPoint.y += pointSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.PageDown))
            targetPoint.y -= pointSpeed * Time.deltaTime;
        float deltaVert = Input.GetAxis("Mouse Y")*vertSpeed;
        Vector3 o = _offset;
        o.y += deltaVert * Time.deltaTime;
        float hor = Mathf.Sqrt(o.x * o.x + o.z * o.z);
        if (o.y/hor > maxYOffset)
            o.y = maxYOffset*hor;
        if (o.y/hor < minYOffset)
            o.y = minYOffset*hor;
        _offset = o;
        float deltaHor = Input.GetAxis("Mouse X") * horSpeed * Time.deltaTime;
        _rotY += deltaHor;

        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        if (distance > maxDistance) distance = maxDistance;
        if (distance < minDistance) distance = minDistance;
        float k = distance / _offset.magnitude;
        _offset *= k;
        Quaternion rotation;
        rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = targetPoint - (rotation * _offset);
        transform.LookAt(targetPoint);
    }
}
