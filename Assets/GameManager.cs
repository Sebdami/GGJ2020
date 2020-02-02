using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameplayEventManager gbm;
    private MapManager mapm;

    public Transform perso;

    public string cheat = "";

    public void Start()
    {
        mapm = MapManager.Instance;
        gbm = FindObjectOfType<GameplayEventManager>();

        if(cheat != "")
        {
            gbm.TriggerEvent(cheat);
        }
        else
        {
            gbm.TriggerEvent("L'appel de l'aventure");
        }
        MapManager.Instance.Init(gbm.currentEvent);
        perso.transform.position = mapm.GetPlayerTargetPosition();
        CameraStateMachine.Instance.Init(perso);

        PlayerData.characters.Add(new GameplayRessource("Robert", false));
        UIManager.Instance.RefreshData();
        MakePrefabAppear();
        PlayerData.timeLeft = PlayerData.totalTime;
        UIManager.Instance.RefreshData();
        UIManager.Instance.ShowPanel<UIEvent>();
    }


    public void AchoicewasMade(string info)
    {
        UIManager.Instance.RefreshData();
        UIManager.Instance.HidePanel<UIEvent>();

        if (gbm.currentEvent.eventTitle == "The End")
        {
            UIManager.Instance.HideAllPanel();
            UIManager.Instance.ShowPanel<UIRecapEnd>();
            return;
        }

        if (info == string.Empty)
        {
            WaitForAlterPrefab();
        }
        else
        {
            UIManager.Instance.ShowPanel<UIRecap>();
        }
    }

    public void Continue()
    {

        PlayerData.timeLeft -= 10;

        UIManager.Instance.RefreshData();
        UIManager.Instance.ShowPanel<UIEvent>();
    }

    public void MakePrefabAppear()
    {
        MapManager.Instance.CurrentChunk.Appear();
    }

    public void ActivatePrefab()
    {
        MapManager.Instance.CurrentChunk.Activate();
    }

    public void WaitForAlterPrefab()
    {

        if (gbm.choiceMade.alterPrefab)
        {
            ActivatePrefab();
            StartCoroutine(WaitBeforeNextAction(3f));
        }
        else
        {
            NextAction();
        }

    }
    public void NextAction()
    {
        string nextAction = gbm.choiceMade.ChoiceConsequences();

        if (string.IsNullOrEmpty(nextAction))
        {
            // Close UI
            // UIManager.Instance.
            UIManager.Instance.HideAllPanel();

            if (gbm.currentEvent.mapPrefab == null)
                Debug.Log(gbm.currentEvent.name + "has no chunk null");
            // Move

            if (!gbm.currentEvent.isReutilisable)
                PlayerData.eventsDone.Add(gbm.currentEvent);
            gbm.TriggerEvent();

            MapManager.Instance.GoToNextChunk(perso, gbm.currentEvent.mapPrefab,
                () => { Continue(); });

            // Call Trigger event on GEM quand le move est terminé
        }
        else
        {

            // Close UI
            UIManager.Instance.HidePanel<UIRecap>();
   

            // Call next event
            gbm.TriggerEvent(nextAction);

            UIManager.Instance.ShowPanel<UIEvent>();
        }

    }

    public IEnumerator WaitBeforeNextAction(float duration)
    {
        yield return new WaitForSeconds(duration);
        NextAction();

    }

}
