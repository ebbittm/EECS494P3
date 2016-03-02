using UnityEngine;
using System.Collections;

public class OxygenController : MonoBehaviour {

    public float MaxOxygen = 100f;
    public float CurrentOxygen = 1f;
    public float OxygenLoss = 0.5f;
    public float OxygenTankLength;
    public float LastTime = 0f;

    public Texture2D OxygenTank;
    public Texture2D OxygenLeft;

    public GUIBarScript g;

	void Awake () {
        OxygenTankLength = Screen.width / 2;
        //g = new GUIBarScript();
	}
	
	void Update () {
        if (Time.time - LastTime > 5.0)
        {
            LastTime = Time.time;
            CurrentOxygen -= .01f;
            g.SetNewValue(CurrentOxygen);
        }
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

    /*void OnGUI()
    {
        GUI.BeginGroup(new Rect(0, 0, OxygenTankLength, 32));
        GUI.Box(new Rect(0, 0, OxygenTankLength, 32), OxygenTank);
        GUI.BeginGroup(new Rect(0, 0, OxygenTankLength, 32), OxygenLeft);
        GUI.Box(new Rect(0, 0, OxygenTankLength, 32), OxygenTank);

        GUI.EndGroup();
        GUI.EndGroup();
    }
    */
}
