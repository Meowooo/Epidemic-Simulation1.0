using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Guy : MonoBehaviour
{
    // 感染状态贴图
    public Sprite UninfectedImg;
    public Sprite IncubateImg;
    public Sprite InfectedImg;
    public Sprite DiedImg;

    // 当前感染状态
    public enum InfectedStatesEnum { Uninfected, Incubate, Infected ,Immune, Died} ;
    public InfectedStatesEnum InfectedStates;

    // 被感染的时间
    public int InfectedTime;
    // 生病到痊愈天数
    public int CuredDays;
    // 潜伏期到感染者天数
    public int IncubateDays;
    // 免疫有效天数
    public int ImmuneDays;
    // 感染次数
    public int SecondCount;

    // 身体
    public GameObject Body;

    // 潜伏期标志和计数器
    public GameObject IncubateCount;
    public GameObject IncubateSymbol;
    TMP_Text IncubateCountText;

    // 免疫期标志和计数器
    public GameObject ImmuneCount;
    public GameObject ImmuneSymbol;
    TMP_Text ImmuneCountText;

    // 居家隔离
    public GameObject Home;
    public bool IsHome;

    // 被感染
    public void Infected()
    {
        if (InfectedStates == InfectedStatesEnum.Uninfected)
        {
            InfectedTime = (int)Time.time;
            InfectedStates = InfectedStatesEnum.Incubate;
            Body.GetComponent<SpriteRenderer>().sprite = IncubateImg;
            IncubateCount.SetActive(true);
            IncubateSymbol.SetActive(true);
            SecondCount += 1;

        }
    }

    // 更新身体感染状态
    void UpdateInfectedStates()
    {
        // 潜伏期
        if(InfectedStates == InfectedStatesEnum.Incubate)
        {
            int elapsedTime = (int)Time.time - InfectedTime;
            if (elapsedTime >= IncubateDays)
            {
                InfectedStates = InfectedStatesEnum.Infected;
                Body.GetComponent<SpriteRenderer>().sprite = InfectedImg;
                IncubateCount.SetActive(false);
                IncubateSymbol.SetActive(false);

                if (IsHome)
                {
                    Home.SetActive(true);
                }

            }
            else
            {
                IncubateCountText.text = (IncubateDays - elapsedTime).ToString("f0");
            }
        }

        // 被感染期
        if (InfectedStates == InfectedStatesEnum.Infected)
        {
            if (((int)Time.time - InfectedTime) >= (IncubateDays + CuredDays))
            {
                InfectedStates = InfectedStatesEnum.Immune;
                Body.GetComponent<SpriteRenderer>().sprite = UninfectedImg;
                Home.SetActive(false);
                ImmuneCount.SetActive(true);
                ImmuneSymbol.SetActive(true);
            }
        }

        // 免疫期
        if (InfectedStates == InfectedStatesEnum.Immune)
        {
            float elapsedTime = (int)Time.time - InfectedTime - IncubateDays - CuredDays;
            if (elapsedTime >= ImmuneDays)
            {
                InfectedStates = InfectedStatesEnum.Uninfected;
                ImmuneCount.SetActive(false);
                ImmuneSymbol.SetActive(false);
            }
            else
            {
                ImmuneCountText.text = (ImmuneDays - elapsedTime).ToString("f0");
            }
        }
    }


    // 死亡
    public void Died()
    {
        GetComponent<SpriteRenderer>().sprite = DiedImg;
        InfectedStates = InfectedStatesEnum.Died;
    }

    // Start is called before the first frame update
    void Start()
    {
        SecondCount = 0;

        IncubateCountText = IncubateCount.GetComponentInChildren<TMP_Text>();
        ImmuneCountText = ImmuneCount.GetComponentInChildren<TMP_Text>();

        InvokeRepeating("UpdateInfectedStates", 0f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
    }

}
