using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainManager : MonoBehaviour {
    [SerializeField]
    public GameObject tetrahedronPrefab;
    [SerializeField]
    public GameObject trianglePrefab;
    [SerializeField]
    public GameObject straightPrefab;
    [SerializeField]
    public GameObject[] curbsPrefab;
    [SerializeField]
    public GameObject electronPrefab;
    public List<GameObject> mustToDelate = new List<GameObject>();
    private bool dontChangePosition = false;
    bool dontChangeLastCarbone = false;


    [SerializeField]
    Dictionary<string, object> atoms;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }

    
    public void InstantiateAlkane(int n, List<int> clores, List<int> bromes, List<int>ftores, List<int> iodes, List<int> ds, List<int> ts, List<int> ethers, List<int> ohs, List<int> sulfides, List<int> tiols, List<int> aminGroups, List<int> methyl, List<int>ethyl, List<int> npropil, List<int> ipropil, List<int> nbutil, List<int> vtorbutil, List<int> izobutil, List<int> tretbutil, List<int>cetons)
    {
        dontChangePosition = false;
        ClearWorld();
        GameObject tetrahedron = Instantiate(tetrahedronPrefab);
        tetrahedron.transform.position = new Vector3(0, 0, 0);
        Tetrahedron script = tetrahedron.GetComponent<Tetrahedron>();
        script.Inicialization();
        GameObject carbone = null;
        GameObject lastCarbone = carbone;
        for (int i = 1; i <= n; i++)
        {
            carbone = Instantiate(curbsPrefab[5]);
            mustToDelate.Add(carbone);
            Atom atomScript = carbone.GetComponent<Atom>();
            atomScript.electrons = new GameObject[4];
            atomScript.neiboursAtoms = new GameObject[4];
            carbone.transform.position = tetrahedron.transform.position;
            if (i != 1)
            {
                if (!dontChangePosition)
                {
                    Vector3 oldPos = tetrahedron.transform.position;
                    Vector3 newPosition = script.PositionOnVector(1, atomScript.covalentRadius * 2);
                    tetrahedron.transform.position = newPosition;
                    script.transform.LookAt(oldPos);
                    script.UpdateVectors();
                }
                else
                    dontChangePosition = false;
                carbone.transform.position = tetrahedron.transform.position;
                atomScript.neiboursAtoms[0] = lastCarbone;
                lastCarbone.GetComponent<Atom>().neiboursAtoms[1] = carbone;
            }
            bool after_sp3 = false;
            if (ds.Contains(i))
            {
                
                GameObject triangle = Instantiate(trianglePrefab);
                Triangle triangleScript = triangle.gameObject.GetComponent<Triangle>();
                triangleScript.Inicialization();
                triangle.transform.position = carbone.transform.position;
                if(i!=1)
                    triangle.transform.LookAt(lastCarbone.transform);
                triangleScript.UpdateVectors();
                Zamestiteli(triangleScript, new List<int>(){ 3 }, atomScript,i,n, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil);
                ds.Remove(i);
                lastCarbone = carbone;
                bool perviyraz = true;
                GameObject straight = null;
                Straight straightScript = null;
                triangle.transform.position = triangleScript.PositionOnVector(1, 2 * atomScript.covalentRadius);
                triangle.transform.Rotate(0, 60, 0, Space.Self);
                triangleScript.UpdateVectors();
                i++;

                while (ds.Contains(i))
                {
                    i++;
                    Debug.Log("Kum");
                    carbone = Instantiate(curbsPrefab[5]);
                    mustToDelate.Add(carbone);
                    atomScript = carbone.GetComponent<Atom>();
                    atomScript.electrons = new GameObject[4];
                    atomScript.neiboursAtoms = new GameObject[4];
                    atomScript.neiboursAtoms[0] = lastCarbone;
                    lastCarbone.GetComponent<Atom>().neiboursAtoms[1] = carbone;
                    if (perviyraz)
                    {
                        carbone.transform.position = triangle.transform.position;
                        straight = Instantiate(straightPrefab);
                        straightScript = straight.gameObject.GetComponent<Straight>();
                        straightScript.Inicialization();
                        straight.transform.position = carbone.transform.position;
                        straight.transform.up = triangle.transform.up;
                        straight.transform.LookAt(lastCarbone.transform);
                        straightScript.UpdateVectors();
                        straightScript.TranslateGameObjectToVershina(triangle, 1, 2 * atomScript.covalentRadius);
                        triangleScript.UpdateVectors();
                        for(int h = 0;h < 4; h++)
                        {
                            GameObject electron = Instantiate(electronPrefab);
                            mustToDelate.Add(electron);
                            straightScript.TranslateGameObjectToVershina(electron, h, atomScript.radius);
                            atomScript.electrons[h] = electron;
                        }
                        perviyraz = false;
                    }
                    else
                    {
                        Debug.Log("Polien");
                        straightScript.TranslateGameObjectToVershina(straight, 1, 2 * atomScript.covalentRadius);
                        carbone.transform.position = straight.transform.position;
                        triangle.transform.position = straightScript.PositionOnVector(1, 2 * atomScript.covalentRadius);
                        for (int h = 0; h < 4; h++)
                        {
                            GameObject electron = Instantiate(electronPrefab);
                            mustToDelate.Add(electron);
                            straightScript.TranslateGameObjectToVershina(electron, h, atomScript.radius);
                            atomScript.electrons[h] = electron;
                        }
                    }
                    lastCarbone = carbone;
                }
                Destroy(straight);
                carbone = Instantiate(curbsPrefab[5]);
                mustToDelate.Add(carbone);
                atomScript = carbone.GetComponent<Atom>();
                atomScript.electrons = new GameObject[4];
                atomScript.neiboursAtoms = new GameObject[4];
                carbone.transform.position = triangle.transform.position;
                atomScript.neiboursAtoms[0] = lastCarbone;
                lastCarbone.GetComponent<Atom>().neiboursAtoms[1] = carbone;
                Zamestiteli(triangleScript, new List<int>() { 3 }, atomScript, i, n, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil);
                tetrahedron.transform.position = triangleScript.PositionOnVector(1, 2 * atomScript.covalentRadius);
                tetrahedron.transform.LookAt(triangle.transform);
                script.UpdateVectors();
                dontChangePosition = true;
                Destroy(triangle);
            }
            else if (ts.Contains(i))
            {
                GameObject straight = Instantiate(straightPrefab);
                Straight straightScript = straight.gameObject.GetComponent<Straight>();
                straightScript.Inicialization();
                straight.transform.position = carbone.transform.position;
                if (i != 1)
                    straight.transform.LookAt(lastCarbone.transform);
                straightScript.UpdateVectors();
                Zamestiteli(straightScript, new List<int>() { 2,3 }, atomScript, i, n, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil);
                ts.Remove(i);
                lastCarbone = carbone;
                i++;
                carbone = Instantiate(curbsPrefab[5]);
                mustToDelate.Add(carbone);
                atomScript = carbone.GetComponent<Atom>();
                atomScript.electrons = new GameObject[4];
                atomScript.neiboursAtoms = new GameObject[4];
                straight.transform.position = straightScript.PositionOnVector(1, 2 * atomScript.covalentRadius);
                //straight.transform.Rotate(0, 60, 0, Space.Self);
                straightScript.UpdateVectors();
                carbone.transform.position = straight.transform.position;
                atomScript.neiboursAtoms[0] = lastCarbone;
                lastCarbone.GetComponent<Atom>().neiboursAtoms[1] = carbone;
                Zamestiteli(straightScript, new List<int>() { 2, 3 }, atomScript, i, n, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil);
                tetrahedron.transform.position = straightScript.PositionOnVector(1, 2 * atomScript.covalentRadius);
                tetrahedron.transform.LookAt(straight.transform);
                script.UpdateVectors();
                dontChangePosition = true;
                Destroy(straight);
            }
            else if (cetons.Contains(i))
            {
                if (i == 1)
                    Debug.Log(i+ "  and "+n);
                cetons.Remove(i);
                if (i == n && n!=1)
                    MakeCeton(carbone, lastCarbone, atomScript,i,script, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil, true, false);
                else if (i == 1 && n!=1)
                    MakeCeton(carbone, lastCarbone, atomScript, i,script, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil, false, true);
                else if( i==1 && n==1)
                    MakeCeton(carbone, lastCarbone, atomScript, i, script, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil, true, true);
                else
                    MakeCeton(carbone, lastCarbone, atomScript, i,script, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil);
            }
            else
            {
                Zamestiteli(script, new List<int>(), atomScript, i, n, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl,npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil);
                after_sp3 = true;
            }

            if (ethers.Contains(i))
            {
                lastCarbone = EthereOrSulfide(script, atomScript, after_sp3, true).gameObject;
                ethers.Remove(i);
                dontChangeLastCarbone = true;
            }
            if (sulfides.Contains(i))
            {
                lastCarbone = EthereOrSulfide(script, atomScript, after_sp3, false).gameObject;
                sulfides.Remove(i);
                dontChangeLastCarbone = true;
            }

            if (!dontChangeLastCarbone)
                lastCarbone = carbone;
            else
                dontChangeLastCarbone = false;
        }
        Destroy(tetrahedron);
    }
    public void ClearWorld()
    {
        int i = mustToDelate.Count;
        GameObject[] del = new GameObject[i];
        for(int x = 0; x < i; x++)
            del[x] = mustToDelate[x];
        for (int x = 0; x < i; x++)
            Destroy(del[x]);
        mustToDelate.Clear();
    }
    public Atom EthereOrSulfide(GibridizationScript script, Atom lastCarboneAtomScript, bool after_sp3, bool ethere)
    {
        //Tetrahedron script;
        GameObject oxygenTetrahedron = Instantiate(tetrahedronPrefab) as GameObject;
        Tetrahedron tetrahedronScript = oxygenTetrahedron.GetComponent<Tetrahedron>();
        oxygenTetrahedron.transform.position = script.gameObject.transform.position;

        //Instantiate oxygen
        GameObject oxygen;
        if (ethere)
            oxygen = Instantiate(curbsPrefab[7]);
        else
            oxygen = Instantiate(curbsPrefab[15]);
        mustToDelate.Add(oxygen);
        Atom oxygenScript = oxygen.GetComponent<Atom>();
        oxygenScript.electrons = new GameObject[6];
        oxygenScript.neiboursAtoms = new GameObject[2];
        lastCarboneAtomScript.neiboursAtoms[1] = oxygen;
        oxygenScript.neiboursAtoms[0] = lastCarboneAtomScript.gameObject;

        //Translate base script;
        Vector3 oldPos = script.gameObject.transform.position;
        Vector3 newPosition = script.PositionOnVector(1, lastCarboneAtomScript.covalentRadius + oxygenScript.covalentRadius);
        if (after_sp3)
        {
            script.gameObject.transform.position = newPosition;
            script.transform.LookAt(oldPos);
            script.UpdateVectors();
        }

        //Translate oxygen;
        oxygen.transform.position = script.transform.position;

        //Translate tetrahedron;
        oxygenTetrahedron.transform.position = script.transform.position;
        oxygenTetrahedron.transform.LookAt(lastCarboneAtomScript.gameObject.transform);
        tetrahedronScript.UpdateVectors();

        //Electrons of oxygen;
        for (int h = 0; h < 4; h++)
        {
            GameObject electron = Instantiate(electronPrefab);
            mustToDelate.Add(electron);
            tetrahedronScript.TranslateGameObjectToVershina(electron, h, oxygenScript.radius);
            if (h == 2 || h == 3)
            {
                electron.transform.LookAt(tetrahedronScript.transform);
                electron.transform.Translate(oxygenScript.distanceBetweenElectronsInPair/2, 0, 0, Space.Self);
                GameObject electron1 = Instantiate(electronPrefab);
                mustToDelate.Add(electron1);
                tetrahedronScript.TranslateGameObjectToVershina(electron1, h, oxygenScript.radius);
                electron1.transform.LookAt(tetrahedronScript.transform);
                electron1.transform.Translate(-oxygenScript.distanceBetweenElectronsInPair / 2, 0, 0, Space.Self);
                oxygenScript.electrons[h] = electron1;
            }
            oxygenScript.electrons[h] = electron;
        }

        //Tetrahedron translate
        tetrahedronScript.TranslateGameObjectToVershina(oxygenTetrahedron, 1, lastCarboneAtomScript.covalentRadius + oxygenScript.covalentRadius);
        tetrahedronScript.transform.LookAt(oxygen.transform);
        tetrahedronScript.UpdateVectors();

        //Translate script;
        script.transform.position = tetrahedronScript.transform.position;
        script.transform.LookAt(oxygen.transform);
        script.UpdateVectors();

        //Destroy tetrahedron;
        Destroy(tetrahedronScript.gameObject);
        dontChangePosition = true;


        return oxygenScript;
    }
    public Atom Sulfide(GibridizationScript script, float carboneCovalentRadius, Atom lastCarboneAtomScript)
    {
        GameObject sulfur = Instantiate(curbsPrefab[15]);
        mustToDelate.Add(sulfur);
        Atom sulfurScript = sulfur.GetComponent<Atom>();
        sulfurScript.electrons = new GameObject[6];
        sulfurScript.neiboursAtoms = new GameObject[2];
        lastCarboneAtomScript.neiboursAtoms[1] = sulfur;
        sulfurScript.neiboursAtoms[0] = lastCarboneAtomScript.gameObject;
        Vector3 oldPos = script.gameObject.transform.position;
        Vector3 newPosition = script.PositionOnVector(1, carboneCovalentRadius * 2);
        script.gameObject.transform.position = newPosition;
        script.transform.LookAt(oldPos);
        script.UpdateVectors();
        sulfur.transform.position = script.transform.position;
        for (int h = 0; h < 4; h++)
        {
            GameObject electron = Instantiate(electronPrefab);
            mustToDelate.Add(electron);
            script.TranslateGameObjectToVershina(electron, h, sulfurScript.radius);
            if (h == 2 || h == 3)
            {
                electron.transform.LookAt(script.transform);
                electron.transform.Translate(sulfurScript.distanceBetweenElectronsInPair / 2, 0, 0, Space.Self);
                GameObject electron1 = Instantiate(electronPrefab);
                mustToDelate.Add(electron1);
                script.TranslateGameObjectToVershina(electron1, h, sulfurScript.radius);
                electron1.transform.LookAt(script.transform);
                electron1.transform.Translate(-sulfurScript.distanceBetweenElectronsInPair / 2, 0, 0, Space.Self);
                sulfurScript.electrons[h] = electron1;
            }
            sulfurScript.electrons[h] = electron;
        }
        return sulfurScript;
    }
    public void MakeCeton(GameObject carbone, GameObject lastCarbone, Atom atomScript, int numberOfCarbone, Tetrahedron tetrahedron,
         List<int> clores, List<int> bromes, List<int> ftores, List<int> iodes, List<int> ohs, List<int> tiols, List<int> aminGroups, List<int> methyl, List<int> ethyl, List<int> npropil, List<int> ipropil, List<int> nbutil, List<int> vtorbutil, List<int> izobutil, List<int> tretbutil, bool endaldegid=false, bool startaldegid = false)
    {
        if (numberOfCarbone == 1)
        {
            Debug.Log("Keton 1");
        }
        GameObject triangle = Instantiate(trianglePrefab);
        Triangle triangleScript = triangle.gameObject.GetComponent<Triangle>();
        triangleScript.Inicialization();
        triangle.transform.position = carbone.transform.position;
        if(lastCarbone!=null)
            triangle.transform.LookAt(lastCarbone.transform);
        triangleScript.UpdateVectors();

            GameObject oxygen = Instantiate(curbsPrefab[7]);
            mustToDelate.Add(oxygen);
            Atom oxygenScript = oxygen.GetComponent<Atom>();
            oxygenScript.electrons = new GameObject[6];
            oxygenScript.neiboursAtoms = new GameObject[3];
            oxygenScript.neiboursAtoms[0] = carbone;

        //Carbone electons

        //for(int h = 0; h < 4; h++)
        //{
        //    GameObject electron = Instantiate(electronPrefab);
        //    mustToDelate.Add(electron);
        //    triangleScript.TranslateGameObjectToVershina(electron, h, atomScript.radius);
        //    atomScript.neiboursAtoms[h] = electron;
        //    //if (endaldegid||startaldegid)
        //    //{
        //    //    GameObject hydrogenium = Instantiate(curbsPrefab[0]);
        //    //    mustToDelate.Add(hydrogenium);
        //    //    Atom hydrogeniumScript = hydrogenium.GetComponent<Atom>();
        //    //    hydrogeniumScript.electrons = new GameObject[2];
        //    //    hydrogeniumScript.neiboursAtoms = new GameObject[1];
        //    //    hydrogeniumScript.neiboursAtoms[0] = carbone;
        //    //    if(endaldegid)
        //    //        triangleScript.TranslateGameObjectToVershina(hydrogenium, 1, atomScript.covalentRadius + hydrogeniumScript.covalentRadius);
        //    //    if(startaldegid)
        //    //        triangleScript.TranslateGameObjectToVershina(hydrogenium, 0, atomScript.covalentRadius + hydrogeniumScript.covalentRadius);
        //    //}
        //}
        if (endaldegid && startaldegid)
        {
            Debug.Log("Formaldegid!");
            Zamestiteli(triangleScript, new List<int>() { 2, 3 }, atomScript, numberOfCarbone, numberOfCarbone, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil);
        }
        else if (endaldegid)
        {
            Zamestiteli(triangleScript, new List<int>() { 0, 2, 3 }, atomScript, numberOfCarbone, numberOfCarbone, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil);
        }
        else if (startaldegid)
        {
            Debug.Log("Start Aldegid!" + numberOfCarbone);
            Zamestiteli(triangleScript, new List<int>() { 1, 2, 3 }, atomScript, numberOfCarbone, numberOfCarbone, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil);
        }
        else
            Zamestiteli(triangleScript, new List<int>() {0, 1, 2, 3 }, atomScript, numberOfCarbone, numberOfCarbone, clores, bromes, ftores, iodes, ohs, tiols, aminGroups, methyl, ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil);



        oxygen.transform.position = triangleScript.PositionOnVector(2, oxygenScript.covalentRadius + atomScript.covalentRadius);
        atomScript.neiboursAtoms[0] = oxygen;

        //Oxygen electrons
        Vector3 oldPosition = triangle.transform.position;
        Vector3 oldRot = triangle.transform.eulerAngles;

        triangle.transform.position = triangleScript.PositionOnVector(2, 2 * atomScript.covalentRadius);
        triangle.transform.Rotate(0, -60, 0, Space.Self);
        triangleScript.UpdateVectors();
        for (int h = 0; h < 4; h++)
        {
            GameObject electron = Instantiate(electronPrefab);
            mustToDelate.Add(electron);
            triangleScript.TranslateGameObjectToVershina(electron, h, oxygenScript.radius);
            if (h == 2 || h == 1)
            {
                electron.transform.LookAt(triangleScript.transform);
                electron.transform.Translate(oxygenScript.distanceBetweenElectronsInPair / 2, 0, 0, Space.Self);
                GameObject electron1 = Instantiate(electronPrefab);
                mustToDelate.Add(electron1);
                triangleScript.TranslateGameObjectToVershina(electron1, h, oxygenScript.radius);
                electron1.transform.LookAt(triangleScript.transform);
                electron1.transform.Translate(-oxygenScript.distanceBetweenElectronsInPair / 2, 0, 0, Space.Self);
                oxygenScript.electrons[h] = electron1;
            }
            oxygenScript.electrons[h] = electron;
        }
        triangle.transform.position = carbone.transform.position;
        triangle.transform.eulerAngles = oldRot;
        if (lastCarbone != null)
            triangle.transform.LookAt(lastCarbone.transform);
        triangleScript.UpdateVectors();


        //Tetrahedron connecting
        tetrahedron.transform.position = triangleScript.PositionOnVector(1, 2 * atomScript.covalentRadius);
        tetrahedron.transform.LookAt(triangle.transform);
        tetrahedron.GetComponent<Tetrahedron>().UpdateVectors();
        dontChangePosition = true;
        lastCarbone = carbone;
        Destroy(triangle);
    }
    public void InstantiateMethylOrEthyl(int numberOfVershina, Atom nCarboneAtomScript, GibridizationScript script, int typeOfAlkil)
    {
        //typeOfAlkil
        //0-Methyl;
        //1-Ethyl;
        //2-n-Propil;
        //3-i-Propil
        //4-n-Butil
        //5-vtor-Butil
        //6-izo-Butil
        //7-tret-Butil


        //Постановка углерода
        GameObject carbone = Instantiate(curbsPrefab[5]);
        mustToDelate.Add(carbone);
        Atom methylCarboneScript = carbone.GetComponent<Atom>();
        methylCarboneScript.electrons = new GameObject[4];
        methylCarboneScript.neiboursAtoms = new GameObject[4];
        nCarboneAtomScript.neiboursAtoms[numberOfVershina] = carbone;
        methylCarboneScript.neiboursAtoms[0] = nCarboneAtomScript.gameObject;
        GameObject tetrahedron = Instantiate(tetrahedronPrefab) as GameObject;
        tetrahedron.transform.position = script.gameObject.transform.position;
        tetrahedron.transform.rotation = script.gameObject.transform.rotation;
        Tetrahedron tetrahegronScript = tetrahedron.GetComponent<Tetrahedron>();
        tetrahegronScript.Inicialization();
        Vector3 basePos = tetrahegronScript.gameObject.transform.position;
        Vector3 newPos = script.PositionOnVector(numberOfVershina, nCarboneAtomScript.covalentRadius * 2);
        tetrahegronScript.gameObject.transform.position = newPos;
        tetrahegronScript.transform.LookAt(basePos);
        tetrahegronScript.UpdateVectors();
        carbone.transform.position = tetrahedron.transform.position;

        //Электрон связи метила с основой
        GameObject electronOfMethyl = Instantiate(electronPrefab) as GameObject;
        mustToDelate.Add(electronOfMethyl);
        tetrahegronScript.TranslateGameObjectToVershina(electronOfMethyl, 0, methylCarboneScript.radius);
        methylCarboneScript.electrons[0] = electronOfMethyl;

        for (int i = 1; i <= 3; i++)
        {
            if (typeOfAlkil == 1 && i==2)
            {
                InstantiateMethylOrEthyl(2, methylCarboneScript, tetrahegronScript, 0);
            }
            else if(typeOfAlkil==2 && i == 1)
            {
                InstantiateMethylOrEthyl(1, methylCarboneScript, tetrahegronScript, 1);
            }
            else if(typeOfAlkil==3 && (i == 1 || i == 2))
            {
                InstantiateMethylOrEthyl(i, methylCarboneScript, tetrahegronScript, 0);
            }
            else if(typeOfAlkil==4 && i == 2)
            {
                InstantiateMethylOrEthyl(i, methylCarboneScript, tetrahegronScript, 2);
            }
            else if(typeOfAlkil==5 && (i==1 || i == 3))
            {
                if (i == 1)
                    InstantiateMethylOrEthyl(1, methylCarboneScript, tetrahegronScript, 0);
                if(i == 3)
                    InstantiateMethylOrEthyl(3, methylCarboneScript, tetrahegronScript, 1);
            }
            else if(typeOfAlkil==6 && i == 2)
            {
                InstantiateMethylOrEthyl(2, methylCarboneScript, tetrahegronScript, 3);
            }
            else if (typeOfAlkil == 7)
            {
                InstantiateMethylOrEthyl(i, methylCarboneScript, tetrahegronScript, 0);
            }
            else
            {
                //Постановка водорода
                GameObject hydrogenium = Instantiate(curbsPrefab[0]) as GameObject;
                mustToDelate.Add(hydrogenium);
                Atom hScript = hydrogenium.GetComponent<Atom>();
                hScript.neiboursAtoms = new GameObject[1];
                hScript.electrons = new GameObject[1];
                tetrahegronScript.TranslateGameObjectToVershina(hydrogenium, i, methylCarboneScript.covalentRadius + hScript.covalentRadius);
                hScript.neiboursAtoms[0] = carbone;
                methylCarboneScript.neiboursAtoms[i] = hydrogenium;

                //Электрон углерода
                GameObject electron = Instantiate(electronPrefab) as GameObject;
                mustToDelate.Add(electron);
                tetrahegronScript.TranslateGameObjectToVershina(electron, i, methylCarboneScript.radius);
                methylCarboneScript.electrons[i] = electron;

                //Углерод водорода
                GameObject eleectronOfHydrogenium = Instantiate(electronPrefab) as GameObject;
                mustToDelate.Add(eleectronOfHydrogenium);
                tetrahegronScript.TranslateGameObjectToVershina(eleectronOfHydrogenium, i, methylCarboneScript.covalentRadius + hScript.covalentRadius - hScript.radius);
                hScript.electrons[0] = eleectronOfHydrogenium;
            }

            //Destroy(tetrahedron);

        }
        Destroy(tetrahedron);
    }
    public void InstantiateAmine(int numberOfVershina, Atom nCarboneAtomScript, GibridizationScript script)
    {
        GameObject nitrogen = Instantiate(curbsPrefab[6]);
        mustToDelate.Add(nitrogen);
        Atom nitrogenScript = nitrogen.GetComponent<Atom>();
        nitrogenScript.electrons = new GameObject[5];
        nitrogenScript.neiboursAtoms = new GameObject[4];
        nCarboneAtomScript.neiboursAtoms[1] = nitrogen;
        nitrogenScript.neiboursAtoms[0] = nCarboneAtomScript.gameObject;
        GameObject tetrahedron2 = Instantiate(tetrahedronPrefab) as GameObject;
        tetrahedron2.transform.position = script.gameObject.transform.position;
        tetrahedron2.transform.rotation = script.gameObject.transform.rotation;
        Tetrahedron tetrahedron2Script = tetrahedron2.GetComponent<Tetrahedron>();
        tetrahedron2Script.Inicialization();
        Vector3 oldPos = tetrahedron2Script.gameObject.transform.position;
        Vector3 newPosition = script.PositionOnVector(numberOfVershina, nCarboneAtomScript.covalentRadius * 2);
        tetrahedron2Script.gameObject.transform.position = newPosition;
        tetrahedron2Script.transform.LookAt(oldPos);
        tetrahedron2Script.UpdateVectors();
        nitrogen.transform.position = tetrahedron2.transform.position;

        for (int h = 0; h < 4; h++)
        {
            GameObject electron = Instantiate(electronPrefab);
            mustToDelate.Add(electron);
            tetrahedron2Script.TranslateGameObjectToVershina(electron, h, nitrogenScript.radius);
            if (h == 1)
            {
                electron.transform.LookAt(tetrahedron2Script.transform);
                electron.transform.Translate(nitrogenScript.distanceBetweenElectronsInPair / 2, 0, 0, Space.Self);
                GameObject electron1 = Instantiate(electronPrefab);
                mustToDelate.Add(electron1);
                tetrahedron2Script.TranslateGameObjectToVershina(electron1, h, nitrogenScript.radius);
                electron1.transform.LookAt(tetrahedron2Script.transform);
                electron1.transform.Translate(-nitrogenScript.distanceBetweenElectronsInPair / 2, 0, 0, Space.Self);
                nitrogenScript.electrons[h] = electron1;
            }
            nitrogenScript.electrons[h] = electron;
        }
        for (int i = 2; i <= 3; i++)
        {
            GameObject hydrogenium = Instantiate(curbsPrefab[0]) as GameObject;
            Atom hydrogeniumScript = hydrogenium.GetComponent<Atom>();
            hydrogeniumScript.electrons = new GameObject[1];
            hydrogeniumScript.neiboursAtoms = new GameObject[1];
            hydrogeniumScript.neiboursAtoms[0] = nitrogen;
            nitrogenScript.neiboursAtoms[1] = hydrogenium;
            mustToDelate.Add(hydrogenium);
            tetrahedron2Script.TranslateGameObjectToVershina(hydrogenium, i, nitrogenScript.covalentRadius + hydrogeniumScript.covalentRadius);

            GameObject electronOFHydrogenium = Instantiate(electronPrefab);
            mustToDelate.Add(electronOFHydrogenium);
            tetrahedron2Script.TranslateGameObjectToVershina(electronOFHydrogenium, i, nitrogenScript.covalentRadius + hydrogeniumScript.covalentRadius - hydrogeniumScript.radius);
        }

        Destroy(tetrahedron2);
    }
    public void InstantiateGidroksilOrTiol( int numberOfVershina,Atom nCarboneAtomScript, GibridizationScript script, bool instantiateGidroksil)
    {
        GameObject oxygen;
        if (instantiateGidroksil)
            oxygen = Instantiate(curbsPrefab[7]);
        else
            oxygen = Instantiate(curbsPrefab[15]);
        mustToDelate.Add(oxygen);
        Atom oxygenScript = oxygen.GetComponent<Atom>();
        oxygenScript.electrons = new GameObject[6];
        oxygenScript.neiboursAtoms = new GameObject[2];
        nCarboneAtomScript.neiboursAtoms[1] = oxygen;
        oxygenScript.neiboursAtoms[0] = nCarboneAtomScript.gameObject;
        GameObject tetrahedron2 = Instantiate(tetrahedronPrefab) as GameObject;
        tetrahedron2.transform.position = script.gameObject.transform.position;
        tetrahedron2.transform.rotation = script.gameObject.transform.rotation;
        Tetrahedron tetrahedron2Script = tetrahedron2.GetComponent<Tetrahedron>();
        tetrahedron2Script.Inicialization();
        Vector3 oldPos = tetrahedron2Script.gameObject.transform.position;
        Vector3 newPosition = script.PositionOnVector(numberOfVershina, nCarboneAtomScript.covalentRadius * 2);
        tetrahedron2Script.gameObject.transform.position = newPosition;
        tetrahedron2Script.transform.LookAt(oldPos);
        tetrahedron2Script.UpdateVectors();
        oxygen.transform.position = tetrahedron2.transform.position;

        for (int h = 0; h < 4; h++)
        {
            GameObject electron = Instantiate(electronPrefab);
            mustToDelate.Add(electron);
            tetrahedron2Script.TranslateGameObjectToVershina(electron, h, oxygenScript.radius);
            if (h == 1 || h == 2)
            {
                electron.transform.LookAt(tetrahedron2Script.transform);
                electron.transform.Translate(oxygenScript.distanceBetweenElectronsInPair / 2, 0, 0, Space.Self);
                GameObject electron1 = Instantiate(electronPrefab);
                mustToDelate.Add(electron1);
                tetrahedron2Script.TranslateGameObjectToVershina(electron1, h, oxygenScript.radius);
                electron1.transform.LookAt(tetrahedron2Script.transform);
                electron1.transform.Translate(-oxygenScript.distanceBetweenElectronsInPair / 2, 0, 0, Space.Self);
                oxygenScript.electrons[h] = electron1;
            }
            oxygenScript.electrons[h] = electron;
        }

        GameObject hydrogenium = Instantiate(curbsPrefab[0]) as GameObject;
        Atom hydrogeniumScript = hydrogenium.GetComponent<Atom>();
        hydrogeniumScript.electrons = new GameObject[1];
        hydrogeniumScript.neiboursAtoms = new GameObject[1];
        hydrogeniumScript.neiboursAtoms[0] = oxygen;
        oxygenScript.neiboursAtoms[1] = hydrogenium;
        mustToDelate.Add(hydrogenium);
        tetrahedron2Script.TranslateGameObjectToVershina(hydrogenium, 3, oxygenScript.covalentRadius +hydrogeniumScript.covalentRadius);

        GameObject electronOFHydrogenium = Instantiate(electronPrefab);
        mustToDelate.Add(electronOFHydrogenium);
        tetrahedron2Script.TranslateGameObjectToVershina(electronOFHydrogenium, 3, oxygenScript.covalentRadius + hydrogeniumScript.covalentRadius - hydrogeniumScript.radius);

        Destroy(tetrahedron2);
    }
    public void Zamestiteli(GibridizationScript gibridScript, List<int> closedPositions, Atom atomScript, int numberOfCarbone, int n, List<int> clores, List<int> bromes, List<int> ftores , List<int> iodes, List<int>ohs , List<int>tiols, List<int>aminGroups, List<int> methyl, List<int> ethyl, List<int> npropil, List<int> ipropil, List<int> nbutil, List<int> vtorbutil, List<int> izobutil, List<int> tretbutil, bool cycl =false)
    {
        int i = numberOfCarbone;
        if (i == 1)
            Debug.Log("Zamestiteli!"+closedPositions.Count);
        for (int h = 0; h < 4; h++)
        {
            Debug.Log("Rasmatrivau: "+ h);
            GameObject electron = Instantiate(electronPrefab);
            mustToDelate.Add(electron);
            gibridScript.TranslateGameObjectToVershina(electron, h, atomScript.radius);
            atomScript.electrons[h] = electron;
            GameObject carbone = atomScript.gameObject;
            //if (((h != 0 && h != 1 && i != n) || (h != 0 && i == n) || (i == 1 && (h != 1 || n == 1)))&&!closedPositions.Contains(h)||(cycl&&!closedPositions.Contains(h)))
            if ((((h != 0 && h != 1 && i != n) || (h != 0 && i == n) || (i == 1 && (h != 1 || n == 1)))|| (cycl && !closedPositions.Contains(h))) && !closedPositions.Contains(h))
            {
                if(i==1)
                 Debug.Log(closedPositions.Contains(h)+" : "+h);
                if (clores.Contains(numberOfCarbone))
                {
                    if(i==1)
                        Debug.Log("Clor est");
                    GameObject clorine = Instantiate(curbsPrefab[16]);
                    mustToDelate.Add(clorine);
                    Atom clorineScript = clorine.GetComponent<Atom>();
                    gibridScript.TranslateGameObjectToVershina(clorine, h, atomScript.covalentRadius + clorineScript.covalentRadius);
                    atomScript.neiboursAtoms[h] = clorine;
                    GameObject secondElectron = Instantiate(electronPrefab);
                    mustToDelate.Add(secondElectron);
                    gibridScript.TranslateGameObjectToVershina(secondElectron, h, atomScript.covalentRadius + clorineScript.covalentRadius - clorineScript.radius);
                    clorineScript.neiboursAtoms = new GameObject[1];
                    clorineScript.neiboursAtoms[0] = carbone;
                    clorineScript.electrons = new GameObject[1] { secondElectron };
                    clores.Remove(numberOfCarbone);
                }
                else if (bromes.Contains(numberOfCarbone))
                {
                    GameObject bromine = Instantiate(curbsPrefab[34]);
                    mustToDelate.Add(bromine);
                    Atom bromineScript = bromine.GetComponent<Atom>();
                    gibridScript.TranslateGameObjectToVershina(bromine, h, atomScript.covalentRadius + bromineScript.covalentRadius);
                    atomScript.neiboursAtoms[h] = bromine;
                    GameObject secondElectron = Instantiate(electronPrefab);
                    mustToDelate.Add(secondElectron);
                    gibridScript.TranslateGameObjectToVershina(secondElectron, h, atomScript.covalentRadius + bromineScript.covalentRadius - bromineScript.radius);
                    bromineScript.neiboursAtoms = new GameObject[1];
                    bromineScript.neiboursAtoms[0] = carbone;
                    bromineScript.electrons = new GameObject[1] { secondElectron };
                    bromes.Remove(numberOfCarbone);
                }
                else if (ftores.Contains(numberOfCarbone))
                {
                    GameObject ftorine = Instantiate(curbsPrefab[8]);
                    mustToDelate.Add(ftorine);
                    Atom ftorineScript = ftorine.GetComponent<Atom>();
                    gibridScript.TranslateGameObjectToVershina(ftorine, h, atomScript.covalentRadius + ftorineScript.covalentRadius);
                    atomScript.neiboursAtoms[h] = ftorine;
                    GameObject secondElectron = Instantiate(electronPrefab);
                    mustToDelate.Add(secondElectron);
                    gibridScript.TranslateGameObjectToVershina(secondElectron, h, atomScript.covalentRadius + ftorineScript.covalentRadius - ftorineScript.radius);
                    ftorineScript.neiboursAtoms = new GameObject[1];
                    ftorineScript.neiboursAtoms[0] = carbone;
                    ftorineScript.electrons = new GameObject[1] { secondElectron };
                    ftores.Remove(numberOfCarbone);
                }
                else if (iodes.Contains(numberOfCarbone))
                {
                    GameObject iodine = Instantiate(curbsPrefab[52]);
                    mustToDelate.Add(iodine);
                    Atom iodineScript = iodine.GetComponent<Atom>();
                    gibridScript.TranslateGameObjectToVershina(iodine, h, atomScript.covalentRadius + iodineScript.covalentRadius);
                    atomScript.neiboursAtoms[h] = iodine;
                    GameObject secondElectron = Instantiate(electronPrefab);
                    mustToDelate.Add(secondElectron);
                    gibridScript.TranslateGameObjectToVershina(secondElectron, h, atomScript.covalentRadius + iodineScript.covalentRadius - iodineScript.radius);
                    iodineScript.neiboursAtoms = new GameObject[1];
                    iodineScript.neiboursAtoms[0] = carbone;
                    iodineScript.electrons = new GameObject[1] { secondElectron };
                    iodes.Remove(numberOfCarbone);
                }
                else if (ohs.Contains(numberOfCarbone))
                {
                    InstantiateGidroksilOrTiol(h,atomScript, gibridScript, true);
                    ohs.Remove(numberOfCarbone);
                }
                else if (tiols.Contains(numberOfCarbone))
                {
                    InstantiateGidroksilOrTiol(h, atomScript, gibridScript, false);
                    tiols.Remove(numberOfCarbone);
                }
                else if (aminGroups.Contains(numberOfCarbone))
                {
                    InstantiateAmine(h, atomScript, gibridScript);
                    aminGroups.Remove(numberOfCarbone);
                }
                else if (methyl.Contains(numberOfCarbone))
                {
                    InstantiateMethylOrEthyl(h, atomScript, gibridScript,0);
                    methyl.Remove(numberOfCarbone);
                }
                else if (ethyl.Contains(numberOfCarbone))
                {
                    InstantiateMethylOrEthyl(h, atomScript, gibridScript, 1);
                    ethyl.Remove(numberOfCarbone);
                }
                else if (npropil.Contains(numberOfCarbone))
                {
                    InstantiateMethylOrEthyl(h, atomScript, gibridScript, 2);
                    npropil.Remove(numberOfCarbone);
                }
                else if (ipropil.Contains(numberOfCarbone))
                {
                    InstantiateMethylOrEthyl(h, atomScript, gibridScript, 3);
                    ipropil.Remove(numberOfCarbone);
                }
                else if (nbutil.Contains(numberOfCarbone))
                {
                    InstantiateMethylOrEthyl(h, atomScript, gibridScript, 4);
                    nbutil.Remove(numberOfCarbone);
                }
                else if (vtorbutil.Contains(numberOfCarbone))
                {
                    InstantiateMethylOrEthyl(h, atomScript, gibridScript, 5);
                    vtorbutil.Remove(numberOfCarbone);
                }
                else if (izobutil.Contains(numberOfCarbone))
                {
                    InstantiateMethylOrEthyl(h, atomScript, gibridScript, 6);
                    izobutil.Remove(numberOfCarbone);
                }
                else if (tretbutil.Contains(numberOfCarbone))
                {
                    InstantiateMethylOrEthyl(h, atomScript, gibridScript, 7);
                    tretbutil.Remove(numberOfCarbone);
                }
                else
                {
                        Debug.Log("Hydrogenium: "+h);
                    GameObject hydrogenium = Instantiate(curbsPrefab[0]);
                    mustToDelate.Add(hydrogenium);
                    Atom hScript = hydrogenium.GetComponent<Atom>();
                    gibridScript.TranslateGameObjectToVershina(hydrogenium, h, atomScript.covalentRadius + hScript.covalentRadius);
                    atomScript.neiboursAtoms[h] = hydrogenium;
                    GameObject secondElectron = Instantiate(electronPrefab);
                    mustToDelate.Add(secondElectron);
                    gibridScript.TranslateGameObjectToVershina(secondElectron, h, atomScript.covalentRadius + hScript.covalentRadius - hScript.radius);
                    hScript.neiboursAtoms = new GameObject[1];
                    hScript.neiboursAtoms[0] = carbone;
                    hScript.electrons = new GameObject[1] { secondElectron };
                    Debug.Log(hydrogenium.transform.position);
                }
            }
        }
    }
    public void Benzol(List<int> clores , List<int> bromes, List<int> ftores, List<int> iodes , List<int> ds, List<int> ts , List<int> ethers , List<int> ohs, List<int> sulfides, List<int> tiols, List<int>aminGroups, List<int>methyl, List<int>ethyl,List<int> npropil, List<int> ipropil, List<int> nbutil, List<int> vtorbutil, List<int> izobutil, List<int>tretbutil)
    {
        ClearWorld();
        GameObject carbone;
        GameObject triangle = Instantiate(trianglePrefab);
        Triangle triangleScript = triangle.gameObject.GetComponent<Triangle>();
        triangleScript.Inicialization();
        triangle.transform.position = new Vector3(2.4f,0,0);
        triangleScript.UpdateVectors();
        GameObject firstCarbone = null;
        GameObject lastCarbone = null;
        for(int i = 1; i <= 6; i++)
        {
            carbone = Instantiate(curbsPrefab[5]);
            if (i == 1)
                firstCarbone = carbone;
            mustToDelate.Add(carbone);
            Atom atomScript = carbone.GetComponent<Atom>();
            atomScript.electrons = new GameObject[4];
            atomScript.neiboursAtoms = new GameObject[4];
            carbone.transform.position = triangle.transform.position;
            Debug.Log(clores);
            Zamestiteli(triangleScript, new List<int>() { 0, 1, 3 }, atomScript, i, 6, clores, bromes, ftores, iodes, ohs, tiols,aminGroups, methyl,ethyl, npropil, ipropil, nbutil, vtorbutil, izobutil, tretbutil, true);
            triangle.transform.position = triangleScript.PositionOnVector(1, 2 * atomScript.covalentRadius);
            triangle.transform.Rotate(0, 60, 0, Space.Self);
            triangleScript.UpdateVectors();
            if (i != 1)
            {
                carbone.GetComponent<Atom>().neiboursAtoms[0] = lastCarbone;
                lastCarbone.GetComponent<Atom>().neiboursAtoms[1] = carbone;
            }
            if (i == 6)
            {
                firstCarbone.GetComponent<Atom>().neiboursAtoms[0] = carbone;
            }
            lastCarbone = carbone;
        }
        Destroy(triangle.gameObject);
    }

    public void SaveMoleculeOpenWindow()
    {

    }
    public void SaveMolecule(string nameOfMolecule)
    {
        
        
        string foldername = Path.Combine(Application.persistentDataPath, "molecules");
        string filename = Path.Combine(foldername, nameOfMolecule.ToString() + ".txt");
        //string filename = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
        Debug.Log(filename);
        Debug.Log(foldername);
        if (!Directory.Exists(foldername))
        {
            Directory.CreateDirectory(foldername);
        }
        atoms = new Dictionary<string, object>();
        atoms.Add("countOfObjects", mustToDelate.Count);
        for(int i = 0; i < mustToDelate.Count; i++)
        {
            Vector3 coordinates = mustToDelate[i].gameObject.transform.position;
            Atom script = mustToDelate[i].GetComponent<Atom>();
            int typeOfobject;
            if (script != null)
            {
                typeOfobject = script.number;
                Debug.Log(script.number);
            }
            else
                typeOfobject = 0;
            atoms.Add(i.ToString()+"type", typeOfobject);
            atoms.Add(i.ToString()+"x", coordinates.x);
            atoms.Add(i.ToString() + "y", coordinates.y);
            atoms.Add(i.ToString() + "z", coordinates.z);
            FileStream stream = File.Create(filename);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, atoms);
            stream.Close();
        }
    }
    public void LoadMolecule(string loadingMoleculeText)
    {
        string foldername = Path.Combine(Application.persistentDataPath, "molecules");
        string filename = Path.Combine(foldername, loadingMoleculeText + ".txt");
        //if (!File.Exists(Path.Combine(Application.persistentDataPath, "/" + loadingMoleculeText.ToString() + ".txt")))
        if (!File.Exists(filename))
        {
            Debug.Log("Not found!");
            return;
        }
        Dictionary<string, object> loadingAtoms;
        BinaryFormatter formatter = new BinaryFormatter();
        //FileStream stream = File.Open(Path.Combine(Application.persistentDataPath, "/" + loadingMoleculeText.ToString() + ".txt"), FileMode.Open);
        FileStream stream = File.Open(filename, FileMode.Open);
        loadingAtoms = formatter.Deserialize(stream) as Dictionary<string, object>;
        stream.Close();

        ClearWorld();
        int countOfObjects = (int)loadingAtoms["countOfObjects"];
        for (int i = 0; i < countOfObjects; i++)
        {
            int type = (int)loadingAtoms[i.ToString() + "type"];
            float x = (float)loadingAtoms[i.ToString() + "x"];
            float y = (float)loadingAtoms[i.ToString() + "y"];
            float z = (float)loadingAtoms[i.ToString() + "z"];


            GameObject obj;
            if (type != 0)
            {
                obj= Instantiate(curbsPrefab[type-1]);
            }
            else
            {
                obj = Instantiate(electronPrefab);
            }
            mustToDelate.Add(obj);
            obj.transform.position = new Vector3(x, y, z);
        }
    }
}


