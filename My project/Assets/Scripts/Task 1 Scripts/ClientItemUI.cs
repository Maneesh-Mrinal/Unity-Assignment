using UnityEngine;
using UnityEngine.UI;

public class ClientItemUI : MonoBehaviour
{
    public Text nameLabel;
    public Text pointsLabel;

    private ClientData clientData;
    private ClientListUI clientListUI;

    public void Initialize(ClientData client, ClientListUI listUI)
    {
        clientData = client;
        clientListUI = listUI;

        nameLabel.text = client.label;
        pointsLabel.text = "Points: " + client.points.ToString();
    }

    public void OnClientClicked()
    {
        clientListUI.ShowClientPopup(clientData);
    }
}
