using System.Collections.Generic;
using UnityEngine;

public static class WordGenerator
{
    public static string lieux = "$[Lieux]";
    public static string names = "$[Nom]";
    public static string animal = "$[Animal]";
    public static string heros = "$[Heros]";

    public static List<string> alreadyUsedPlace = new List<string>();
    public static List<string> alreadyUsedNom = new List<string>();
    public static List<string> alreadyUsedAnimal = new List<string>();

    public static void LoadLieux(WordData wordData)
    {
        alreadyUsedPlace.AddRange(wordData.wordList);
    }

    public static void LoadAnimal(WordData wordData)
    {
        alreadyUsedAnimal.AddRange(wordData.wordList);
    }

    public static void LoadNom(WordData wordData)
    {
        alreadyUsedNom.AddRange(wordData.wordList);
    }

    public static string ReplaceSentence(string sentence)
    {
        sentence = sentence.Replace(heros, "Robert");

        if (sentence.Contains(animal))
        {
            if(alreadyUsedAnimal.Count == 0)
                WordManager.Instance.Reload();
            
            var index = Random.Range(0, alreadyUsedAnimal.Count);

            sentence = sentence.Replace(animal, alreadyUsedAnimal[index]);
            alreadyUsedAnimal.RemoveAt(index);
        }


        if (sentence.Contains(lieux))
        {
            if (alreadyUsedPlace.Count == 0)
                WordManager.Instance.Reload();

            var index = Random.Range(0, alreadyUsedPlace.Count);


            sentence = sentence.Replace(lieux, alreadyUsedPlace[index]);
            alreadyUsedPlace.RemoveAt(index);
        }

        if (sentence.Contains(names))
        {
            if (alreadyUsedNom.Count ==  0)
                WordManager.Instance.Reload();

            var index = Random.Range(0, alreadyUsedNom.Count);


            sentence = sentence.Replace(names, alreadyUsedNom[index]);
            alreadyUsedNom.RemoveAt(index);
           
        }

        return sentence;
    }
}
