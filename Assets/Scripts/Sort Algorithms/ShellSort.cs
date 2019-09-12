using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSort : SortBase, ISort
{
    public static ShellSort Instance;

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
                compareOperator = CompareOperator.Greater;
                break;
            case Ordering.Descending:
                compareOperator = CompareOperator.Less;
                break;
            default:
                compareOperator = CompareOperator.Greater;
                break;
        }

        int array_size = bars.Count;

        int i, j, inc, temp;
        inc = 3;

        while (inc > 0)
        {
            for (i = 0; i < array_size; i++)
            {
                j = i;
                temp = bars[i].Value;

                while ((j >= inc) && CompareTool.CompareValues(bars[j - inc].Value, temp, compareOperator))
                {
                    bars[j].Value = bars[j - inc].Value;

                    int tempJ = j;  // because j will be changed in next statement

                    j = j - inc;

                    // changing bars color temporary
                    bars[tempJ].SetColor(m_ModifierColor);
                    bars[tempJ - inc].SetColor(m_ModifierColor);

                    yield return new WaitForSeconds(delayTime);

                    // reset bars color
                    bars[tempJ].ResetColorToOriginal();
                    bars[tempJ - inc].ResetColorToOriginal();
                }

                bars[j].Value = temp;
                yield return new WaitForSeconds(delayTime);
            }

            if (inc / 2 != 0)
                inc = inc / 2;
            else if (inc == 1)
                inc = 0;
            else
                inc = 1;

            yield return new WaitForSeconds(delayTime);
        }

        onComplete();
    }

    public void Sort(List<Bar> bars, float delay, Ordering order, Action onComplete)
    {
        StartCoroutine(StartSort(bars, delay, order, onComplete));
    }
}