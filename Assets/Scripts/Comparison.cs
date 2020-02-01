[System.Serializable]
public enum Comparison
{
    Equal,
    GreaterThan,
    LowerThan
}

public static class ComparisonOperation
{
    public static bool CheckComparison(Comparison comparison, int valueA, int valueB)
    {
        switch (comparison)
        {
            case Comparison.Equal:
                return valueA == valueB;
            case Comparison.GreaterThan:
                return valueA > valueB;
            case Comparison.LowerThan:
                return valueA < valueB;
            default:
                return true;
        }
    }
}