using Spectre.Console;
using View.ViewModel;

namespace View.Contracts.Services;

public interface ITableBuilder
{
    Table Build<T>(TableData<T> tableData);
}