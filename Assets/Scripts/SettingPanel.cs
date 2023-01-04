using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    CreateGuys createGuys;

    // 设置密接人数
    public void SetTouchNum(string s)
    {
        createGuys.TouchNum = int.Parse(s);
    }

    // 设置感染率
    public void SetInfectedRate(string s)
    {
        createGuys.InfectedRate = float.Parse(s);
    }

    // 设置痊愈天数
    public void SetCuredDays(string s)
    {
        int days = int.Parse(s);
        createGuys.CuredDays = days;
        List<GameObject> guys = createGuys.guys;
        foreach(GameObject guy in guys)
        {
            guy.GetComponent<Guy>().CuredDays = days;
        }
    }

    // 设置潜伏期天数
    public void SetIncubateDays(string s)
    {
        int days = int.Parse(s);
        createGuys.IncubateDays = days;
        List<GameObject> guys = createGuys.guys;
        foreach (GameObject guy in guys)
        {
            guy.GetComponent<Guy>().IncubateDays = days;
        }
    }

    // 设置免疫力天数
    public void SetImmuneDays(string s)
    {
        int days = int.Parse(s);
        createGuys.ImmuneDays = days;
        List<GameObject> guys = createGuys.guys;
        foreach (GameObject guy in guys)
        {
            guy.GetComponent<Guy>().ImmuneDays = days;
        }
    }

    public void Exit()
    {

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//编辑状态下退出
        #else
        Application.Quit();//打包编译后退出
        #endif

    }

    public void Close()
    {
        GameObject canvas = GameObject.Find("Canvas");
        canvas.transform.Find("MainPanel").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


    void Start()
    {
        createGuys = GameObject.Find("Main Camera").GetComponent<CreateGuys>();
        //ExitButton.GetComponent<Button>().onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
