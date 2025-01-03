using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Fruits : MonoBehaviour
{
    public GameObject full;
    public GameObject halved;
    public GameObject rotten;
    public GameObject sliced;
    public GameObject unripe;

    public FruitsStateMachine fruitsStateMachine;
    public void Start()
    {
        fruitsStateMachine=GetComponent<FruitsStateMachine>();
        fruitsStateMachine.Init(new FruitsFullState(this,fruitsStateMachine));
    }

    public void Update()
    {
        fruitsStateMachine.currentState.UpdateLogic();
    }
    public void ChangeModel(GameObject newModel)
    {
        // Tắt tất cả các model
        full.SetActive(false);
        halved.SetActive(false);
        rotten.SetActive(false);
        sliced.SetActive(false);
        unripe.SetActive(false);

        newModel.SetActive(true);
    }
    public void ChangeState(FruitsState newState)
    {
        fruitsStateMachine.ChangeState(newState);
    }
    public IEnumerator DisableAndReactivate()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2f); 

        gameObject.SetActive(true); 
        ChangeState(new FruitsFullState(this, fruitsStateMachine)); 
    }
}
