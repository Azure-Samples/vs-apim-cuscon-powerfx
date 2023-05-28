using System.Net.Http.Json;

using Microsoft.AspNetCore.Components;

using Microsoft.OpenApi.Readers;
using Microsoft.PowerFx;
using Microsoft.PowerFx.Types;

namespace DashboardApp.Pages
{
    public partial class Index : ComponentBase
    {
        private Product[]? productList;

        private Dictionary<string, string> powerFxColumns = new Dictionary<string, string>();

        private bool showDialog = false;
        private string? columnName;
        private string? columnExpression;

        // ⬇️⬇️⬇️ Uncomment: Power Fx interpreter and type marshaller members
        //private RecalcEngine? engine;

        //private TypeMarshallerCache? cache;
        //private ITypeMarshaller? productType;
        // ⬆️⬆️⬆️ Uncomment: Power Fx interpreter and type marshaller members

        private string errors = string.Empty;

        [Inject]
        public HttpClient Http { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            productList = await Http.GetFromJsonAsync<Product[]>("sample-data/products.json");

            // ⬇️⬇️⬇️ Uncomment: Initialize Power Fx interpreter RecalcEngine
            //var config = new PowerFxConfig(Features.PowerFxV1);
            //engine = new RecalcEngine(config);
            // ⬆️⬆️⬆️ Uncomment: Initialize Power Fx interpreter RecalcEngine

            // ⬇️⬇️⬇️ Uncomment: Initialize out type marshaller cache
            //cache = new TypeMarshallerCache();
            //productType = cache.GetMarshaller(typeof(Product));
            // ⬆️⬆️⬆️ Uncomment: Initialize out type marshaller cache

            // ⬇️⬇️⬇️ Uncomment: Create strongly-typed Power Fx objects from the swagger
            //var swagger = await Http.GetStringAsync("https://localhost:5051/swagger/v1/swagger.json");
            //var openAPIDoc = new OpenApiStringReader().Read(swagger, out OpenApiDiagnostic diag);
            //var client = new HttpClient { BaseAddress = new Uri("https://localhost:5051") };

            //// Add the service to the Power Fx configuration as "InventoryAPI_Connector"
            //config.AddService("InventoryAPI_Connector", openAPIDoc, client);
            // ⬆️⬆️⬆️ Uncomment: Create strongly-typed Power Fx objects from the swagger

            // Sum( InventoryAPI_Connector.GetItemsOnHand( ItemId ), numberInStock )
        }

        protected async Task AddColumn(string name, string expression)
        {
            errors = string.Empty;

            if (!powerFxColumns.ContainsKey(name))
            {
                foreach (var product in productList)
                {
                    // ⬇️⬇️⬇️ Comment: When to use PowerFx
                    var value = string.Empty;
                    // ⬆️⬆️⬆️ Comment: When to use PowerFx

                    // ⬇️⬇️⬇️ Uncomment: Evaluate the expression for each product
                    //var value = (await engine.EvalAsync(
                    //    expression,
                    //    cancellationToken: default,
                    //    productType.Marshal(product) as RecordValue
                    //)).ToObject().ToString();
                    // ⬆️⬆️⬆️ Uncomment: Evaluate the expression for each product

                    product.CalculatedColumns.Add(name, value);
                }

                powerFxColumns.Add(name, expression);
            }
            else
            {
                errors = $"Column '{name}' already exists. Choose a different name or remove the existing column.";
            }

            CloseDialog();
        }

        protected void ShowDialog()
        {
            showDialog = true;
        }

        protected void CloseDialog()
        {
            showDialog = false;
            errors = string.Empty;
        }

        protected void RemoveColumn(string name)
        {
            powerFxColumns.Remove(name);

            foreach (var product in productList)
            {
                product.CalculatedColumns.Remove(name);
            }
        }

        protected Task AddColumn()
        {
            return AddColumn(columnName, columnExpression);
        }

        public class Product
        {
            public string? ItemId { get; set; }

            public string? Name { get; set; }

            public string? CategoryId { get; set; }

            public Decimal Price { get; set; }

            public string? Description { get; set; }

            public Dictionary<string, string> CalculatedColumns { get; set; } = new Dictionary<string, string>();
        }
    }
}