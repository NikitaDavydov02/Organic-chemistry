using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibridizationScript : MonoBehaviour {
    [SerializeField]
    public List<GameObject> vershini;
    public List<Vector3> vectors;
    public float distance;
    public float hightDistance;
    void Start()
    {
        for (int i = 0; i < vershini.Count; i++)
        {
            vectors.Add(vershini[i].transform.position - transform.position);
        }
        distance = vectors[0].magnitude;
        hightDistance = vectors[3].magnitude;
    }
    public void Inicialization()
    {
        //Debug.Log("Start");
        for (int i = 0; i < vershini.Count; i++)
        {
            vectors.Add(vershini[i].transform.position - transform.position);
        }
        distance = vectors[0].magnitude;
        hightDistance = vectors[3].magnitude;
    }
    public void NewDistance(float newDistance, float newHightDistance)
    {
        for (int i = 0; i < vershini.Count; i++)
        {
            vectors[i] *= 1000;
            if (i != 3)
                vectors[i] = Vector3.ClampMagnitude(vectors[i], newDistance);
            else
                vectors[i] = Vector3.ClampMagnitude(vectors[i], newHightDistance);
            vershini[i].transform.position = transform.position + vectors[i];
        }

    }
    public void TranslateGameObjectToVershina(GameObject go, int indexOfVershina, float distanceOfGO)
    {
        //Debug.Log("A: " + vectors.Count);
        //Debug.Log(indexOfVershina + vectors.Count);
        vectors[indexOfVershina] *= 1000;
        vectors[indexOfVershina] = Vector3.ClampMagnitude(vectors[indexOfVershina], distanceOfGO);
        go.transform.position = transform.position + vectors[indexOfVershina];
    }
    // Update is called once per frame
    void Update()
    {
        vectors.Clear();
        for (int i = 0; i < vershini.Count; i++)
        {
            vectors.Add(vershini[i].transform.position - transform.position);
        }
    }
    public Vector3 PositionOnVector(int indexOfVershina, float distance)
    {
        Vector3 copy = vectors[indexOfVershina];
        vectors[indexOfVershina] *= 1000;
        vectors[indexOfVershina] = Vector3.ClampMagnitude(vectors[indexOfVershina], distance);
        Vector3 output = transform.position + vectors[indexOfVershina];
        vectors[indexOfVershina] = copy;
        return output;
    }
    public void UpdateVectors()
    {
        vectors.Clear();
        for (int i = 0; i < vershini.Count; i++)
        {
            vectors.Add(vershini[i].transform.position - transform.position);
        }
    }
}
