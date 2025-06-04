using System.Collections.Generic;
using TravelJournal.Models;

namespace TravelJournal.Repositories;

public abstract class DataRepository<T> where T : ModelObject
{
    public abstract T FindById(int id);
    public abstract List<T> FindAll();
    public abstract void Insert(T item);
    public abstract void Update(T item);
    public abstract void Delete(int itemId);
}
