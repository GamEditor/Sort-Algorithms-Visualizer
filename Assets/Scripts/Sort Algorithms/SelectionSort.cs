using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSort : SortBase, ISort
{
    public static SelectionSort Instance;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator StartSort(List<Bar> bars, float delayTime, Ordering order, Action onComplete)
    {
        CompareOperator compareOperator;

        switch (order)
        {
            case Ordering.Ascending:
                compareOperator = CompareOperator.Less;
                break;
            case Ordering.Descending:
                compareOperator = CompareOperator.Greater;
                break;
            default:
                compareOperator = CompareOperator.Less;
                break;
        }

        int n = bars.Count;

        // One by one move boundary of unsorted subarray 
        for (int i = 0; i < n - 1; i++)
        {
            // Find the minimum element in unsorted array 
            int min_idx = i;
            for (int j = i + 1; j < n; j++)
                if (CompareTool.CompareValues(bars[j].Value, bars[min_idx].Value, compareOperator))
                    min_idx = j;

            // Swap the found minimum element with the first 
            // element 
            int temp = bars[min_idx].Value;
            bars[min_idx].Value = bars[i].Value;
            bars[i].Value = temp;

            // changing bars color temporary
            bars[min_idx].SetColor(m_ModifierColor);
            bars[i].SetColor(m_ModifierColor);

            yield return new WaitForSeconds(delayTime);

            // reset bars color
            bars[min_idx].ResetColorToOriginal();
            bars[i].ResetColorToOriginal();
        }

        onComplete();
    }

    public void Sort(List<Bar> bars, float delay, Ordering order, Action onComplete)
    {
        StartCoroutine(StartSort(bars, delay, order, onComplete));
    }
}