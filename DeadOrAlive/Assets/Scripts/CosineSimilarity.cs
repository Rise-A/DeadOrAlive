using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosineSimilarity
{
    public double CalculateCosineSimilarity(int dotProduct, double magnitude1, double magnitude2)
    {
        if (magnitude1 == 0 || magnitude2 == 0)
        {
            return 0.0;
        }

        double cosineSimilarity = dotProduct / (magnitude1 * magnitude2);
        // cosineSimilarity = Math.Round(cosineSimilarity, 2);

        return cosineSimilarity;
    }

    public int CalculateDotProduct(List<int> vector1, List<int> vector2)
    {
        int dotProduct = 0;
        for (int i = 0; i < vector1.Count; i++)
        {
            dotProduct += vector1[i] * vector2[i];
        }

        return dotProduct;
    }

    public double CalculateMagnitude(List<int> vector)
    {
        int sum = 0;

        for (int i = 0; i < vector.Count; i++)
        {
            sum += (int) Math.Pow(vector[i], 2);
        }

        double magnitude = Math.Sqrt(sum);
        // Math.Round(magnitude, 2);
        return magnitude;
    }
}