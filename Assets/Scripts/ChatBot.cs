using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class MsgConfig {
    public string name;
    public string msg;
    public bool hasFollowUpMsg;
    public bool isLarge;

    public MsgConfig(string name, string msg, bool hasFollowUpMsg, bool isLarge) {
        this.name = name;
        this.msg = msg;
        this.hasFollowUpMsg = hasFollowUpMsg;
        this.isLarge = isLarge;
    }
}

public class ChatBot : MonoBehaviour {
    public static string CHAT_BOT_NAME = "Alice";
    public static string USER_NAME = "You";
    public static float DELAY_FINDING_RESULT = 1.5f;

    public Canvas chatBotCanvas;
    public Canvas itineraryCanvas;

    public Animator unityChanAnimator;

    private int currentMsgConfig = 0;
    public List<MsgConfig> msgConfigs;

    public ScrollRect chatHistoryScroll;    // scroller
    public Transform chatHistoryHolder;    // to hold all historyPanelPrefab
    public HistoryPanel historyPanelPrefab;

    public InputField msgInputField;

    void Start() {
        chatBotCanvas.gameObject.SetActive(true);
        itineraryCanvas.gameObject.SetActive(false);

        unityChanAnimator.SetTrigger("Intro");
        msgConfigs = new List<MsgConfig> {
            // intro
            new MsgConfig(CHAT_BOT_NAME, "Oh hi there! How may I help you?", false, false),

            // find destination of $200
            new MsgConfig(CHAT_BOT_NAME, "Hmm... let me check for you...", true, false),
            new MsgConfig(CHAT_BOT_NAME, "Here you are! Malaysia, Singapore, Thailand, Indonesia.", false, true),

            // when do you which to go?
            new MsgConfig(CHAT_BOT_NAME, "Alright! When do you wish to go?", false, false),

            // get itinerary
            new MsgConfig(CHAT_BOT_NAME, "I see... Let me find some itineraries from other users...", true, true),
            new MsgConfig(CHAT_BOT_NAME, "I found 3 top recommended itineraries for you! Do you wish to see?", false, true),
        };
        historyPanelPrefab.gameObject.SetActive(false);

        // load json
        LoadJson();


        StartCoroutine(OnFinishTweening());
    }

    private void LoadJson() {
        TextAsset postData = (TextAsset)Resources.Load("data/tripcase", typeof(TextAsset));
        JSONNode resData = JSONNode.Parse(postData.text) as JSONNode;

        // items
       // JSONArray items = resData["items"] as JSONArray;

        //foreach (JSONNode item in items) {
        //    item[""]
        //}
    }

    IEnumerator OnFinishTweening() {
        yield return new WaitForSeconds(DELAY_FINDING_RESULT);
        BotReply();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
			SubmitMsg();
        }
    }

	public void SubmitMsg()
	{
        if (msgInputField.text != "") {
            //ShowItinerary();
            InstantiateHistoryPanel(USER_NAME, msgInputField.text, false, false);
            BotReply();
        }
	}

	public void BotReply() {
        if (currentMsgConfig < msgConfigs.Count) {
            // show msg from bot
            InstantiateHistoryPanel(msgConfigs[currentMsgConfig].name, msgConfigs[currentMsgConfig].msg, true, msgConfigs[currentMsgConfig].isLarge);

            // clear input
            msgInputField.text = "";
            //msgInputField.ActivateInputField();
        } else {
            // show itinerary
            ShowItinerary();
        }
    }

    private void InstantiateHistoryPanel(string nameStr, string msgStr, bool isTween, bool isLarge) {
        HistoryPanel historyPanel = Instantiate<HistoryPanel>(historyPanelPrefab);
        historyPanel.transform.SetParent(chatHistoryHolder);
        historyPanel.GetComponent<RectTransform>().localScale = Vector3.one;
        historyPanel.gameObject.SetActive(true);

        historyPanel.Init(this, nameStr, msgStr, isTween, isLarge);

        Canvas.ForceUpdateCanvases();
        chatHistoryScroll.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    public void OnTextDisplayFinish() {
        if (msgConfigs[currentMsgConfig].hasFollowUpMsg == true) {
            StartCoroutine(OnFinishTweening());
        }

        currentMsgConfig++;
    }

    private void ShowItinerary() {
        chatBotCanvas.gameObject.SetActive(false);
        unityChanAnimator.SetTrigger("Jump");

        StartCoroutine(DoShowItinerary());
    }

    IEnumerator DoShowItinerary() {
        yield return new WaitForSeconds(3f);
        itineraryCanvas.gameObject.SetActive(true);
    }
    public void OnSingaporeRiverItineraryClick() {
        SceneManager.LoadScene("Map");
    }
}