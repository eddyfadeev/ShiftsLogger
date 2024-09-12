using Shared.Interfaces;
using Shared.Models.Entities;

namespace Shared.Models;

public class GenericReportModel<TEntity> where TEntity : class, IReportModel
{
    public TEntity Information { get; set; }
    
    public List<Shift> Shifts { get; set; }
}