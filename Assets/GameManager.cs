using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameplayEventManager gbm;
    private MapManager mapm;

    public Transform perso;

    public void Start()
    {
        mapm = MapManager.Instance;
        gbm = FindObjectOfType<GameplayEventManager>();
        MapManager.Instance.Init();
        perso.transform.position = mapm.GetPlayerTargetPosition();
        CameraStateMachine.Instance.Init(perso);
        gbm.TriggerEvent("L'appel de l'aventure");
        PlayerData.characters.Add(new GameplayRessource("Robert", false));
     
        SpawnPrefab();
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
            UIManager.Instance.ShowPanel<UIRecapEnd>(); 
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
        if(!gbm.currentEvent.isReutilisable)
            PlayerData.eventsDone.Add(gbm.currentEvent);

        PlayerData.timeLeft -= 10;
        gbm.TriggerEvent();
        UIManager.Instance.RefreshData();
        UIManager.Instance.ShowPanel<UIEvent>();
    }

    Tile currentTile;

    public void SpawnPrefab()
    {
        // Load scene
        if (gbm.currentEvent.mapPrefab == null)
            return;

        currentTile = Instantiate(gbm.currentEvent.mapPrefab, mapm.GetPrefabTargetPosition(), Quaternion.identity, mapm.GetCurrentChuckTransform()).GetComponent<Tile>();
    }

    public void ActivatePrefab()
    {
        currentTile.Activate();
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
                // Move
                MapManager.Instance.GoToNextChunk(perso,
                    () => { Continue(); });

                // Call Trigger event on GEM quand le move est terminé
            }
            else
            {
                // Close UI
                UIManager.Instance.HideAllPanel();

                // Call next event
                gbm.TriggerEvent(nextAction);
            }

    }

    public IEnumerator WaitBeforeNextAction(float duration)
    {
        yield return new WaitForSeconds(duration);
        NextAction();

    }

}
