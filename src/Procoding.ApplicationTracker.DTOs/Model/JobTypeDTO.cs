namespace Procoding.ApplicationTracker.DTOs.Model;

public class JobTypeDTO
{
    public JobTypeDTO(string value)
    {
        Value = value;
    }

    public JobTypeDTO()
    {

    }

    public string Value { get; set; }

}
