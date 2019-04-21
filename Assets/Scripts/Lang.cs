using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lang : MonoBehaviour
{
    public Button[] buttons = new Button[3];
    public Sprite[] tr = new Sprite[4], en = new Sprite[4];
    Text[] text;
    string[] en_text, tr_text;
    MenuScript scriptMenu;
    public Button button_tr,button_eng;
    public GameObject panel_lang;
    public Image image_lvl;
    // Start is called before the first frame update
    void Start()
    {
        button_tr.onClick.AddListener(() => { SelectLang(1); });
        button_eng.onClick.AddListener(() => { SelectLang(2); });
        LangChecker();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LangChecker()
    {
        if (PlayerPrefs.GetInt("Lang", 1) == 1)
        {
            buttons[0].image.sprite = tr[0];
            buttons[1].image.sprite = tr[1];
            buttons[2].image.sprite = tr[2];
            image_lvl.sprite = tr[3];
            image_lvl.rectTransform.localScale = new Vector3(1,2);
        }
        if (PlayerPrefs.GetInt("Lang", 1) == 2)
        {
            buttons[0].image.sprite = en[0];
            buttons[1].image.sprite = en[1];
            buttons[2].image.sprite = en[2];
            image_lvl.sprite = en[3];
            image_lvl.rectTransform.localScale = new Vector3(1, 1);
        }
    }
    void SelectLang(int a)
    {
        if (a == 1)
        {
            PlayerPrefs.SetInt("Lang", 1);
            LangChecker();
        }
        else
        {
            PlayerPrefs.SetInt("Lang", 2);
            LangChecker();
        }
        panel_lang.SetActive(false);
    }


}
