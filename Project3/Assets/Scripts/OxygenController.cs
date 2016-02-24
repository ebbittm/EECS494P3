using UnityEngine;
using System.Collections;

public class OxygenController : MonoBehaviour {

    public float MaxOxygen = 100f;
    public float CurrentOxygen = 100f;
    public float OxygenLoss = 0.5f;
    public float OxygenTankLength;

    public Texture2D OxygenTank;
    public Texture2D OxygenLeft;

	void Awake () {
        OxygenTankLength = Screen.width / 2;
	}
	
	void Update () {
        LoseOxygen();
    }

    public void AddOxygen(float amount)
    {
        CurrentOxygen += amount;
        if(CurrentOxygen > MaxOxygen)
        {
            CurrentOxygen = MaxOxygen;
        }
        OxygenTankLength = (Screen.width / 2) * (CurrentOxygen / MaxOxygen);
    }

    void LoseOxygen()
    {
        CurrentOxygen -= OxygenLoss;
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(0, 0, OxygenTankLength, 32));
        GUI.Box(new Rect(0, 0, OxygenTankLength, 32), OxygenTank);
        GUI.BeginGroup(new Rect(0, 0, OxygenTankLength, 32), OxygenLeft);
        GUI.Box(new Rect(0, 0, OxygenTankLength, 32), OxygenTank);

        GUI.EndGroup();
        GUI.EndGroup();
    }
}
