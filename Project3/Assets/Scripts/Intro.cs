using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour {

    public float characterDelay = 0.1f; // the delay between each character appearing on screen
    public float messageLife = 3f; // the amount of time the message stays on the screen after being completed
    public float cursorBlinkInterval = 0.8f; // the amount of time between cursor blinks
    public Color textBorderColor = new Color(0, 0, 0); // the color of the text's border
    public bool displayTextBorder = true; // whether or not to display the text border
    public string introText = "ERROR: Suit malfunction.\n\tUser control impossible.\n\tRemote control activated.";
    public GameObject UITextPrefab;

    private Text UIElement;
    private float borderOffset = 1.0f; // the offset used to position the text border
    private GameObject borderTextNW; // the duplicate GameObject used to give the text a border in the northwest direction
    private GameObject borderTextSE; // the duplicate GameObject used to give the text a border in the southeast direction
    private string completedText = ""; // the completed text of the message that will be displayed
    private float blinkTime = 0f; // the time at which the cursor last blinked
    private bool cursorDisplay = true; // controls cursor display when blinking
    private float timeElapsed = 0f; // the time elapsed since the message began playing

	// Use this for initialization
	void Start () {
        this.UIElement = this.GetComponent<Text>();
        CreateTextBorder();
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

    private void CreateTextBorder() {
        // don't display the border if the bool is set to false
        if (!displayTextBorder)
            return;

        // instantiate the objects
        this.borderTextNW = Instantiate(this.UITextPrefab);
        this.borderTextSE = Instantiate(this.UITextPrefab);

        // set the borders' parent to the same canvas
        this.borderTextNW.transform.SetParent(this.UIElement.transform.parent, false);
        this.borderTextSE.transform.SetParent(this.UIElement.transform.parent, false);

        // change render order of border text
        this.borderTextNW.transform.SetAsFirstSibling();
        this.borderTextSE.transform.SetAsFirstSibling();

        // move their positions to create the shadow
        Vector3 NWPos = this.UIElement.transform.position;
        Vector3 SEPos = this.UIElement.transform.position;
        NWPos.x += this.borderOffset;
        NWPos.y -= this.borderOffset;
        SEPos.x -= this.borderOffset;
        SEPos.y += this.borderOffset;
        this.borderTextNW.transform.position = NWPos;
        this.borderTextSE.transform.position = SEPos;

        // change the text borders' colors
        this.borderTextNW.GetComponent<Text>().color = this.textBorderColor;
        this.borderTextSE.GetComponent<Text>().color = this.textBorderColor;
    }

    private void UpdateTextBorder() {
        // don't display the border if the bool is set to false
        if (!displayTextBorder)
            return;

        // update the text elements of the borders to match the main text
        this.borderTextNW.GetComponent<Text>().text = this.UIElement.text;
        this.borderTextSE.GetComponent<Text>().text = this.UIElement.text;
    }
	
	// Update is called once per frame
	void Update () {
        this.timeElapsed += Time.deltaTime; // track time elapsed
        UpdateText(); // update the displayed text
        UpdateTextBorder(); // update the displayed text border
        if (this.timeElapsed > this.characterDelay * this.completedText.Length + messageLife) { // at the end of the message's lifespan
            EndMessage();
        }

	}
}
