using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

[System.Serializable]
public class Stat {
    [SerializeField] private float baseValue;
    private List<float> flatModifiers = new List<float>();
    private List<float> percentModifiers = new List<float>();

    public float GetBaseValue() {
        return baseValue;
    }

    public float GetValue() {
        float totalFlatValue = 0f;
        flatModifiers.ForEach(x => totalFlatValue += x);

        float totalPercentValue = 0f;
        percentModifiers.ForEach(x => totalPercentValue += x);

        float finalValue = baseValue + totalFlatValue + totalPercentValue * baseValue;

        return finalValue;
    }

    public void AddFlatModifier(float modifier) {
        flatModifiers.Add(modifier);
    }

    public void RemoveFlatModifier(float modifier) {
        flatModifiers.Remove(modifier);
    }

    public void AddPercentModifier(float modifier) {
        percentModifiers.Add(modifier);
    }

    public void RemovePercentModifier(float modifier) {
        percentModifiers.Remove(modifier);
    }
}