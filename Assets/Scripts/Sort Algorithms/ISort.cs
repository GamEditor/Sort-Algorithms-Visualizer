using System;
using System.Collections.Generic;

public interface ISort
{
    void Sort(List<Bar> bars, float delay, Ordering order, Action onComplete);
}