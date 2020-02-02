using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public static int totalTime = 500;

    private static int timeLeft;
    public static List<GameplayRessource> characters = new List<GameplayRessource>();
    public static List<GameplayRessource> tools = new List<GameplayRessource>();
    public static List<GameplayEvent> eventsDone = new List<GameplayEvent>();
    public static List<GameplayRessource> charactersLost = new List<GameplayRessource>();

    public static int TimeLeft
    {
        get => timeLeft;
        set
        {
            timeLeft = value;
            if (PlayerData.timeLeft < 100 && !GameManager.Instance.EndMusicIsPlaying)
            {
                AudioManager.Instance.FadeMusic(AudioManager.Instance.endMusic, 3f);
                GameManager.Instance.EndMusicIsPlaying = true;
            }
        }
    }

    public static void Reset()
    {
        characters.Clear();
        tools.Clear();
        eventsDone.Clear();
        charactersLost.Clear();
    }


    public static string DamageRessource(string _ressourceName, bool _isCharacter, bool _destroy, bool _setToDamaged, string _feedback, string _overrideFeedback, string _overrideFeedbackDeath)
    {
        GameplayRessource ressource;
        if (_isCharacter)
            ressource = characters.Find(x => x.ressourceName == _ressourceName);
        else
            ressource = tools.Find(x => x.ressourceName == _ressourceName);

        if (ressource != null)
        {
            if (_setToDamaged)
            {
                ressource.damaged = true;
                return _feedback;
            }

            // Trouver un moyen de communiquer qui est mort ce tour ci 
            if (ressource.damaged || _destroy)
            {
                if (_isCharacter)
                {
                    charactersLost.Add(ressource);
                    characters.Remove(ressource);
                    if (_feedback != null)
                    {
                        if (string.IsNullOrEmpty(_overrideFeedback))
                            _feedback += "\n" + ressource.ressourceName + " est mort. "; // :'(
                        else
                            _feedback += "\n" + _overrideFeedbackDeath;
                    }
                }
                else
                {
                    tools.Remove(ressource);
                    if (_feedback != null)
                    {
                        if (string.IsNullOrEmpty(_overrideFeedback))
                            _feedback += "\n" + ressource.ressourceName + " s'est cassé. "; // :'(
                        else
                            _feedback += "\n" + _overrideFeedbackDeath;
                    }
                }
            }
            else
            {
                if (_feedback != null)
                {
                    if (string.IsNullOrEmpty(_overrideFeedback))
                        _feedback += "\n" + ressource.ressourceName + ((_isCharacter) ? " s'est blessé. " : " s'est usé. "); // :'(
                    else
                        _feedback += "\n" + _overrideFeedback;
                }
            }

            ressource.damaged = true;
        }

        return _feedback;
    }

    public static void ShuffleRessources()
    {
        Shuffle(characters);
        Shuffle(tools);
    }

    static void Shuffle(List<GameplayRessource> _array)
    {
        for (int i = _array.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i);

            GameplayRessource tmp = _array[i];
            _array[i] = _array[j];
            _array[j] = tmp;
        }
    }

    public static List<GameplayRessource> GetGameplayResourceList(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Characters:
                return characters;
            case ResourceType.Tools:
                return tools;
            default:
                return null;
        }
    }

    public static bool CheckIfGameIsOver()
    {
        if (charactersLost.Find(x => !x.canBeLost) != null)
            return true;

        if (TimeLeft < 0)
            return true;

        return false;
    }
}
