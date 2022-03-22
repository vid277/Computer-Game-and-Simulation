using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
public class BranchingDialogController : MonoBehaviour
{
    [SerializeField] private GameObject branchingCanvas;
    [SerializeField] private GameObject dialogPrefab;
    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private TextAssetValue dialogValue;
    [SerializeField] private Story myStory;
    [SerializeField] private GameObject dialogContainer;
    [SerializeField] private GameObject choiceContainer;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableCanvas(){
        branchingCanvas.SetActive(true);
        SetStory();
        RefreshView();
    }

    public void SetStory(){
        if (dialogValue.value){
            myStory = new Story(dialogValue.value.text);
        }
        else {
            Debug.Log("Ink. Story Setup Error.");
        }
    }

    public void RefreshView(){
        while(myStory.canContinue){
            MakeNewDialog(myStory.Continue());
        }
        if (myStory.currentChoices.Count > 0){
            MakeNewChoices();
        }
        else {
            branchingCanvas.SetActive(false);
        }
    }

    void MakeNewDialog(string newDialog){
        DialogObject newDialogObject = Instantiate(dialogPrefab, dialogContainer.transform).GetComponent<DialogObject>();
        newDialogObject.Setup(newDialog);
    }

    void MakeNewResponse(string newDialog, int choiceValue){
        ButtonObject newResponseObject = Instantiate(choicePrefab, choiceContainer.transform).GetComponent<ButtonObject>();
        newResponseObject.Setup(newDialog, choiceValue);
        Button responseButton = newResponseObject.gameObject.GetComponent<Button>(); 
        if (responseButton){
            responseButton.onClick.AddListener(delegate {
                ChooseChoice(choiceValue);
            });
        }
    }
    async void MakeNewChoices(){
        for (int i = 0; i < choiceContainer.transform.childCount; i++){
            Destroy(choiceContainer.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < myStory.currentChoices.Count; i++){
            MakeNewResponse(myStory.currentChoices[i].text, i);
        }
    }

    void ChooseChoice(int choice){
        myStory.ChooseChoiceIndex(choice);
        RefreshView();
    }
}
