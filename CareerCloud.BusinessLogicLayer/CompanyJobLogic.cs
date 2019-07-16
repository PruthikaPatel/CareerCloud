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
   public class CompanyJobLogic:BaseLogic<CompanyJobPoco>
    {
        public CompanyJobLogic(IDataRepository<CompanyJobPoco> repository) : base(repository)
        { }
    }
}
