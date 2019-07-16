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
    public class CompanyProfileLogic:BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository):base(repository)
        { }

        public override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
          foreach (var poco in pocos)
            {
                if (poco.CompanyWebsite != null)
                {
                    string input = poco.CompanyWebsite;             
                    string lastWord = input.Split('.').Last();
                    //string[] words = input.Split(' ');
                    //string myLastWord = words[words.Length - 1];
                    if (!lastWord.Equals("ca") || !lastWord.Equals("com") || !lastWord.Equals("biz"))
                    {
                        exceptions.Add(new ValidationException(600, $"Valid websites must end with the following extensions – .ca, .com, .biz"));
                    }
                }

                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, $"PhoneNumber is required"));
                }
                else
                {                 
                
                    string[] phoneComponents = poco.ContactPhone.Split('-');
                    if (phoneComponents.Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, $"PhoneNumber is not in the required format."));
                    }
                    else
                    {
                        if (phoneComponents[0].Length != 3)
                        {
                            exceptions.Add(new ValidationException(601, $"PhoneNumber is not in the required format."));
                        }
                        else if (phoneComponents[1].Length != 3)
                        {
                            exceptions.Add(new ValidationException(601, $"PhoneNumber is not in the required format."));
                        }
                        else if (phoneComponents[2].Length != 4)
                        {
                            exceptions.Add(new ValidationException(601, $"PhoneNumber is not in the required format."));
                        }
                    }
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);           
            base.Add(pocos);
        }
        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
