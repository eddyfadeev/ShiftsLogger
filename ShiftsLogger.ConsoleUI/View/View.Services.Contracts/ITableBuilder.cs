using Spectre.Console;
using View.Entity.Models;

namespace View.Services.Contracts;

public interface ITableBuilder
{
    Table Build<T>(TableData<T> tableData, ITableBuilderStrategy<T> strategy);
}