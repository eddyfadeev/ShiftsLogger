using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public class ShiftsByUserCommand : ShiftsCommandBase<User, Shift>
{
    private readonly UserController _userController;

    public ShiftsByUserCommand(
        IRenderService renderService, 
        IPanelBuilderService panelBuilderService, 
        UserController userController
    ) : base(renderService, panelBuilderService)
    {
        _userController = userController;
        
        LeftPanelEntities = FetchLeftPanelData();
    }
    
    private protected sealed override List<User> FetchLeftPanelData() =>
        _userController.GetAllUsers().Result;

    private protected sealed override void FetchRightPanelData(int entityId) =>
        RightPanelEntries = _userController.GetShiftsByUserId(entityId).Result;
}