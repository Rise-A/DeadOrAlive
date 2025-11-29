using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace kNN.BagOfWords{
    public class BagOfWords 
    {
        // private List<string> tokenizedQuery = new();
        // [SerializeField] private List<string> tokenizedQuery;
        [SerializeField] private static List<string> stopWords = new() {"the", "and", "but", "a", "has", "not", "he", "she", "they", "it", "the", "here", "is", "with", "this"};
        [SerializeField] private static List<string> delimiters = new() { ",", ";", ".", "?", "!", " "};

        // public static void AddToTokenizedQuery(List<string> tokenizedQuery, List<string> input)
        // {
        //     tokenizedQuery = input
        //         .SelectMany(ws => ws.Split(" ", StringSplitOptions.RemoveEmptyEntries))
        //         .Select(word => word.Trim(delimiters.Select(d => d[0]).ToArray()))
        //         .Select(word => word.ToLower())
        //         .Where(word => !string.IsNullOrWhiteSpace(word))
        //         .Where(word => !stopWords.Contains(word))
        //         .Distinct()
        //         .ToList();
        // }

        public static List<string> ReturnTokenizedQuery(List<string> input)
        {
            List<string> tokenizedQuery = input
                .SelectMany(ws => ws.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                .Select(word => word.Trim(delimiters.Select(d => d[0]).ToArray()))
                .Select(word => word.ToLower())
                .Where(word => !string.IsNullOrWhiteSpace(word))
                .Where(word => !stopWords.Contains(word))
                .Distinct()
                .ToList();
            
            return tokenizedQuery;
        }

        public static List<string> SeparateTokens(string input)
        {
            List<string> tokens = input.Split(" ").ToList();
            return tokens;
        }

        // public List<string> GetTokenizedQuery()
        // {
        //     return tokenizedQuery;
        // }

        public static List<int> VectorizeQuery(List<string> query, List<string> corpusVocab)
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
}