using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : Singleton<WordManager>
{
    public WordData lieux;
    public WordData noms;
    public WordData animaux;

    protected override void SingletonAwake()
    {
        base.SingletonAwake();
        Reload();
    }

    public void Reload()
    {
        WordGenerator.LoadLieux(lieux);
        WordGenerator.LoadNom(noms);
        WordGenerator.LoadAnimal(animaux);
    }
}
