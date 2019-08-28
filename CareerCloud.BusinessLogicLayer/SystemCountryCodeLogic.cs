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
    public class SystemCountryCodeLogic 
    {

        private IDataRepository<SystemCountryCodePoco> _repository;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
        {
            _repository = repository;
        }

        public SystemCountryCodePoco Get(string code)
        {
            return _repository.GetSingle(c => c.Code == code);
        }

        public List<SystemCountryCodePoco> Getall()
        {
            IList<SystemCountryCodePoco> pocos = _repository.GetAll();
            return pocos.ToList();
        }

        public void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {

                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptions.Add(new ValidationException(900, $"Code can not b e empty"));
                }
                if(string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(901, $"Name can not be empty"));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);           
            _repository.Add(pocos);
        }
        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }
    }
}
