using DataLayer.Models;

namespace ServiceLayer.Interface
{
    public interface IShelf
    {
        public Task<Shelf> GetShelf(int Id);
        public Task<List<Shelf>> GetAll();
		public Task<List<Shelf>> GetAllArchive();
		public Task<Shelf> Edit(int Id , Shelf EditedShelf);
        public Task<Shelf> Delete(int Id);
		public Task<Shelf> Retrive(int Id);
		public Task<Shelf> DeletePermenetly(int Id);
		public Task<Shelf> Create(Shelf shelf);

    }
}
