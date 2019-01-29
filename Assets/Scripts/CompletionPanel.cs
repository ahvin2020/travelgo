using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompletionPanel : MonoBehaviour {

    public Text descriptionText;
    public Text completionText;
    public RectTransform completionAmount;

    public MapHandler mapHandler;

    private float oldCompletion;
    private float newCompletion;
    private float currentCompletion;

    public bool isShow;

	public void ShowPanel(string locationName, float oldCompletion, float newCompletion) {
        isShow = true;

        descriptionText.text = "Visited " + locationName + "!";
        completionText.text = oldCompletion.ToString("P0") + " Completed!";
        completionAmount.localScale = new Vector3(oldCompletion, 1, 1);

        this.oldCompletion = oldCompletion;
        this.newCompletion = newCompletion;
        this.currentCompletion = oldCompletion;

        // tween to show completion panel
        GetComponent<Animator>().SetBool("Show", true);

        StartCoroutine(doCompletionTween());
    }

   // private void OnCompletionPanelShow() {
        // do completion tween
   //     StartCoroutine(doCompletionTween());
  //  }

    IEnumerator doCompletionTween() {
        yield return new WaitForSeconds(2.2f);
        iTween.ValueTo(this.gameObject, iTween.Hash("from", oldCompletion,
                                                   "to", newCompletion,
                                                   "time", 1f,
                                                   "ignoretimescale", true,
                                                   "easeType", iTween.EaseType.easeInQuad,
                                                   "onupdate", "onCompletionUpdate",
                                                   "onupdatetarget", this.gameObject,
                                                   "oncomplete", "onCompletionTweenDone",
                                                   "oncompletetarget", this.gameObject
                                                   ));
    }

    private void onCompletionUpdate(float newValue) {
        currentCompletion = newValue;

        completionText.text = currentCompletion.ToString("P0") + " Completed!";
        completionAmount.localScale = new Vector3(currentCompletion, 1, 1);
    }

    private void onCompletionTweenDone() {
        StartCoroutine(HidePanel());
    }

    IEnumerator HidePanel() {
        yield return new WaitForSeconds(2f);
        GetComponent<Animator>().SetBool("Show", false);


        isShow = false;

        yield return new WaitForSeconds(0.5f);
        mapHandler.OnCompletionPanelHide();
    }

    /*
    public void UpdateAmountDisplay() {
        if (displayAmount < 0) {
            onTweenUpdate((int)GameState.me.CurrencyNormal);
        } else {
            iTween.ValueTo(this.gameObject, iTween.Hash("from", displayAmount,
                                                    "to", (int)GameState.me.CurrencyNormal,
                                                    "time", 0.5f,
                                                    "ignoretimescale", true,
                                                    "easeType", iTween.EaseType.easeInQuad,
                                                    "onupdate", "onTweenUpdate",
                                                    "onupdatetarget", this.gameObject
                                                    ));
        }
    }

    private void onTweenUpdate(int newValue) {
        displayAmount = newValue;
        normalCurrencyText.text = GameUtil.FormatInteger(displayAmount);// string.Format(GameUtil.FormatInteger(displayAmount) + "/" + GameUtil.FormatLong(GameState.me.CurrencyNormalCapacity));
    }
    */
}
