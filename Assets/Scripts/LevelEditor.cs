using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    public static LevelEditor instance;
    public Button buttonSend,buttonBring,buttonUpdate;
    string taslar,brickname;
    string[] bricks;
    public InputField levelField;
    public GameObject brick1,brick2,brick3, brick4, brick5, brick6, brick7, brick8,
        brick9,brick_gold,brick_inv,brick_3rd2,brick_3rd1,canvas;

    
    // Start is called before the first frame update
    void Start()
    {
      // buttonSend.onClick.AddListener(LevelSend);
      // buttonUpdate.onClick.AddListener(UpdateLevelStart);
      // buttonBring.onClick.AddListener(() => { BringLevelStart(int.Parse(levelField.text)); });
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LevelSend()
    {
        GameObject[] bricks_normal = GameObject.FindGameObjectsWithTag("tas");
        GameObject[] bricks_golden = GameObject.FindGameObjectsWithTag("gold_brick");
        GameObject[] bricks_3rd    = GameObject.FindGameObjectsWithTag("3rd_brick");
        GameObject[] bricks_3rd2  = GameObject.FindGameObjectsWithTag("2rd_brick");
        GameObject[] bricks_inv    = GameObject.FindGameObjectsWithTag("inv_brick");

        for (int i = 0; i < bricks_normal.Length; i++)
        {

            string[] ad = bricks_normal[i].name.Split(' ');

            taslar = taslar +
            (bricks_normal[i].transform.position.x) + "/" +
            (bricks_normal[i].transform.position.y) + "/" +
            (bricks_normal[i].transform.position.z) + "/" +
            (ad[0]) + "/";
        }
        for (int i = 0; i < bricks_golden.Length; i++)
        {

            string[] ad = bricks_golden[i].name.Split(' ');

            taslar = taslar +
            (bricks_golden[i].transform.position.x) + "/" +
            (bricks_golden[i].transform.position.y) + "/" +
            (bricks_golden[i].transform.position.z) + "/" +
            (ad[0]) + "/";
        }
        for (int i = 0; i < bricks_3rd.Length; i++)
        {

            string[] ad = bricks_3rd[i].name.Split(' ');

            taslar = taslar +
            (bricks_3rd[i].transform.position.x) + "/" +
            (bricks_3rd[i].transform.position.y) + "/" +
            (bricks_3rd[i].transform.position.z) + "/" +
            (ad[0]) + "/";
        }
        for (int i = 0; i < bricks_3rd2.Length; i++)
        {

            string[] ad = bricks_3rd2[i].name.Split(' ');

            taslar = taslar +
            (bricks_3rd2[i].transform.position.x) + "/" +
            (bricks_3rd2[i].transform.position.y) + "/" +
            (bricks_3rd2[i].transform.position.z) + "/" +
            (ad[0]) + "/";
        }
        for (int i = 0; i < bricks_inv.Length; i++)
        {

            string[] ad = bricks_inv[i].name.Split(' ');

            taslar = taslar +
            (bricks_inv[i].transform.position.x) + "/" +
            (bricks_inv[i].transform.position.y) + "/" +
            (bricks_inv[i].transform.position.z) + "/" +
            (ad[0]) + "/";
        }
        print(taslar);
        AddLevelStart();
    }
    void UpdateBricks()
    {
        GameObject[] bricks_normal = GameObject.FindGameObjectsWithTag("tas");
        GameObject[] bricks_golden = GameObject.FindGameObjectsWithTag("gold_brick");
        GameObject[] bricks_3rd = GameObject.FindGameObjectsWithTag("3rd_brick");
        GameObject[] bricks_3rd2 = GameObject.FindGameObjectsWithTag("2rd_brick");
        GameObject[] bricks_inv = GameObject.FindGameObjectsWithTag("inv_brick");

        for (int i = 0; i < bricks_normal.Length; i++)
        {

            string[] ad = bricks_normal[i].name.Split('(');

            taslar = taslar +
            (bricks_normal[i].transform.position.x) + "/" +
            (bricks_normal[i].transform.position.y) + "/" +
            (bricks_normal[i].transform.position.z) + "/" +
            (ad[0]) + "/";
        }
        for (int i = 0; i < bricks_golden.Length; i++)
        {

            string[] ad = bricks_golden[i].name.Split('(');

            taslar = taslar +
            (bricks_golden[i].transform.position.x) + "/" +
            (bricks_golden[i].transform.position.y) + "/" +
            (bricks_golden[i].transform.position.z) + "/" +
            (ad[0]) + "/";
        }
        for (int i = 0; i < bricks_3rd.Length; i++)
        {

            string[] ad = bricks_3rd[i].name.Split('(');

            taslar = taslar +
            (bricks_3rd[i].transform.position.x) + "/" +
            (bricks_3rd[i].transform.position.y) + "/" +
            (bricks_3rd[i].transform.position.z) + "/" +
            (ad[0]) + "/";
        }
        for (int i = 0; i < bricks_3rd2.Length; i++)
        {

            string[] ad = bricks_3rd2[i].name.Split('(');

            taslar = taslar +
            (bricks_3rd2[i].transform.position.x) + "/" +
            (bricks_3rd2[i].transform.position.y) + "/" +
            (bricks_3rd2[i].transform.position.z) + "/" +
            (ad[0]) + "/";
        }
        for (int i = 0; i < bricks_inv.Length; i++)
        {

            string[] ad = bricks_inv[i].name.Split('(');

            taslar = taslar +
            (bricks_inv[i].transform.position.x) + "/" +
            (bricks_inv[i].transform.position.y) + "/" +
            (bricks_inv[i].transform.position.z) + "/" +
            (ad[0]) + "/";
        }
        print(taslar);
    }
    public void AddLevelStart()
    {
        StartCoroutine(AddLevel());
    }
    public void UpdateLevelStart()
    {
        StartCoroutine(UpdateLevel());
    }
    public void BringLevelStart(int levelid)
    {
        StartCoroutine(BringLevel(levelid));

    }
    IEnumerator AddLevel()
    {
        WWWForm form = new WWWForm();
        form.AddField("level", taslar);
        WWW www = new WWW("http://localhost/dexball/leveladd.php", form);
        yield return www;
        string a = www.text;
        print(www.text);
        if (a == "0 ")
        {  
            Debug.Log("BAŞARILI");   
        } 
    }
    IEnumerator BringLevel(int levelid)
    {
        PlayerPrefs.SetInt("CurrentLevel", levelid);
        MenuScript.instance.GoMakeFalse();
        SceneManager.LoadScene(1);
        DeleteOldLevel();
        WWWForm form = new WWWForm();
        form.AddField("level", levelid);
        WWW www = new WWW("http://www.mustafademirtas.com/dexball/bringlevel.php", form);
        yield return www;
        print(www.text);
        GameOver.instance.health = 2;
        bricks = www.text.Split('/');
        Vector3 vector3 = new Vector3();
        
        for (int i = 0; i < bricks.Length -1; i = i + 4)
        {
            vector3.x = float.Parse(bricks[i]);
            vector3.y = float.Parse(bricks[i+1]);
            vector3.z = float.Parse(bricks[i+2]);
            brickname = bricks[i + 3];
            
            switch (brickname)
            {
                case "zbrick1":
                    Instantiate(brick1, vector3, Quaternion.identity);
                    break;
                case "zbrick2":
                    Instantiate(brick2, vector3, Quaternion.identity);
                    break;
                case "zbrick3":
                    Instantiate(brick3, vector3, Quaternion.identity);
                    break;
                case "zbrick4":
                    Instantiate(brick4, vector3, Quaternion.identity);
                    break;
                case "zbrick5":
                    Instantiate(brick5, vector3, Quaternion.identity);
                    break;
                case "zbrick6":
                    Instantiate(brick6, vector3, Quaternion.identity);
                    break;
                case "zbrick7":
                    Instantiate(brick7, vector3, Quaternion.identity);
                    break;
                case "zbrick8":
                    Instantiate(brick8, vector3, Quaternion.identity);
                    break;
                case "zbrick9":
                    Instantiate(brick9, vector3, Quaternion.identity);
                    break;
                case "brick_gold":
                    Instantiate(brick_gold, vector3, Quaternion.identity);
                    break;
                case "brick_inv":
                    GameObject Go = Instantiate(brick_inv, vector3, Quaternion.identity);
                    Go.GetComponent<SpriteRenderer>().enabled = false;
                    break;
                case "brick_3rd":
                    Instantiate(brick_3rd1, vector3, Quaternion.identity);
                    break;
                case "brick_3rd_2":
                    Instantiate(brick_3rd2, vector3, Quaternion.identity);
                    break;
                default:
                    print("AAA natural");
                    break;
            }
     
           
        }


    }
    void DeleteOldLevel()
    {
        GameObject[] taslar, taslar2, taslar3, taslar4, taslar5, taslar6;
        taslar = GameObject.FindGameObjectsWithTag("tas");
        taslar2 = GameObject.FindGameObjectsWithTag("inv_brick");
        taslar3 = GameObject.FindGameObjectsWithTag("3rd_brick");
        taslar4 = GameObject.FindGameObjectsWithTag("2rd_brick");
        taslar5 = GameObject.FindGameObjectsWithTag("1rd_brick");
        taslar6 = GameObject.FindGameObjectsWithTag("gold_brick");
        taslar = taslar.Concat(taslar2).ToArray();
        if (taslar3 != null)
            taslar = taslar.Concat(taslar3).ToArray();
        if (taslar4 != null)
            taslar = taslar.Concat(taslar4).ToArray();
        if (taslar5 != null)
            taslar = taslar.Concat(taslar5).ToArray();
        if (taslar6 != null)
            taslar = taslar.Concat(taslar6).ToArray();


        foreach (GameObject tas in taslar)
        {
            Destroy(tas);
        }
    }
    IEnumerator UpdateLevel()
    {
        UpdateBricks();
        int levelid = int.Parse(levelField.text);
        WWWForm form = new WWWForm();
        form.AddField("level", levelid);
        form.AddField("bricks", taslar);
        WWW www = new WWW("http://www.mustafademirtas.com/dexball/updatelevel.php", form);
        yield return www;
        string a = www.text;
        print(www.text);
        if (a == "0 ")
        {
            Debug.Log("BAŞARILI");
        }
    }
}
