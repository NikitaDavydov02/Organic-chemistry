using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[PreferBinarySerialization]
public class Atom : MonoBehaviour {
    [SerializeField]
    public GameObject[] electrons;
    public float radius;
    public float covalentRadius;
    [SerializeField]
    public GameObject[] neiboursAtoms;
    public int number;
    public float distanceBetweenElectronsInPair;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
