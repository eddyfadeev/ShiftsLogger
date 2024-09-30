using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public class ShiftsByUserCommand : ShiftsCommandBase<User>
{
    private readonly UserController _userController;

    public ShiftsByUserCommand(
        IRenderService renderService, 
        IPanelBuilder panelBuilder, 
        UserController userController
    ) : base(renderService, panelBuilder)
    {
        _userController = userController;
        
        Entries = PopulateEntities();
    }
    
    private protected sealed override List<User> PopulateEntities() =>
        _userController.GetAllUsers().Result;

    private protected sealed override void PopulateShifts(int entityId) =>
        ShiftsByEntity = _userController.GetShiftsByUserId(entityId).Result;
}