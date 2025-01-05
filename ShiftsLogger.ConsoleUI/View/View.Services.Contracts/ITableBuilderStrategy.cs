using Spectre.Console;
using View.Entity;

namespace View.Services.Contracts;

public interface ITableBuilderStrategy<T>
{
    Table Build(TableData<T> tableData);
}