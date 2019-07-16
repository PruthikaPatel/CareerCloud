using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public abstract class SystemBaseLogic<TPoco>       
    {
        public IDataRepository<TPoco> _repository;
        public SystemBaseLogic(IDataRepository<TPoco> repository)
        {
            _repository = repository;
        }
        public virtual void Verify(TPoco[] pocos)
        {
            return;
        }
        public virtual void Add(TPoco[] pocos)
        {
            _repository.Add(pocos);
        }
        public virtual TPoco Get(string id)
        {
            return _repository.GetSingle(c => c.ToString() == id);
        }
        public virtual List<TPoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public virtual void Update(TPoco[] pocos)
        {
            _repository.Update(pocos);
        }
        public void Delete(TPoco[] pocos)
        {
            _repository.Remove(pocos);
        }
    }
}
