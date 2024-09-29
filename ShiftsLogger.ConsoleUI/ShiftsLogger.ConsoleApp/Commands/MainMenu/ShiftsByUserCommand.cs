using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public class ShiftsByUserCommand : ShiftsCommandBase<User>
{
    private readonly UserController _userController;

    public ShiftsByUserCommand(
        IRenderService renderService, 
        IPanelBuilderService panelBuilderService, 
        UserController userController
    ) : base(renderService, panelBuilderService)
    {
        _userController = userController;
        
        Entities = PopulateEntities();
        ShiftsByEntity = PopulateShifts();
    }
    
    private protected sealed override List<User> PopulateEntities() =>
        _userController.GetAllUsers().Result;

    private protected sealed override Dictionary<User, List<Shift>> PopulateShifts()
    {
        var result = new Dictionary<User, List<Shift>>();

        foreach (var user in Entities)
        {
            result.Add(
                user, _userController.GetShiftsByUserId(user.Id).Result);
        }

        return result;
    }
}