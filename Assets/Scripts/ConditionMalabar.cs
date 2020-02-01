using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parce que quand y en a marre, y a la condition malabar
/// </summary>
[System.Serializable]
public class ConditionMalabar
{
    public enum ConditionType
    {
        EventBased,
        Characters,
        Tools,
        Time
    }

    public enum ConditionCheck
    {
        Greater,
        Equals
    }

    public bool invertCondition;
    public ConditionType type;
    public ConditionCheck checkType;
    public int intParameter;
    public string stringParameter;

    public bool Check()
    {
        bool result = false;
        switch (type)
        {
            case ConditionType.EventBased:
                result = PlayerData.eventsDone.Find(x => x.name == stringParameter);
                break;
            case ConditionType.Characters:
                switch (checkType)
                {
                    case ConditionCheck.Greater:
                        result = PlayerData.characters.Count > intParameter;
                        break;
                    case ConditionCheck.Equals:
                        result = PlayerData.characters.Find(x => x.ressourceName == stringParameter) != null;
                        break;
                    default:
                        break;
                }
                break;
            case ConditionType.Tools:
                switch (checkType)
                {
                    case ConditionCheck.Greater:
                        result = PlayerData.tools.Count > intParameter;
                        break;
                    case ConditionCheck.Equals:
                        result = PlayerData.tools.Find(x => x.ressourceName == stringParameter) != null;
                        break;
                    default:
                        break;
                }
                break;
            case ConditionType.Time:
                result = PlayerData.totalTime - PlayerData.timeLeft > intParameter;
                break;
            default:
                break;
        }

        return (invertCondition) ? !result : result;
    }
}
