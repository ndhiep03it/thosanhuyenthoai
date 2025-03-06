using System.Collections.Generic;

[System.Serializable]
public class Credit
{
    public string name;   // Tên
    public string role;   // Vai trò
}

[System.Serializable]
public class CreditList
{
    public List<Credit> credits = new List<Credit>();
}
