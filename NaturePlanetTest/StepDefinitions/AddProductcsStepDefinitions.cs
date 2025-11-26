using DataAccessLayer.Context;
using DataAccessLayer.Model;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using System.Threading.Tasks;

using Xunit;


namespace Test.StepDefinitions
{
    [Binding]
    public class ProductCreateStepDefinitions
    {
        private readonly DatabaseContext _context;
        private readonly ProductRepository _repo;

        private DALProduct _product;

        public ProductCreateStepDefinitions()
        {
            _context = TestServiceProvider.Instance.GetRequiredService<DatabaseContext>();
            _repo = new ProductRepository(_context);
        }

        [Given(@"an admin wants to create a product with name ""(.)"" and weight ""(.)""")]
        public void GivenAdminWantsToCreateProduct(string name, string weight)
        {
            _product = new DALProduct
            {
                Name = name,
                Weight = int.Parse(weight)
            };
        }

        [When("the admin submits the product creation")]
        public async Task WhenAdminSubmitsCreation()
        {
            _repo.CreateProduct(_product);
        }

        [Then(@"the product should exist in the database with name ""(.*)""")]
        public void ThenProductShouldExist(string name)
        {
            var p = _context.Products.FirstOrDefault(x => x.Name == name);
            Assert.IsNotNull(p);
        }
    }
}