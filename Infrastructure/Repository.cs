using Dapper;
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
        var sql = $@"SELECT boxId, name, size, SUBSTRING(description, 1, 50 ) AS description, price, boxImage, material FROM boxes.box;";
        using (var conn = _datasource.OpenConnection())
        {
            return conn.Query<Box>(sql);
        }
    }

    public Box PostBox(Box box)
    {
        var sql = $@"INSERT INTO 
                    boxes.box (name, size, description, price, boxImage, material)
                    VALUES (@name, @size, @description, @price, @boxImgUrl, @materials) 
                    RETURNING *;";
        using (var conn = _datasource.OpenConnection())
        {
            return conn.QueryFirst<Box>(sql, box);
        }
    }

    public Box UpdateBox(Box box, int boxId)
    {
        var sql = $@"UPDATE boxes.box 
                    SET name = @name, 
                        size = @size, 
                        description = @description, 
                        price = @price, 
                        boxImage = @boxImgUrl, 
                        material = @materials 
                    WHERE boxId = @boxId 
                    RETURNING *;";
        using (var conn = _datasource.OpenConnection())
        {
            return conn.QueryFirst<Box>(sql,
                new { box.name, box.size, box.description, box.price, box.boxImgUrl, box.materials, boxId });
        }
    }

    public void DeleteBox(int boxId)
    {
        var sql = $@"DELETE FROM boxes.box WHERE boxid = @boxId";
        using (var conn = _datasource.OpenConnection())
        {
            conn.Execute(sql, new { boxId });
        }
    }

    public IEnumerable<Box> searchBox(string searchTerm)
    {
        var sql = $@"SELECT * FROM boxes.box WHERE
                            name ILIKE '%' || @searchTerm || '%'
                            OR size ILIKE '%' || @searchTerm || '%'
                            OR material ILIKE '%' || @searchTerm || '%';";
        if (searchTerm.Length >= 4)
        {
            using (var conn = _datasource.OpenConnection())
            {
                return conn.Query<Box>(sql, new { searchTerm });
            }
        }

        throw new Exception("Search term not long enough.");
    }

    public Box getBox(int boxId)
    {
        var sql = $@"SELECT * FROM boxes.box WHERE boxid = @boxId";
        using (var conn = _datasource.OpenConnection())
        {
            return conn.QueryFirst<Box>(sql, new { boxId });
        }
    }
}