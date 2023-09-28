using EShop.Core.Entities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace EShop.Application.Validation
{

    public class GenerateSlip : IGenerateSlip
    {

        private readonly IConfiguration _configuration;

        public GenerateSlip(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool GeneratePdf(ShippingSlipEntity order)
        {
            string fileName = "test.pdf";
            string generatedSlipPath = _configuration["FilePath:GeneratedSlipPath"];

            string GeneratedFile = generatedSlipPath + fileName;

            // Create a new PDF document
            // var renderer = new ChromePdfRender();
            var Renderer = new ChromePdfRenderer(); // Instantiates Chrome Renderer

            string html = string.Empty;
            if (order.shipping != null && order.shipping.Count > 0)
            {
                html = GenerateShippingModal(order.shipping[0]);
            }
            foreach (var orderDetail in order.orderDetails)
            {
                html = html + GetOrderDetails(orderDetail);
            }

            var pdf = Renderer.RenderHtmlAsPdf(html);

            pdf.SaveAs(GeneratedFile);
            return true;
        }

        private string GetOrderDetails(OrderEntity orderDetail)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.Append("<div id='getOrderDetailModal' class='modal'>");
            htmlBuilder.Append("<div class='modal-content'>");
            htmlBuilder.Append("<div>");
            htmlBuilder.Append("<h2>OrderDetail</h2>");
            htmlBuilder.Append($"<p><strong>Product Name:</strong> {orderDetail.ProductName}</p>");
            htmlBuilder.Append($"<p><strong>Quantity:</strong> {orderDetail.Quantity}</p>");
            htmlBuilder.Append($"<p><strong>Total Amount:</strong> {orderDetail.TotalAmount}</p>");
            htmlBuilder.Append($"<p><strong>Customer Name:</strong> {orderDetail.CustomerName}</p>");
            htmlBuilder.Append($"<p><strong>Transaction Date:</strong> {orderDetail.TransactionDate.Value.ToString("yyyy-MM-dd")}</p>");
            htmlBuilder.Append("</div>");

            return htmlBuilder.ToString();
        }
        private string GenerateShippingModal(ShippingEntity shipping)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.Append("<div class='modal-content'>");
            htmlBuilder.Append("<h2>Shipping</h2>");
            htmlBuilder.Append($"<p><strong>Address Line 1:</strong> {shipping.AddressLine1}</p>");
            htmlBuilder.Append($"<p><strong>Address Line 2:</strong> {shipping.AddressLine2}</p>");
            htmlBuilder.Append($"<p><strong>City:</strong> {shipping.City}</p>");
            htmlBuilder.Append($"<p><strong>Country:</strong> {shipping.Country}</p>");
            htmlBuilder.Append($"<p><strong>Zip Code:</strong> {shipping.ZipCode}</p>");
            htmlBuilder.Append("</div>");

            return htmlBuilder.ToString();
        }

    }


}
