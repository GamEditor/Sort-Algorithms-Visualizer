using UnityEngine;

public static class ArrayGenerator
{
    private static int[] s_CreatedArray;
    public static int[] CreatedArray => s_CreatedArray;   // get { return s_Array; }

    /// <summary>
    /// Creating and storing an int[] array. you can access the generated array from -> ArrayGenerator.CreatedArray
    /// </summary>
    /// <param name="size">Size of array</param>
    /// <param name="minRange">minimum number in array can be this</param>
    /// <param name="maxRange">maximum number in array can be this</param>
    /// <returns>It'll returns the stored int[] array</returns>
    public static int[] CreateRandomArray(int size, uint minRange, uint maxRange)
    {
        s_CreatedArray = new int[size];

        // generating random numbers
        for (int i = 0; i < s_CreatedArray.Length; i++)
        {
            // filling randomArray by random numbers between minRange and maxRange
            s_CreatedArray[i] = Random.Range((int)minRange, (int)maxRange + 1);
        }

        return s_CreatedArray;
    }
}