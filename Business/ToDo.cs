using System;

namespace Business
{
    public class ToDo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        public ToDo()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}