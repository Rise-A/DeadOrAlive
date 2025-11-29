using System.Collections;
using System.Collections.Generic;
using kNN.BagOfWords;
using kNN.CosineSimilarity;
using UnityEngine;
using kNN.CosineSimilarity;
using kNN.BagOfWords;

public class QueryHandler : MonoBehaviour
{
    [Header("References")]

    [Header("Values")]
    [SerializeField] private string query;
    [SerializeField] private List<string> tokenizedQuery;

    public void SetQuery(string text)
    {
        query = text;
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Q)){
    //         Debug.Log(string.Join(", ", tokenizedQuery));
    //     }
    // }
}
