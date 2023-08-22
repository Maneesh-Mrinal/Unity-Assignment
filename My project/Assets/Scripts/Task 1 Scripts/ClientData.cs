using System.Collections.Generic;

[System.Serializable]
public class ClientData
{
    public string label; // Rename 'name' to 'label'
    public int points;
    public string address;
    public bool isManager;
}

[System.Serializable]
public class ClientListData
{
    public List<ClientData> clients;
}
