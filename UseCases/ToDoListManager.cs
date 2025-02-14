using Entities;

namespace UseCases
{
    public class ToDoListManager(IToDoItemRepository repository)
    {
        private readonly IToDoItemRepository repository = repository;

        public IEnumerable<ToDoItem> GetToDoItems()
        {
            return repository.GetItems();
        }
        public void AddToDoItem(ToDoItem item)
        {
            repository.Add(item);
        }
        public void MarkComplete(int id)
        {
            var item = repository.GetById(id);
            if (item != null)
            {
                item.IsCompleted = !item.IsCompleted; // Toggle completion status
                repository.Update(item);
            }
        }
        public void Delete(int id)
        {
            var item = repository.GetById(id);
            if (item != null)
            {
                repository.Delete(id);
            }
        }
    }
}
