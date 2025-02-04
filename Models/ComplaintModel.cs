namespace courseraClone.Models;

public class ComplaintModel
{
    public int Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ComplaintText { get; set; } = string.Empty;

   
}


public class Complaint
{
    public int Id {get; set;}
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string ComplaintText { get; set; } = string.Empty;
}

public class Program
{
    ComplaintModel cm = new ComplaintModel();
    
    
}