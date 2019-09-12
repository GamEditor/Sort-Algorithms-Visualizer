using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionSort : SortBase, ISort
{
    public static InsertionSort Instance;

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

        //1. For each value in the array...
        for (int i = 1; i < bars.Count; ++i)
        {
            //2. Store the current value in a variable.
            int currentValue = bars[i].Value;
            int pointer = i - 1;

            //3. While we are pointing to a valid value...
            //4. If the current value is less than the value we are pointing at...
            while (pointer >= 0 && CompareTool.CompareValues(bars[pointer].Value, currentValue, compareOperator))
            {
                //5. Then move the pointed-at value up one space, and store the
                //   current value at the pointed-at position.
                bars[pointer + 1].Value = bars[pointer].Value;

                int tempPointer = pointer;  // because pointer will be changed in next statement
                
                pointer = pointer - 1;

                // changing bars color temporary
                bars[tempPointer].SetColor(m_ModifierColor);
                bars[tempPointer + 1].SetColor(m_ModifierColor);

                yield return new WaitForSeconds(delayTime);

                // reset bars color
                bars[tempPointer].ResetColorToOriginal();
                bars[tempPointer + 1].ResetColorToOriginal();
            }

            bars[pointer + 1].Value = currentValue;

            // changing bars color temporary
            bars[pointer + 1].SetColor(m_ModifierColor);

            yield return new WaitForSeconds(delayTime);

            // changing bars color temporary
            bars[pointer + 1].ResetColorToOriginal();
        }

        onComplete();
    }

    public void Sort(List<Bar> bars, float delay, Ordering order, Action onComplete)
    {
        StartCoroutine(StartSort(bars, delay, order, onComplete));
    }
}