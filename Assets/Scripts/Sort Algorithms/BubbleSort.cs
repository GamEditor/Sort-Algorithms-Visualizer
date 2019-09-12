using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : SortBase, ISort
{
    public static BubbleSort Instance;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator StartSort(List<Bar> bars, float delayTime, Ordering order, Action onComplete)
    {
        CompareOperator compareOperator;

        switch(order)
        {
            case Ordering.Ascending:
                compareOperator = CompareOperator.Greater;
                break;
            case Ordering.Descending:
                compareOperator = CompareOperator.Less;
                break;
            default:
                compareOperator = CompareOperator.Greater;
                break;
        }
        
        int n = bars.Count;

        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (CompareTool.CompareValues(bars[j].Value, bars[j + 1].Value, compareOperator))
                {
                    // swap temp and array[i].Value
                    int temp = bars[j].Value;
                    bars[j].Value = bars[j + 1].Value;
                    bars[j + 1].Value = temp;

                    // changing bars color temporary
                    bars[j].SetColor(m_ModifierColor);
                    bars[j + 1].SetColor(m_ModifierColor);

                    yield return new WaitForSeconds(delayTime);

                    // reset bars color
                    bars[j].ResetColorToOriginal();
                    bars[j + 1].ResetColorToOriginal();
                }

        onComplete();
    }

    public void Sort(List<Bar> bars, float delay, Ordering order, Action onComplete)
    {
        StartCoroutine(StartSort(bars, delay, order, onComplete));
    }
}