using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Controls a single slot reel:
/// - Visual spinning
/// - RNG-based final symbol selection
/// </summary>
public class Reel : MonoBehaviour
{
    [Header("Spin Settings")]
    [Tooltip("Visual speed of the reel spin")]
    public float spinSpeed = 1500f;

    [Tooltip("Vertical spacing between symbols")]
    public float symbolSpacing = 200f;

    [Header("Reel Symbols (Top to Bottom)")]
    public List<Transform> symbols = new List<Transform>();

    public bool IsSpinning { get; private set; }
    public string FinalSymbolName { get; private set; }

    void Update()
    {
        if (!IsSpinning) return;

        foreach (Transform symbol in symbols)
        {
            symbol.localPosition += Vector3.down * spinSpeed * Time.deltaTime;

            // Loop symbols for infinite spin illusion
            if (symbol.localPosition.y <= -symbolSpacing * symbols.Count)
            {
                symbol.localPosition += Vector3.up * symbolSpacing * symbols.Count;
            }
        }
    }

    public void StartSpin()
    {
        IsSpinning = true;
    }

    /// <summary>
    /// Stops spinning and selects final symbol using RNG.
    /// Visual position is snapped so the selected symbol lands at center.
    /// </summary>
    public void StopSpin()
    {
        IsSpinning = false;

        int randomIndex = Random.Range(0, symbols.Count);

        for (int i = 0; i < symbols.Count; i++)
        {
            float targetY = (i - randomIndex) * symbolSpacing;
            symbols[i].localPosition = new Vector3(0, targetY, 0);
        }

        FinalSymbolName = symbols[randomIndex].gameObject.name;
    }
}