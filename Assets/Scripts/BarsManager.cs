using System.Collections.Generic;
using UnityEngine;

public class BarsManager : MonoBehaviour
{
    public static BarsManager Instance;

    [SerializeField]
    private SortSettings m_SortSettings;

    [SerializeField]
    private Bar m_BarPrefab;

    [SerializeField]
    private Transform m_BarsParent;

    private List<Bar> m_Bars;
    private List<Bar> m_UsedBars;

    public List<Bar> Bars => m_UsedBars;

    /* bars information */
    private float m_BarWidth = 0;   // for making space between bars
    
    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()
    {
        CreateBars();

        m_BarWidth = (int)m_Bars[0].GetComponent<RectTransform>().sizeDelta.x;
    }

    private void CreateBars()
    {
        m_Bars = new List<Bar>();
        m_UsedBars = new List<Bar>();

        for (int i = 0; i < m_SortSettings.NumberOf_MaximumElements; i++)
        {
            Bar bar = Instantiate(m_BarPrefab, m_BarsParent);
            bar.Value = 0;
            bar.gameObject.SetActive(false);

            m_Bars.Add(bar);
        }
    }

    public void HideAllBars()
    {
        for (int i = 0; i < m_Bars.Count; i++)
            m_Bars[i].gameObject.SetActive(false);
    }

    public void SetValues(int[] values)
    {
        if (values.Length > m_SortSettings.NumberOf_MaximumElements)
        {
            Debug.LogWarning($"your array size is greater than {m_SortSettings.NumberOf_MaximumElements}");
            HideAllBars();
            return;
        }
        else if (values.Length < m_SortSettings.NumberOf_MinimumElements)
        {
            Debug.LogWarning($"your array size is less than {m_SortSettings.NumberOf_MinimumElements}");
            HideAllBars();
            return;
        }

        // hide all bars and reusing them
        HideAllBars();

        m_UsedBars.Clear();

        /* bring bars to middle of screen */
        float totalWidth = m_BarWidth * values.Length;
        float startPositionX = (totalWidth / 2.0f) - totalWidth;

        /* rendering bars on screen */
        for (int i = 0; i < values.Length; i++)
        {
            m_UsedBars.Add(m_Bars[i]);

            m_Bars[i].Value = values[i];
            m_Bars[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(startPositionX, 0, 0);
            m_Bars[i].gameObject.SetActive(true);

            startPositionX += m_BarWidth;
        }
    }
}