using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HistoryPanel : MonoBehaviour {

    public ChatBot chatBot;

    public Text nameText;
    public Text msgText;

    public LayoutElement layoutElement;

    private string[] goatText;
    private int currentlyDisplayingText = 0;

    public void Init(ChatBot chatBot, string nameStr, string msgStr, bool isTween, bool isLarge) {
        this.chatBot = chatBot;

        nameText.text = nameStr + ":";
        if (nameStr == ChatBot.CHAT_BOT_NAME) {
            nameText.color = Color.blue;
        } else {
            nameText.color = Color.red;
        }

        msgText.text = "";

        if (isTween) {
            goatText = new string[] { msgStr };
            StartCoroutine(AnimateText());
        } else {
            msgText.text = msgStr;
        }

        if (isLarge) {
            layoutElement.minHeight = 38;
            msgText.GetComponent<RectTransform>().sizeDelta = new Vector2(msgText.GetComponent<RectTransform>().sizeDelta.x, 43f);
        } else {
            layoutElement.minHeight = 23;
            msgText.GetComponent<RectTransform>().sizeDelta = new Vector2(msgText.GetComponent<RectTransform>().sizeDelta.x, 28f);
        }
    }

    IEnumerator AnimateText() {
        for (int i = 0; i < (goatText[currentlyDisplayingText].Length + 1); i++) {
            msgText.text = goatText[currentlyDisplayingText].Substring(0, i);

            if (i == goatText.Length - 1) {
                chatBot.OnTextDisplayFinish();
            }

            yield return new WaitForSeconds(.03f);
        }
    }
}
