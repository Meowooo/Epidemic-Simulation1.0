using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGuys : MonoBehaviour
{
    public GameObject GuyPrfb;

    // 未感染人数
    public int UninfectedNum;
    // 已感染人数
    public int InfetctedNum;
    // 密接人数
    public int TouchNum;
    // 感染率
    public float InfectedRate;
    // 是否居家隔离
    public bool IsHome;

    // 生病到痊愈天数
    public int CuredDays;
    // 潜伏期到感染者天数
    public int IncubateDays;
    // 免疫有效天数
    public int ImmuneDays;

    // 创建的开始时间
    public float CreateTime;

    public GameObject StatisPanel;

    int allNum
    {
        get
        {
            return UninfectedNum + InfetctedNum;
        }
    }

    public List<GameObject> guys;

    System.Random random = new System.Random();



    //public enum InfectedStatesEnum { Uninfected, Incubate, Infected, Immune, Died };

    Dictionary<Guy.InfectedStatesEnum, int> statesCount = new Dictionary<Guy.InfectedStatesEnum, int>();

    // 生成人
    public void Create()
    {
        Clear();

        // 保存创建时间
        CreateTime = Time.time;

        // 开始统计
        StatisPanel.GetComponent<StatisPanel>().StartStatis();

        Vector2 center = new(0, 0);
        float angle = 0;
        float r = 3;
        float R = 5;
        float scale = 28f / (10f + allNum);

        // 打乱列表顺序
        List<GameObject> newList = new List<GameObject>();

        // 创建未感染并随机
        for (int i = 0; i < UninfectedNum; i++)
        {
            GameObject guy = (GameObject)GameObject.Instantiate(GuyPrfb);
            newList.Insert(random.Next(i + 1), guy);
        }

        // 创建已感染并随机
        for (int i = 0; i < InfetctedNum; i++)
        {
            GameObject guy = (GameObject)GameObject.Instantiate(GuyPrfb);
            guy.GetComponent<Guy>().Infected();
            newList.Insert(random.Next(UninfectedNum + i), guy);
        }

        // 写入分布信息
        foreach (GameObject guy in newList)
        {
            float hudu = (angle / 180f) * Mathf.PI;
            float xx = center.x + R * Mathf.Cos(hudu);
            float yy = center.y + r * Mathf.Sin(hudu);
            guy.transform.position = new Vector2(xx, yy);
            guy.transform.localScale = new Vector3(scale, scale, scale);

            guy.GetComponent<Guy>().IsHome = IsHome;
            guy.GetComponent<Guy>().CuredDays = CuredDays;
            guy.GetComponent<Guy>().IncubateDays = IncubateDays;
            guy.GetComponent<Guy>().ImmuneDays = ImmuneDays;

            guys.Add(guy);

            angle += (360f / allNum);
        }
    }

    // 清除所有人
    public void Clear()
    {
        foreach(GameObject guy in guys)
        {
            Destroy(guy);
        }
        guys.Clear();

        // 取消统计
        StatisPanel.GetComponent<StatisPanel>().EndStatis();
    }

    // 密接感染
    void TouchInfect()
    {
        // 滞后感染存储列表
        List<GameObject> infectedGuys = new List<GameObject>();

        for(int i=0; i< guys.Count; i++)
        {
            Guy.InfectedStatesEnum state = guys[i].GetComponent<Guy>().InfectedStates;
            bool isHome = guys[i].GetComponent<Guy>().IsHome;

            if ((state == Guy.InfectedStatesEnum.Infected && !isHome) || state == Guy.InfectedStatesEnum.Incubate)
            {
                // 密接人数>=1
                if (TouchNum >= 1) {infectedGuys.Add(guys[(i-1+guys.Count)%guys.Count]); }

                // 密接人数>=2
                if (TouchNum >= 2) {infectedGuys.Add(guys[(i+1)% guys.Count]);}

                // 密接人数>=3
                for(int j=0; j < TouchNum-2; j++)
                {
                    infectedGuys.Add(guys[random.Next(0, guys.Count)]);
                }

            }

        }

        // 滞后感染处理
        foreach (GameObject guy in infectedGuys)
        {
            double rdm = random.NextDouble();
            if (rdm <= InfectedRate)
            {
                guy.GetComponent<Guy>().Infected();
            }

        }

    }

    public void SetCuredDays(int value)
    {
        CuredDays = value;

    }

    void Start()
    {
        guys = new List<GameObject>();

        Create();

        InvokeRepeating("TouchInfect", 1, 1);

    }

    // Update is called once per frame
    void Update()
    {

    }
}