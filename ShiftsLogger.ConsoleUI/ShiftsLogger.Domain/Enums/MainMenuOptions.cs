using System.Collections;
using System.ComponentModel;

namespace ShiftsLogger.Domain.Enums;

public enum MainMenuOptions
{
    [Description("Show All Shifts")]
    AllShifts,
    [Description("Shifts By User")]
    ShiftsByUser,
    [Description("Shifts By Shift Type")]
    ShiftsByType,
    [Description("Shifts By Location")]
    ShiftsByLocation,
    [Description("Exit")]
    Exit
}