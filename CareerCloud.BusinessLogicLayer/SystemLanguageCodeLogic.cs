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
    public class SystemLanguageCodeLogic : SystemBaseLogic<SystemLanguageCodePoco>
    {
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository):base(repository)
        {

        }
        public override void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {

                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000, $"Language ID can not be empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(1001, $"Name can not be empty"));
                }
                if (string.IsNullOrEmpty(poco.NativeName))
                {
                    exceptions.Add(new ValidationException(1002, $"Native name can not be empty"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);          
            base.Add(pocos);
        }
        public override void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
