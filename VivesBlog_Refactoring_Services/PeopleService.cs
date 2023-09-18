using VivesBlog_Core;
using VivesBlog_Models;
using System.Collections.Generic;
using System.Linq;


namespace VivesBlog_Services
{
    public class PeopleService
    {

        private readonly BlogDbContext _dbContext;

        public PeopleService(BlogDbContext dbContext) 
        { 
           _dbContext = dbContext;
        }

        public IList<Person> GetAll()
        {
            return _dbContext.People.ToList();
        }

        public Person GetPerson(int id)
        {
            return _dbContext.People.Find(id);
        }

        public Person Create(Person person)
        {
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
            return person;
        }

        public Person Update(Person person,int id) 
        { 
            var dbperson = _dbContext.People.Find(id);
            if (dbperson != null) 
            {
                return null;
            }

            dbperson.FirstName = person.FirstName;
            dbperson.LastName = person.LastName;
            dbperson.Articles = person.Articles;
            
            _dbContext.SaveChanges();
            return person;

        
        }

        public Person Delete(int id)
        {
            var person = new Person
            {
                Id = id,
                FirstName = string.Empty,
                LastName = string.Empty,
                Articles = new List<Article>(),

            };

            _dbContext.People.Attach(person);
            _dbContext.People.Remove(person);

            _dbContext.SaveChanges();

            return person;
        }


    }
}
