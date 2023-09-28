using EShop.Core.Entities;

namespace EShop.Application.Validation
{
    public interface IGenerateSlip
    {
        bool GeneratePdf(ShippingSlipEntity order);

    }
}
