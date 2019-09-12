using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : SortBase, ISort
{
    public static QuickSort Instance;

    private CompareOperator m_CompareOperator;
    private float m_DelayTime;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// This function takes last element as pivot,  places the pivot element at its correct position in sorted array,
    /// and places all smaller (smaller than pivot) to left of pivot and all greater elements to right of pivot.
    /// </summary>
    /// <param name="bars"></param>
    /// <param name="low"></param>
    /// <param name="high"></param>
    /// <returns></returns>
    private IEnumerator Partition(List<Bar> bars, int low, int high, Action<List<Bar>, int, int, int> onPartitionCompleted)
    {
        int pivot = bars[high].Value;

        // index of smaller element 
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            // If current element is smaller than the pivot 
            if (CompareTool.CompareValues(bars[j].Value, pivot, m_CompareOperator))
            {
                i++;

                // swap bars[i].Value and bars[j].Value
                int temp = bars[i].Value;
                bars[i].Value = bars[j].Value;
                bars[j].Value = temp;

                // changing bars color temporary
                bars[i].SetColor(m_ModifierColor);
                bars[j].SetColor(m_ModifierColor);

                yield return new WaitForSeconds(m_DelayTime);

                // reset bars color
                bars[i].ResetColorToOriginal();
                bars[j].ResetColorToOriginal();
            }
        }

        // swap bars[i+1].Value and bars[high].Value (or pivot)
        int temp1 = bars[i + 1].Value;
        bars[i + 1].Value = bars[high].Value;
        bars[high].Value = temp1;

        // changing bars color temporary
        bars[i + 1].SetColor(m_ModifierColor);
        bars[high].SetColor(m_ModifierColor);

        yield return new WaitForSeconds(m_DelayTime);

        // reset bars color
        bars[i + 1].ResetColorToOriginal();
        bars[high].ResetColorToOriginal();

        onPartitionCompleted(bars, low, high, i + 1);

        yield break;
    }

    /// <summary>
    /// The main function that implements QuickSort() 
    /// </summary>
    /// <param name="bars">Array to be sorted</param>
    /// <param name="low">Starting index</param>
    /// <param name="high">Ending index</param>
    private void StartSort(List<Bar> bars, int low, int high)
    {
        if (low < high)
            StartCoroutine(Partition(bars, low, high, OnPartitionIsReady));
    }

    private void OnPartitionIsReady(List<Bar> bars, int low, int high, int partitionIndex)
    {
        // partitionIndex is partitioning index, bars[partitionIndex] is now at right place
        // Recursively sort elements before  partition and after partition
        StartSort(bars, low, partitionIndex - 1);
        StartSort(bars, partitionIndex + 1, high);
    }

    public void Sort(List<Bar> bars, float delay, Ordering order, Action onComplete)
    {
        switch (order)
        {
            case Ordering.Ascending:
                m_CompareOperator = CompareOperator.Less;
                break;
            case Ordering.Descending:
                m_CompareOperator = CompareOperator.Greater;
                break;
            default:
                m_CompareOperator = CompareOperator.Less;
                break;
        }
        m_DelayTime = delay;

        StartSort(bars, 0, bars.Count - 1);

        onComplete();
    }
}