using API_Rest_Simple.Context;
using API_Rest_Simple.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Rest_Simple.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllPersonAsync();
        Task<Person> GetPersonByIdAsync(int id);
        Task AddPersonAsync(Person person);
        Task DeletePersonAsync(int id);
        Task UpdatePersonAsync(Person person);
        Task<IEnumerable<string>> GetAllPeople_Hardcoded(int cantidad = 0);
        Task<IEnumerable<Person>> GetAllPeople_with_Working_exp();
    }

    public class PersonRepository : IPersonRepository
    {
        private readonly appDbContext _context;

        private List<string> _people;

        public PersonRepository(appDbContext context)
        {
            _context = context;
        }

        public async Task AddPersonAsync(Person person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await _context.persons.FindAsync(id);
            if (person != null)
            {
                _context.persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Person>> GetAllPersonAsync()
        {
            return await _context.persons.ToListAsync();
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _context.persons.FindAsync(id);
        }

        public async Task UpdatePersonAsync(Person person)
        {
            var _person = await GetPersonByIdAsync(person.id);
            if (_person != null)
            {
                _context.Entry(_person).CurrentValues.SetValues(person);
                await _context.SaveChangesAsync();
            }
        }

        private void initialize_people_list()
        {
            //_people = new List<string>();
            _people = new List<string>
            {
                "Jhon",
                "Jack",
                "Lisa",
                "Bart",
                "Homero",
                "Marge",
                "Superman",
                "Batman",
                "Darth Vader"
            };
        }



        public async Task<IEnumerable<String>> GetAllPeople_Hardcoded(int cantidad)
        {
            initialize_people_list();
            return _people.Take(cantidad).ToList();

        }

        public async Task<IEnumerable<Person>> GetAllPeople_with_Working_exp()
        {
            return await _context.persons.Include(p => p.professional_exp).ToListAsync();
        }
    }
}
