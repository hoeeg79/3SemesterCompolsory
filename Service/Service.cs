using Infrastructure;
using Infrastructure.Models;

namespace service;

public class Service
{
    private readonly Repository _repository;

    public Service(Repository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Box> GetAllBoxes()
    {
        try
        {
            return _repository.GetAllBoxes();
        }
        catch (Exception)
        {
            throw new Exception("Could not get boxes.");
        }
    }

    public Box PostBox(Box box)
    {
        try
        {
            return _repository.PostBox(box);
        }
        catch (Exception)
        {
            throw new Exception("Could not create box.");
        }
    }
}