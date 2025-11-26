using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BagOfWords
{
    private List<string> tokenizedQuery = new();
    private List<string> stopWords = new() {"the", "and", "but", "a", "has", "not", "he", "she", "they", "it", "the", "here", "is", "with"};
    private List<string> delimiters = new() { ",", ";", ".", "?", "!", " "};

    public void AddToTokenizedQuery(List<string> input)
    {
        tokenizedQuery = input
            .SelectMany(ws => ws.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .Select(word => word.Trim(delimiters.Select(d => d[0]).ToArray()))
            .Select(word => word.ToLower())
            .Where(word => !string.IsNullOrWhiteSpace(word))
            .Where(word => !stopWords.Contains(word))
            .Distinct()
            .ToList();
    }

    public List<string> SeparateTokens(string input)
    {
        List<string> tokens = input.Split(" ").ToList();
        return tokens;
    }

    public List<string> GetTokenizedQuery()
    {
        return tokenizedQuery;
    }

    public List<int> VectorizeQuery(List<string> query, List<string> corpusVocab)
    {
        List<int> vectors = new();

        foreach (string s in corpusVocab)
        {
            if (query.Contains(s))
            {
                vectors.Add(1);
            }

            else
            {
                vectors.Add(0);
            }
        }
        return vectors;
    }
}
