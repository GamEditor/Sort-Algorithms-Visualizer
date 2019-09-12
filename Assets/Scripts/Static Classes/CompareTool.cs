public class CompareTool
{
    public static bool CompareValues(int value1, int value2, CompareOperator compareOperator)
    {
        bool answer = false;

        switch(compareOperator)
        {
            case CompareOperator.Equal:
                answer = value1 == value2 ? true : false;
                break;

            case CompareOperator.Greater:
                answer = value1 > value2 ? true : false;
                break;

            case CompareOperator.GreaterOrEqual:
                answer = value1 >= value2 ? true : false;
                break;

            case CompareOperator.Less:
                answer = value1 < value2 ? true : false;
                break;

            case CompareOperator.LessOrEqual:
                answer = value1 <= value2 ? true : false;
                break;
        }

        return answer;
    }
}