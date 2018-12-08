using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public static GameUIController instance = null;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public GameObject EndGameCanvas;
    public Text EndGameText;
    
    private string defaultEndGameText = "";
    private Color defaultEndGameColor = Color.black;

    public void Show(string endGameText, Color endGameColor) {
        EndGameCanvas.SetActive(true);
        EndGameText.text = endGameText;
        EndGameText.color = endGameColor;
    }

    public void Hide() {
        EndGameCanvas.SetActive(false);
        Clear();
    }

    private void Clear() {
        EndGameText.color = defaultEndGameColor;
        EndGameText.text = defaultEndGameText;
    }
}
