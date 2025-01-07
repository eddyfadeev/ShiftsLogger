using Spectre.Console;
using View.Entity.Models;

namespace View.Services.Contracts;

public interface ITableBuilderStrategy<T>
{
    Table Build(TableData<T> tableData);
}