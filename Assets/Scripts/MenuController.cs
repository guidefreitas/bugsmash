using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MenuController : MonoBehaviour {

	private GameManager gameManager;
	private Text countDownTextLabel;
	private Button button;

	void Awake () {
		this.gameManager = GameObject.FindGameObjectWithTag("GameManager")
									 .GetComponent<GameManager>();
		countDownTextLabel = this.GetComponentsInChildren<Text>()[0];
        button = this.GetComponentsInChildren<Button>()[0];
	}
	
	IEnumerator CountDownAndStart() {
        countDownTextLabel.text = "3";
        yield return new WaitForSeconds(1);
        countDownTextLabel.text = "2";
        yield return new WaitForSeconds(1);
        countDownTextLabel.text = "1";
        yield return new WaitForSeconds(1);
        countDownTextLabel.text = "Go!";
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
        gameManager.gameStarted = true;
        yield return new WaitForSeconds(1);

    }

	public void StartPressed()
    {
        
    }

    public void StartFocused()
    {
        StartCoroutine(CountDownAndStart());
    }

    public void StartLostFocus()
    {
        StopAllCoroutines();
		countDownTextLabel.text = "Ready?";
    }
}
