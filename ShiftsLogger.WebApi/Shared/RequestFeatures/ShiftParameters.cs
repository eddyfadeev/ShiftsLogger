namespace Shared.RequestFeatures;

public class ShiftParameters : RequestParameters
{
    public ShiftParameters() => OrderBy = "start_time";
    public DateTime? FromDate { get; set; } = DateTime.MinValue;
    public DateTime? ToDate { get; set; } = DateTime.MaxValue;
    public decimal MinWorkedHours { get; set; } = 0;
    public decimal MaxWorkedHours { get; set; } = int.MaxValue;

    public bool ValidWorkedHoursRange => 
        MaxWorkedHours > MinWorkedHours;
}