using Spectre.Console;
using View.Entity;

namespace View.Contracts;

public interface ITableBuilder
{
    Table Build<T>(TableData<T> tableData);
}