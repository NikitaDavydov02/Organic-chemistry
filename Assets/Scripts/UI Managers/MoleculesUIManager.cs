using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleculesUIManager : MonoBehaviour {
    [SerializeField]
    public Button sintezButton;
    [SerializeField]
    public Text nOfCarbones;
    [SerializeField]
    public Text cloresText;
    [SerializeField]
    public Text bromesText;
    [SerializeField]
    public Text ftoresText;
    [SerializeField]
    public Text iodesText;
    [SerializeField]
    public Text dsText;
    [SerializeField]
    public Text tsText;
    [SerializeField]
    public Text etherText;
    [SerializeField]
    public Text ohText;
    [SerializeField]
    public Text sulfidesText;
    [SerializeField]
    public Text tiolText;
    [SerializeField]
    public Text aminGroupText;
    [SerializeField]
    public Text methylText;
    [SerializeField]
    public Text ethylText;
    [SerializeField]
    public Text npropilText;
    [SerializeField]
    public Text ipropilText;
    [SerializeField]
    public Text nbutilText;
    [SerializeField]
    public Text vtorbutilText;
    [SerializeField]
    public Text izobutilText;
    [SerializeField]
    public Text tretbutilText;
    [SerializeField]
    public Text cetonText;

    [SerializeField]
    public Toggle cyclicCompounds;
    [SerializeField]
    public List<Dropdown> zamestiteliBenzola;

    [SerializeField]
    public GameObject savePanel;
    [SerializeField]
    public Text saveText;
    [SerializeField]
    public Text loadText;


    private MainManager mainManager;
    // Use this for initialization
    void Start () {
        mainManager = this.gameObject.GetComponent<MainManager>();
        savePanel.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	}
    public void MainMenu()
    {
        Application.LoadLevel("Main Menu");
    }
    public void SintezOfAlkan()
    {
        if (!cyclicCompounds.isOn)
        {
            //Clores
            string[] clorN = cloresText.text.Split(' ');
            List<int> nOfClores = new List<int>();
            for (int i = 0; i < clorN.Length; i++)
            {
                int x;
                int.TryParse(clorN[i], out x);
                nOfClores.Add(x);
            }
            //Bromes
            string[] bromN = bromesText.text.Split(' ');
            List<int> nOfBromes = new List<int>();
            for (int i = 0; i < bromN.Length; i++)
            {
                int x;
                int.TryParse(bromN[i], out x);
                nOfBromes.Add(x);
            }
            //Ftores
            string[] ftorN = ftoresText.text.Split(' ');
            List<int> nOfFtores = new List<int>();
            for (int i = 0; i < ftorN.Length; i++)
            {
                int x;
                int.TryParse(ftorN[i], out x);
                nOfFtores.Add(x);
            }
            //Iodes
            string[] iodN = iodesText.text.Split(' ');
            List<int> nOfIodes = new List<int>();
            for (int i = 0; i < iodN.Length; i++)
            {
                int x;
                int.TryParse(iodN[i], out x);
                nOfIodes.Add(x);
            }
            //DS
            string[] dsN = dsText.text.Split(' ');
            List<int> nOfDs = new List<int>();
            for (int i = 0; i < dsN.Length; i++)
            {
                int x;
                int.TryParse(dsN[i], out x);
                nOfDs.Add(x);
            }
            //TS
            string[] tsN = tsText.text.Split(' ');
            List<int> nOfTs = new List<int>();
            for (int i = 0; i < tsN.Length; i++)
            {
                int x;
                int.TryParse(tsN[i], out x);
                nOfTs.Add(x);
            }
            //Ether
            string[] etherN = etherText.text.Split(' ');
            List<int> nOfEther = new List<int>();
            for (int i = 0; i < etherN.Length; i++)
            {
                int x;
                int.TryParse(etherN[i], out x);
                nOfEther.Add(x);
            }
            //OHs
            string[] ohN = ohText.text.Split(' ');
            List<int> nOfoh = new List<int>();
            for (int i = 0; i < ohN.Length; i++)
            {
                int x;
                int.TryParse(ohN[i], out x);
                nOfoh.Add(x);
            }
            //Sulfides
            string[] sulfidesN = sulfidesText.text.Split(' ');
            List<int> nOfSulfides = new List<int>();
            for (int i = 0; i < sulfidesN.Length; i++)
            {
                int x;
                int.TryParse(sulfidesN[i], out x);
                nOfSulfides.Add(x);
            }
            //Tiols
            string[] tiolN = tiolText.text.Split(' ');
            List<int> nOftiol = new List<int>();
            for (int i = 0; i < tiolN.Length; i++)
            {
                int x;
                int.TryParse(tiolN[i], out x);
                nOftiol.Add(x);
            }
            //AninGroups
            string[] aminGroupsN = aminGroupText.text.Split(' ');
            List<int> nOfaminGroups = new List<int>();
            for (int i = 0; i < aminGroupsN.Length; i++)
            {
                int x;
                int.TryParse(aminGroupsN[i], out x);
                nOfaminGroups.Add(x);
            }
            //Methyls
            string[] methylN = methylText.text.Split(' ');
            List<int> nOfmethyl = new List<int>();
            for (int i = 0; i < methylN.Length; i++)
            {
                int x;
                int.TryParse(methylN[i], out x);
                nOfmethyl.Add(x);
            }
            //Ethyls
            string[] ethylN = ethylText.text.Split(' ');
            List<int> nOfethyl = new List<int>();
            for (int i = 0; i < ethylN.Length; i++)
            {
                int x;
                int.TryParse(ethylN[i], out x);
                nOfethyl.Add(x);
            }
            //NPropiles
            string[] npropilN = npropilText.text.Split(' ');
            List<int> nOfnpropil = new List<int>();
            for (int i = 0; i < npropilN.Length; i++)
            {
                int x;
                int.TryParse(npropilN[i], out x);
                nOfnpropil.Add(x);
            }
            //i-Propiles
            string[] ipropilN = ipropilText.text.Split(' ');
            List<int> nOfipropil = new List<int>();
            for (int i = 0; i < ipropilN.Length; i++)
            {
                int x;
                int.TryParse(ipropilN[i], out x);
                nOfipropil.Add(x);
            }
            //NButil
            string[] nbutilN = nbutilText.text.Split(' ');
            List<int> nOfnbutil = new List<int>();
            for (int i = 0; i < nbutilN.Length; i++)
            {
                int x;
                int.TryParse(nbutilN[i], out x);
                nOfnbutil.Add(x);
            }
            //VtorButil
            string[] vtorbutilN = vtorbutilText.text.Split(' ');
            List<int> nOfvtorbutil = new List<int>();
            for (int i = 0; i < vtorbutilN.Length; i++)
            {
                int x;
                int.TryParse(vtorbutilN[i], out x);
                nOfvtorbutil.Add(x);
            }
            //IzoButil
            string[] izobutilN = izobutilText.text.Split(' ');
            List<int> nOfizobutil = new List<int>();
            for (int i = 0; i < izobutilN.Length; i++)
            {
                int x;
                int.TryParse(izobutilN[i], out x);
                nOfizobutil.Add(x);
            }
            //TretButil
            string[] tretbutilN = tretbutilText.text.Split(' ');
            List<int> nOftretbutil = new List<int>();
            for (int i = 0; i < tretbutilN.Length; i++)
            {
                int x;
                int.TryParse(tretbutilN[i], out x);
                nOftretbutil.Add(x);
            }
            //CetonButil
            string[] cetonN = cetonText.text.Split(' ');
            List<int> nOfceton = new List<int>();
            for (int i = 0; i < cetonN.Length; i++)
            {
                int x;
                int.TryParse(cetonN[i], out x);
                nOfceton.Add(x);
            }


            string s = nOfCarbones.text;
            int n = 0;
            int.TryParse(s, out n);
            Debug.Log("Alkan");
            mainManager.InstantiateAlkane(n, nOfClores, nOfBromes, nOfFtores, nOfIodes, nOfDs, nOfTs, nOfEther, nOfoh, nOfSulfides, nOftiol, nOfaminGroups, nOfmethyl, nOfethyl,nOfnpropil, nOfipropil, nOfnbutil, nOfvtorbutil, nOfizobutil, nOftretbutil, nOfceton);
        }
        else
        {
            List<int> clores = new List<int>();
            List<int> bromes = new List<int>();
            List<int> ftores = new List<int>();
            List<int> iodes = new List<int>();
            List<int> ds = new List<int>();
            List<int> ts = new List<int>();
            List<int> ethers = new List<int>();
            List<int> ohs = new List<int>();
            List<int> sulfides = new List<int>();
            List<int> tiols = new List<int>();
            List<int> aminGroups = new List<int>();
            List<int> methyls = new List<int>();
            List<int> ethyls = new List<int>();
            List<int> npropils = new List<int>();
            List<int> ipropils = new List<int>();
            List<int> nbutils = new List<int>();
            List<int> vtorbutils = new List<int>();
            List<int> izobutils = new List<int>();
            List<int> tretbutils = new List<int>();
            for (int i = 0; i < zamestiteliBenzola.Count; i++)
            {
                if (zamestiteliBenzola[i].value == 1)
                    clores.Add(i + 1);
                if (zamestiteliBenzola[i].value == 2)
                    bromes.Add(i + 1);
                if (zamestiteliBenzola[i].value == 3)
                    ftores.Add(i + 1);
                if (zamestiteliBenzola[i].value == 4)
                    iodes.Add(i + 1);
                if (zamestiteliBenzola[i].value == 5)
                    ohs.Add(i + 1);
                if (zamestiteliBenzola[i].value == 6)
                    tiols.Add(i + 1);
                if (zamestiteliBenzola[i].value == 7)
                    aminGroups.Add(i + 1);
                if (zamestiteliBenzola[i].value == 8)
                    methyls.Add(i + 1);
                if (zamestiteliBenzola[i].value == 9)
                    ethyls.Add(i + 1);
                if (zamestiteliBenzola[i].value == 10)
                    npropils.Add(i + 1);
                if (zamestiteliBenzola[i].value == 11)
                    ipropils.Add(i + 1);
                if (zamestiteliBenzola[i].value == 12)
                    nbutils.Add(i + 1);
                if (zamestiteliBenzola[i].value == 13)
                    vtorbutils.Add(i + 1);
                if (zamestiteliBenzola[i].value == 14)
                    izobutils.Add(i + 1);
                if (zamestiteliBenzola[i].value == 15)
                    tretbutils.Add(i + 1);
            }
            mainManager.Benzol(clores, bromes, ftores, iodes, ds, ts, ethers, ohs, sulfides, tiols, aminGroups, methyls, ethyls,npropils,ipropils, nbutils, vtorbutils, izobutils, tretbutils);
            //if (firstZamestiteValue == 1)
            //{
            //    mainManager.Benzol(new List<int>());
            //}
            //else
            //{
            //    Debug.Log("Usuall benzool");
            //    mainManager.Benzol(new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>());
            //}
        }
        
    }
    public void SetAlifaticActiveOrNot()
    {
        bool setValue = false;
        if (cyclicCompounds.isOn)
            setValue = false;
        else
            setValue = true;
        nOfCarbones.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
         cloresText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
         bromesText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        ftoresText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        iodesText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        dsText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        tsText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        etherText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        ohText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        sulfidesText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        aminGroupText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        tiolText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        methylText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        ethylText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        npropilText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        ipropilText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        nbutilText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        vtorbutilText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        izobutilText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        tretbutilText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        cetonText.gameObject.transform.parent.GetComponent<InputField>().interactable = setValue;
        
        foreach (Dropdown dropdown in zamestiteliBenzola)
            dropdown.interactable = !setValue;
    }
    public void SaveMoleculeOpen()
    {
        savePanel.gameObject.SetActive(true);
    }
    public void SaveMoleculeClose()
    {
        savePanel.gameObject.SetActive(false);
    }
    public void SaveMolecule()
    {
        if (string.IsNullOrEmpty(saveText.text))
            return;
        mainManager.SaveMolecule(saveText.text);
        SaveMoleculeClose();
    }
    public void LoadMolecule()
    {
        if (string.IsNullOrEmpty(loadText.text))
            return;
        mainManager.LoadMolecule(loadText.text);
    }
}
