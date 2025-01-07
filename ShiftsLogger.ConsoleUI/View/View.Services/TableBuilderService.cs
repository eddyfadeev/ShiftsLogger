using Spectre.Console;
using View.Entity.Models;
using View.Services.Contracts;

namespace View.Services;

public class TableBuilderService : ITableBuilder
{
    public Table Build<T>(TableData<T> tableData, ITableBuilderStrategy<T> strategy) =>
        strategy.Build(tableData);
}