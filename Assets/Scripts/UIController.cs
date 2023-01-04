using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public float TimeScale;
    bool timeRun = true;

    // 设置未感染人数
    public void SetUninfectedNum(string s)
    {
        GetComponent<CreateGuys>().UninfectedNum = int.Parse(s);
    }

    // 设置已感染人数
    public void SetInfectedNum(string s)
    {
        GetComponent<CreateGuys>().InfetctedNum = int.Parse(s);
    }   

    // 游戏变速控制
    public void Scale(bool value)
    {
        if (value)
        {
            TimeScale = 1f;
        }
        else
        {
            TimeScale = 2f;
        }

        if (timeRun)
        {
            Time.timeScale = TimeScale;
        }
    }

    // 游戏暂停
    public void Pause(bool value)
    {

        if (value)
        {
            timeRun = true;
            Time.timeScale = TimeScale;

        }
        else
        {
            timeRun = false;
            Time.timeScale = 0f;
        }
    }

    // 居家隔离
    public void Home(bool value)
    {
        if (!value)
        {
            GetComponent<CreateGuys>().IsHome = true;
        }
        else
        {
            GetComponent<CreateGuys>().IsHome = false;
        }

        GetComponent<CreateGuys>().Create();
    }

    // 设置按钮
    public void Setting()
    {
        GameObject canvas = GameObject.Find("Canvas");
        canvas.transform.Find("MainPanel").gameObject.SetActive(false);
        canvas.transform.Find("SettingPanel").gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start()
	{
        TimeScale = 1f;
    }

	// Update is called once per frame
	void Update()
	{

    }
}

