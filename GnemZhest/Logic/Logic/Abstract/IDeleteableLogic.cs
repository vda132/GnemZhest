namespace Logic.Logic;

public interface IDeleteableLogic
{
    Task<bool> DeleteAsync(int id);
}

