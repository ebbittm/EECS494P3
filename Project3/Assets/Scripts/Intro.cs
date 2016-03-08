using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour {

    public float characterDelay = 0.1f; // the delay between each character appearing on screen
    public float messageLife = 3f; // the amount of time the message stays on the screen after being completed
    public float cursorBlinkInterval = 0.8f; // the amount of time between cursor blinks
    public string introText = "ERROR: Suit malfunction.\n\tUser control impossible.\n\tRemote control activated.";

    private Text UIElement;
    private string completedText = ""; // the completed text of the message that will be displayed
    private float blinkTime = 0f; // the time at which the cursor last blinked
    private bool cursorDisplay = true; // controls cursor display when blinking
    private float timeElapsed = 0f; // the time elapsed since the message began playing

	// Use this for initialization
	void Start () {

        this.UIElement = this.GetComponent<Text>();
        PlayMessage(this.introText);

	}

    // kicks off a message across the screen using a "typing" effect
    void PlayMessage(string messageText) {
        // replace inspector special characters with actual special characters
        messageText = messageText.Replace("\\n", "\n"); // newline
        messageText = messageText.Replace("\\t", "\t"); // tab

        this.timeElapsed = 0f; // reset elapsedTime
        this.completedText = messageText; // establish completedMessage state
    }

    // marks a playing message as having completed and drops it
    void EndMessage() {
        this.completedText = "";
    }

    int min(int a, int b) {
        if (a < b)
            return a;
        else
            return b;
    }

    // updates the text displayed in the UIElement based on timeElapsed and completedText
    // this is used to achieve a "typing" effect when displaying text
    private void UpdateText() {
        int numCompletedChars = (int)(this.timeElapsed / this.characterDelay); // find the number of characters that should have been displayed
        string currentText = this.completedText.Substring(0, min(numCompletedChars, this.completedText.Length)); // get the characters that have been completed from the completed string
        this.UIElement.text = currentText; // update the actual UIElement
        Cursor(); // accomplish cursor display
    }

    private void Cursor() {
        if (this.timeElapsed - this.blinkTime >= this.cursorBlinkInterval) { // if it's time for the cursor to blink (change states)
            this.cursorDisplay = !this.cursorDisplay; // reverse the state of the cursor
            this.blinkTime = this.timeElapsed; // update time of last blink
        }

        // the if statements below accomplish the following:
        // the cursor does NOT exist if there is no message to display
        // the cursor exists if the message hasn't been fully displayed yet
        // if the message has been fully displayed, the cursor blinks on the interval this.cursorBlinkInterval

        if (this.completedText != "") // if the message isn't empty (ie, a message is being displayed)
            if (this.timeElapsed < this.characterDelay * this.completedText.Length || this.cursorDisplay) // if the message hasn't been fully printed or if the cursor wouldn't be gone due to blink
                this.UIElement.text += "█"; // add the block/cursor character
    }
	
	// Update is called once per frame
	void Update () {
        this.timeElapsed += Time.deltaTime; // track time elapsed
        UpdateText(); // update the displayed text
        if (this.timeElapsed > this.characterDelay * this.completedText.Length + messageLife) { // at the end of the message's lifespan
            EndMessage();
        }

	}
}
