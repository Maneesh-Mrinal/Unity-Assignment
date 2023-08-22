using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClientPopupUI : MonoBehaviour
{
    public GameObject popupPanel;
    public Text nameLabel;
    public Text pointsLabel;
    public Text addressLabel;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = popupPanel.GetComponent<CanvasGroup>();
        HidePopupImmediate();
    }

    public void ShowPopup(ClientData client)
    {
        nameLabel.text = client.label;
        pointsLabel.text = "Points: " + client.points.ToString();
        addressLabel.text = "Address: " + client.address;

        canvasGroup.alpha = 0;
        popupPanel.SetActive(true);

        canvasGroup.DOFade(1, 0.3f);
    }

    public void ClosePopup()
    {
        canvasGroup.DOFade(0, 0.3f).OnComplete(() => popupPanel.SetActive(false));
    }

    private void HidePopupImmediate()
    {
        popupPanel.SetActive(false);
    }
}
