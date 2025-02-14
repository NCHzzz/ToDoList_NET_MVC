using Entities;
using UseCases;

namespace Infrastructure
{
    public class InMemoryToDoItemRepository : IToDoItemRepository
    {
        private readonly List<ToDoItem> _items;
        private int _nextId = 1; // Initialize next ID to 1

        public InMemoryToDoItemRepository()
        {
            _items = new List<ToDoItem>();
        }

        public void Add(ToDoItem item)
        {
            item.Id = _nextId++; // Assign a unique ID
            _items.Add(item);
        }

        public void Delete(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _items.Remove(item);
            }
        }

        public ToDoItem? GetById(int id)
        {
            return _items.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<ToDoItem> GetItems()
        {
            return _items;
        }

        public void Update(ToDoItem item)
        {
            var existingItem = GetById(item.Id);
            if (existingItem != null)
            {
                existingItem.Text = item.Text;
                existingItem.IsCompleted = item.IsCompleted;
            }
        }
    }
}
