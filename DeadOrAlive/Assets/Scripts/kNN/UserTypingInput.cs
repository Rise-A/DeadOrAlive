using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using kNN.BagOfWords;
using kNN.CosineSimilarity;

public class UserTypingInput : MonoBehaviour
{
    [Header("References")]
    public AttributeRegistry attributeRegistry;
    public TMP_InputField inputField;
    public Button executeQueryButton;
    public QueryHandler queryHandler;
    public TextMeshProUGUI queryVectorText;
    public RoundManager roundManager;
    [SerializeField] private List<string> separatedTokens;
    [SerializeField] private List<string> tokenizedQuery;
    [SerializeField] private List<int> vectorizedQuery;
    
    void Start()
    {
        executeQueryButton.onClick.AddListener(ExecuteQuery);
        // for (int i = 0; i < attributeRegistry.allAttributes.Count; i++)
        // {
        //     vectorizedQuery.Add(0);
        // }
    }

    public void ExecuteQuery()
    {
        roundManager.DisableAllPeopleCircles(); // To clear all targets from previous queries

        string input = inputField.text;
        separatedTokens = BagOfWords.SeparateTokens(input);
        tokenizedQuery = BagOfWords.ReturnTokenizedQuery(separatedTokens);
        vectorizedQuery = BagOfWords.VectorizeQuery(tokenizedQuery, attributeRegistry.allAttributes);
        queryVectorText.text = "[" + string.Join(", ", vectorizedQuery) + "]";

        roundManager.DisplayMostSimilarScores(vectorizedQuery);
        // Debug.Log(input);
    }

    // Get Methods
    //////////////
    
    public List<int> GetVectorizedQuery()
    {
        return vectorizedQuery;
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Q)){
    //         Debug.Log(string.Join(", ", tokenizedQuery));
    //     }
    // }
}
