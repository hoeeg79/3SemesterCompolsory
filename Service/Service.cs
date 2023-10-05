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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.InnerException);
            Console.WriteLine(e.StackTrace);
            throw new Exception("Could not create box.");
        }
    }

    public Box UpdateBox(Box box, int id)
    {
        try
        {
            return _repository.UpdateBox(box, id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Could not update box");
        }
        throw new NotImplementedException();
    }

    public void DeleteBox(int boxId)
    {
        try
        {
            _repository.DeleteBox(boxId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new NotImplementedException();
        }
        
    }

    public IEnumerable<Box> SearchBox(string searchTerm)
    {
        try
        {
            return _repository.searchBox(searchTerm);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Could not search");
        }
        
    }

    public Box getBox(int boxId)
    {
        try
        {
            return _repository.getBox(boxId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("could not get box");
        }
    }
}