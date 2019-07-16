using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository):base(repository)
        {
        }
        public override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {

                if (poco.CompanyDescription != null && poco.CompanyDescription.Length < 3 )
                {
                    exceptions.Add(new ValidationException(107, $"Company Description must be greater than 2 characters"));
                }
                if(poco.CompanyName != null && poco.CompanyName.Length < 3 )
                {
                    exceptions.Add(new ValidationException(106, $"Comapny name must be grater than 2 characters"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);            
            base.Add(pocos);
        }
        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
