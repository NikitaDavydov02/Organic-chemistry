using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanizmManager : MonoBehaviour {
    [SerializeField]
    public MainManager mainManager;
    [SerializeField]
    GameObject lightPrefab;
    public Vector3 lokingForLightPosition=Vector3.zero;
    private GameObject lightObject = null;
    public float lightSpeed = 10f;
    public float lightSpawnDistance = 10f;
    public float critDistance = 0.5f;
    GameObject clorine = null;
    GameObject electron = null;
    GameObject clorine1 = null;
    GameObject electron1 = null;
    public float raspadSpeed;
    public float contactingSpeed;
    public float atomRotSpeed;
    public float HClSpeed;
    public float raspadDistance;
    public float raspad2Distance;
    private float contactingDistance;
    private float clorGoingDistance;

    private bool raspadClora = false;
    private bool povorotClora = false;
    private bool contactingClorineWithHydrogenium = false;
    private bool HClgoing = false;
    private bool clorGoing = false;
    private bool povorotClora2 = false;
    private bool lastRaspad = false;

    private float currentRaspadDistance=0;
    private float currentRaspad2Distance = 0;
    private float currentPovorotAngle = 0;
    private float currentPovorot2Angle = 0;
    private float currentContactingDistance;
    private float currentHClgoingDistance = 0;
    private float currentClorGoingDistance = 0;
    private float timeOfContacting;
    private float HydrogeniumRotSpeed;
    private Atom targetCarbone;
    private Atom targedHydrogenim;
    public float HClgoingDistance;
    public float clorgoingSpeed;

    public bool pause = false;
    public bool mechanizmIsLoaded = false;

    // Use this for initialization
    void Start () {
        ClorirovanieOfPropan();
}
	
	// Update is called once per frame
	void Update () {
        if (pause)
            return;
        if (lightObject != null)
        {
            lightObject.transform.Translate(0, 0, lightSpeed * Time.deltaTime, Space.Self);
            if (Vector3.Magnitude(lightObject.transform.position - lokingForLightPosition) <= critDistance)
            {
                raspadClora = true;
                Destroy(lightObject);
            }
        }
        if (raspadClora)
        {
            electron1.transform.Translate(0, 0, raspadSpeed * Time.deltaTime, Space.Self);
            clorine1.transform.Translate(0, 0, raspadSpeed * Time.deltaTime, Space.Self);
            currentRaspadDistance += raspadSpeed * Time.deltaTime;
            if (currentRaspadDistance >= raspadDistance)
            {
                raspadClora = false;
                povorotClora = true;
            }
        }
        if (povorotClora)
        {
            clorine1.transform.Rotate(0, atomRotSpeed * Time.deltaTime, 0, Space.Self);
            electron1.transform.position = clorine1.transform.position + Vector3.ClampMagnitude(-clorine1.transform.forward * 10, clorine1.gameObject.GetComponent<Atom>().radius);
            currentPovorotAngle += atomRotSpeed * Time.deltaTime;
            if (currentPovorotAngle >= 180)
            {
                povorotClora = false;
                contactingClorineWithHydrogenium = true;
                targedHydrogenim = targetCarbone.neiboursAtoms[2].gameObject.GetComponent<Atom>();
                float HCllength = targedHydrogenim.covalentRadius + clorine1.GetComponent<Atom>().covalentRadius;
                float currentDistance = Vector3.Magnitude(clorine1.transform.position - targedHydrogenim.gameObject.transform.position);
                contactingDistance = currentDistance - HCllength;
                clorine1.transform.Rotate(0, 180, 0, Space.Self);
                electron1.transform.forward = clorine1.transform.forward;
                timeOfContacting = contactingDistance / contactingSpeed;
                HydrogeniumRotSpeed = 180 / timeOfContacting;
            }
        }
        if (contactingClorineWithHydrogenium)
        {
            clorine1.transform.Translate(0, 0, contactingSpeed * Time.deltaTime, Space.Self);
            electron1.transform.Translate(0, 0, raspadSpeed * Time.deltaTime, Space.Self);
            targedHydrogenim.gameObject.transform.Rotate(0, HydrogeniumRotSpeed * Time.deltaTime, 0,Space.Self);
            targedHydrogenim.electrons[0].transform.position = targedHydrogenim.transform.position + targedHydrogenim.transform.forward * targedHydrogenim.radius;
            currentContactingDistance += contactingSpeed * Time.deltaTime;
            if (currentContactingDistance >= contactingDistance)
            {
                contactingClorineWithHydrogenium = false;
                clorine1.transform.Rotate(0, 180, 0, Space.Self);
                electron1.transform.forward = clorine1.transform.forward;
                clorine.transform.forward= clorine1.transform.forward;
                electron.transform.forward= clorine1.transform.forward;
                targedHydrogenim.gameObject.transform.forward= clorine1.transform.forward; 
                targedHydrogenim.electrons[0].gameObject.transform.forward= clorine1.transform.forward;
                HClgoing = true;
            }
        }
        if (HClgoing)
        {
            clorine1.transform.Translate(0, 0, HClSpeed * Time.deltaTime, Space.Self);
            electron1.transform.Translate(0, 0, HClSpeed * Time.deltaTime, Space.Self);
            clorine.transform.Translate(0, 0, HClSpeed * Time.deltaTime, Space.Self);
            electron.transform.Translate(0, 0, HClSpeed * Time.deltaTime, Space.Self);
            targedHydrogenim.gameObject.transform.Translate(0, 0, HClSpeed * Time.deltaTime, Space.Self);
            targedHydrogenim.electrons[0].gameObject.transform.Translate(0, 0, HClSpeed * Time.deltaTime, Space.Self);
            currentHClgoingDistance += HClSpeed * Time.deltaTime;
            if (currentHClgoingDistance >= HClgoingDistance)
            {
                HClgoing = false;
                Destroy(clorine1);
                Destroy(electron1);
                Destroy(clorine);
                Destroy(electron);
                Destroy(targedHydrogenim.electrons[0].gameObject);
                Destroy(targedHydrogenim.gameObject);
                InstantiateClor();
                clorine.transform.forward = clorine1.transform.forward;
                electron.transform.forward = electron1.transform.forward;
                clorGoingDistance = Vector3.Magnitude(clorine1.transform.position - targetCarbone.transform.position) - (targetCarbone.covalentRadius + clorine.GetComponent<Atom>().covalentRadius);
                clorGoing = true;
            }
        }
        if (clorGoing)
        {
            clorine.transform.forward = clorine1.transform.forward;
            electron.transform.forward = electron1.transform.forward;
            clorine.transform.Translate(0, 0, clorgoingSpeed * Time.deltaTime, Space.Self);
            electron.transform.Translate(0, 0, clorgoingSpeed * Time.deltaTime, Space.Self);
            clorine1.transform.Translate(0, 0, clorgoingSpeed * Time.deltaTime, Space.Self);
            electron1.transform.Translate(0, 0, clorgoingSpeed * Time.deltaTime, Space.Self);
            currentClorGoingDistance += clorgoingSpeed * Time.deltaTime;
            if (currentClorGoingDistance >= clorGoingDistance)
            {
                clorGoing = false;
                povorotClora2 = true;
            }
        }
        if (povorotClora2)
        {
            clorine1.transform.Rotate(0, atomRotSpeed * Time.deltaTime, 0, Space.Self);
            electron1.transform.position = clorine1.transform.position + Vector3.ClampMagnitude(-clorine1.transform.forward * 10, clorine1.gameObject.GetComponent<Atom>().radius);
            currentPovorot2Angle += atomRotSpeed * Time.deltaTime;
            if (currentPovorot2Angle >= 180)
            {
                povorotClora2 = false;
                lastRaspad = true;
            }
        }
        if (lastRaspad)
        {
            clorine.transform.Translate(0, 0, -raspadSpeed * Time.deltaTime);
            electron.transform.Translate(0, 0, -raspadSpeed * Time.deltaTime);
            currentRaspad2Distance += raspadSpeed * Time.deltaTime;
            if (currentRaspad2Distance >= raspad2Distance)
                lastRaspad = false;
        }
	}
    public void ClorirovanieOfPropan()
    {
        mechanizmIsLoaded = true;
        mainManager.InstantiateAlkane(3, new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(),
            new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>());
        InstantiateClor();
        lightObject = Instantiate(lightPrefab);
        mainManager.mustToDelate.Add(lightObject);
        lightObject.transform.position = electron.transform.position + electron.transform.up * lightSpawnDistance;
        lightObject.transform.forward = -electron.transform.up;
        lokingForLightPosition = electron.transform.position;
    }
    public void RaspadClora()
    {

    }
    public void InstantiateClor()
    {
        Atom firstCarboneScript = mainManager.mustToDelate[0].gameObject.GetComponent<Atom>();
        Atom secondCarboneScript = firstCarboneScript.neiboursAtoms[1].gameObject.GetComponent<Atom>();
        targetCarbone = secondCarboneScript;
        Vector3 vectorNaClor = secondCarboneScript.electrons[2].transform.position - secondCarboneScript.transform.position;
        secondCarboneScript.neiboursAtoms[2].transform.forward = -vectorNaClor;
        Atom clorineScript = null;
        for (int i = 0; i < 2; i++)
        {
            clorine = Instantiate(mainManager.curbsPrefab[16]);
            mainManager.mustToDelate.Add(clorine);
            clorine.transform.position = secondCarboneScript.transform.position + vectorNaClor * 4;
            clorineScript = clorine.gameObject.GetComponent<Atom>();
            clorine.transform.forward = vectorNaClor;
            electron = Instantiate(mainManager.electronPrefab);
            mainManager.mustToDelate.Add(electron);
            electron.transform.position = secondCarboneScript.transform.position + vectorNaClor * 4;
            electron.transform.forward = vectorNaClor;
            electron.transform.Translate(0, 0, clorineScript.radius, Space.Self);
            if (i == 0)
            {
                clorine1 = clorine;
                electron1 = electron;
                clorine1.transform.forward = -vectorNaClor;
                electron1.transform.forward = -vectorNaClor;
            }
        }
        clorine.transform.Translate(0, 0, clorineScript.covalentRadius * 2, Space.Self);
        electron.transform.Translate(0, 0, -2 * (clorineScript.radius - clorineScript.covalentRadius), Space.Self);
    }
    public void NewMechanizm(int index)
    {
        if (index == 0)
        {
            pause = true;
            mainManager.ClearWorld();
            ClorirovanieOfPropan();
        }
    }
    public void StopMechanizm()
    {
        lokingForLightPosition = Vector3.zero;
        raspadClora = false;
        povorotClora = false;
        contactingClorineWithHydrogenium = false;
        HClgoing = false;
        clorGoing = false;
        povorotClora2 = false;
        lastRaspad = false;

        currentRaspadDistance = 0;
        currentRaspad2Distance = 0;
        currentPovorotAngle = 0;
        currentPovorot2Angle = 0;
        currentContactingDistance = 0;
        currentHClgoingDistance = 0;
        currentClorGoingDistance = 0;
    }
}
