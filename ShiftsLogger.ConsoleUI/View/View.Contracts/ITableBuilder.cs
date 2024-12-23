using Spectre.Console;

namespace View.Contracts;

public interface ITableBuilder
{
    Table Build<T>(params List<T> tableData);
}