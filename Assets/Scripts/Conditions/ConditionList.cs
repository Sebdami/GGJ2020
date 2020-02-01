using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GGJ
{
    [System.Serializable]
    public class ConditionList : Condition
    {
        public List<Condition> conditionList = new List<Condition>();

        public enum Required
        {
            AllTrueRequired,
            AnyTrueRequired
        }
        public Required required;

        public ConditionList()
        {
            this.conditionList = new List<Condition>();
            this.required = Required.AllTrueRequired;
        }

        public ConditionList(List<Condition> conditionList, Required required)
        {
            this.conditionList = conditionList;
            this.required = required;
        }

        public bool Check()
        {
            if (conditionList.Count == 0)
                return true;

            foreach (var condition in conditionList)
            {
                bool result = condition.Check();
                if (required == Required.AllTrueRequired && !result)
                    return false;

                if (required == Required.AnyTrueRequired)
                    return true;
            }

            switch (required)
            {
                case Required.AllTrueRequired:
                    return true;
                case Required.AnyTrueRequired:
                    return false;
                default:
                    return true;
            }
        }
    }
}