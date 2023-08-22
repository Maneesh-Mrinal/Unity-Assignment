using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ClientListUI : MonoBehaviour
{
    public GameObject clientItemPrefab;
    public Transform contentParent;
    public Dropdown filterDropdown;
    public ClientPopupUI popupUI;

    private List<ClientData> allClients;
    public APIManager apiManager;


    private void Start()
    {
        apiManager = GetComponent<APIManager>();
        StartCoroutine(FetchAndPopulateClients());

        filterDropdown.onValueChanged.AddListener(OnFilterDropdownChanged);
    }

    private IEnumerator FetchAndPopulateClients()
    {
        yield return apiManager.FetchClientsData(clientsData =>
        {
            allClients = clientsData.clients;
            PopulateClientList(allClients);
        });
    }

    public void PopulateClientList(List<ClientData> clients)
    {
        ClearClientList();
        allClients = clients;

        foreach (ClientData client in clients)
        {
            GameObject clientItem = Instantiate(clientItemPrefab, contentParent);
            clientItem.GetComponent<ClientItemUI>().Initialize(client, this);
        }
    }

    public void ClearClientList()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnFilterDropdownChanged(int filterIndex)
    {
        List<ClientData> filteredClients = FilterClients(filterIndex);
        PopulateClientList(filteredClients);
    }

    private List<ClientData> FilterClients(int filterIndex)
    {
        switch (filterIndex)
        {
            case 1: // Managers only
                return allClients.FindAll(client => client.isManager);
            case 2: // Non-managers
                return allClients.FindAll(client => !client.isManager);
            default:
                return allClients;
        }
    }

    public void ShowClientPopup(ClientData client)
    {
        popupUI.ShowPopup(client);
    }
}
