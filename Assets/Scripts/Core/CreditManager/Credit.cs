using System.Collections.Generic;

[System.Serializable]
public class Credit
{
    public string name;   // T�n
    public string role;   // Vai tr�
}

[System.Serializable]
public class CreditList
{
    public List<Credit> credits = new List<Credit>();
}
