using Infrastructure.Models;
using Npgsql;

namespace Infrastructure;

public class Repository
{
    private readonly NpgsqlDataSource _datasource;

    public Repository(NpgsqlDataSource datasource)
    {
        _datasource = datasource;
    }

    public IEnumerable<Box> GetAllBoxes()
    {
        throw new NotImplementedException();
    }

    public Box PostBox(Box box)
    {
        throw new NotImplementedException();
    }
}