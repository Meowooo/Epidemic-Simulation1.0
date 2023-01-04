using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisPanel : MonoBehaviour
{
    public GameObject UninfectedNum;
    public GameObject IncubateNum;
    public GameObject InfectedNum;
    public GameObject ImmuneNum;
    public GameObject SecondNum;
    public GameObject DayNum;
    public float StatisPeriod;

    // 二次感染计数器
    public int SecondCount;

    GameObject mainCamera;

    List<GameObject> guys;
    Dictionary<Guy.InfectedStatesEnum, int> statisDict = new();

    void _Statis()
    {
        SecondCount = 0;

        foreach (Guy.InfectedStatesEnum state in System.Enum.GetValues(typeof(Guy.InfectedStatesEnum)))
        {
            statisDict[state] = 0;
        }

        foreach (GameObject guy in mainCamera.GetComponent<CreateGuys>().guys)
        {
            Guy.InfectedStatesEnum state = guy.GetComponent<Guy>().InfectedStates;
            statisDict[state] += 1;
            if(guy.GetComponent<Guy>().SecondCount > 1)
            {
                SecondCount += 1;
            }
        }

        UninfectedNum.GetComponent<Text>().text = statisDict[Guy.InfectedStatesEnum.Uninfected].ToString();
        IncubateNum.GetComponent<Text>().text = statisDict[Guy.InfectedStatesEnum.Incubate].ToString();
        InfectedNum.GetComponent<Text>().text = statisDict[Guy.InfectedStatesEnum.Infected].ToString();
        ImmuneNum.GetComponent<Text>().text = statisDict[Guy.InfectedStatesEnum.Immune].ToString();
        SecondNum.GetComponent<Text>().text = SecondCount.ToString();
        DayNum.GetComponent<Text>().text = ((int)(Time.time - mainCamera.GetComponent<CreateGuys>().CreateTime)).ToString();
    } 

    public void StartStatis()
    {
        InvokeRepeating("_Statis", 0f, StatisPeriod);
    }

    public void EndStatis()
    {
        CancelInvoke("_Statis");
    }


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");

        foreach (Guy.InfectedStatesEnum state in System.Enum.GetValues(typeof(Guy.InfectedStatesEnum)))
        {
            statisDict.Add(state, 0);
        }

        //InvokeRepeating("Statis", 0f, StatisPeriod);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
