using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SQLCon : MonoBehaviour
{
    public static SQLCon instance;
    public InputField nameField;
    public Button submitButton;
    public GameObject panelUser;
    public Text text;
    int level;
    string[] leaderList;
    public Text[] textName, textLevel;
    // Start is called before the first frame update
    void Start()
    {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        StartCoroutine(InternetControl());
        level = PlayerPrefs.GetInt("Comp_Levels", 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CallRegister()
    {
        StartCoroutine(Register());
    }
    public void LeaderLoad()
    {
        StartCoroutine(LeaderBoard());
    }
    public void UpdateLoad()
    {
        StartCoroutine(UpdateLevelSQL());
    }
    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_name", nameField.text);
        form.AddField("level", level);
        WWW www = new WWW("http://www.mustafademirtas.com/dexball/register.php", form);
        yield return www;
        string a = www.text;
        print(www.text);
        print(nameField.text);
        if (a == "0 ")
        {
            PlayerPrefs.SetString("username", nameField.text);
            Debug.Log("BAŞARILI");
            panelUser.SetActive(false);
            print(nameField.text);
        }
        else if(a == "1")
        {
            if(PlayerPrefs.GetInt("Lang") == 0) {

                text.text = "Bağlantı başarısız";
            }
            else
            {
                text.text = "Connection Failed";
            }
        }
        else if (a == "2")
        {
            if (PlayerPrefs.GetInt("Lang") == 1)
            {
                text.text = "Kullanıcı adı alınmış";
            }
            else
            {
                text.text = "ID already taken";
            }
        }
    }

    IEnumerator UpdateLevelSQL()
    {
         
        WWWForm form = new WWWForm();
        form.AddField("user_name", PlayerPrefs.GetString("username"));
        print(PlayerPrefs.GetString("username"));
        level = PlayerPrefs.GetInt("Comp_Levels");
        form.AddField("level", level);
        WWW www = new WWW("http://www.mustafademirtas.com/dexball/update.php", form);
        yield return www;
        string a = www.text;
        print(a);
        if (a == "0")
        { 
            Debug.Log("BAŞARILI");
          
        }
        
    }

    IEnumerator LeaderBoard()
    {
        WWW www = new WWW("http://www.mustafademirtas.com/dexball/leaderboard.php");
        yield return www;
        print(www.text);

        leaderList = www.text.Split(',');
        
        int a = 0,b = 0;;
        for (int i = 0; i < leaderList.Length -1; i=i+2)
        {
            textName[a].text = leaderList[i];
            a++;
        }
        for (int i = 1; i < leaderList.Length; i = i + 2)
        {
            textLevel[b].text = leaderList[i];
            b++;
        }
        StopCoroutine(LeaderBoard());
    }
    IEnumerator InternetControl()
    {
        WWW www = new WWW("http://www.mustafademirtas.com/");
        yield return www;
        if(www.error != null)
        {
            Application.Quit();
        }
        else{
            print("Internet Var");
        }
    }
}
