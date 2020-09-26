using Lender.API.Models.Base;

namespace Lender.API.Models.Validators
{
    public interface IEntityValidator
    {
        void Validate(params Entity[] entities);
    }
}