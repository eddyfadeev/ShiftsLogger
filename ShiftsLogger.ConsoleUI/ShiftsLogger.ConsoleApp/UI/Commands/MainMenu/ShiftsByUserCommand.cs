using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.ConsoleApp.UI.Extensions;
using ShiftsLogger.ConsoleApp.UI.Models;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.UI.Commands.MainMenu;

public sealed class ShiftsByUserCommand : DoublePanelMenuCommand
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

    private protected override IEnumerable<UserViewEntity> FetchLeftPanelData() =>
        _userController.GetAllUsers().Result.Select(u => u.MapToViewEntity());

    private protected override IEnumerable<ShiftViewEntity> FetchRightPanelData(int entityId) =>
        _userController.GetShiftsByUserId(entityId).Result.Select(s => s.MapToViewEntity());
}