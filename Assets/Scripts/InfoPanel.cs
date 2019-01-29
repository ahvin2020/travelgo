using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class InfoPanel : MonoBehaviour {

    public Text titleText;
    public Text descriptionText;
    public Image image;

    void Start() {
        //GetComponent<RectTransform>().localScale = Vector3.zero;
    }

	public void ShowPanel(LocationConfig locationConfig) {
        gameObject.SetActive(true);
        GetComponent<Animator>().SetBool("Show", true);

        titleText.text = locationConfig.name;
        descriptionText.text = locationConfig.description;
        image.sprite = Resources.Load<Sprite>(locationConfig.url);
    }

    public void HidePanel() {
        GetComponent<Animator>().SetBool("Show", false);
    }

    public void OnCardboardClick() {
        Debug.Log("CLICKY");
		SceneManager.LoadScene("vrscene");
    }

	public void OnCardboardClickFireworks()
	{
		SceneManager.LoadScene("vrscene_fireworks");
	}
}
