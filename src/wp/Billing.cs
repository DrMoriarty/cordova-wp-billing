using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel.Store;
using WPCordovaClassLib.Cordova;
using WPCordovaClassLib.Cordova.Commands;
using WPCordovaClassLib.Cordova.JSON;

namespace ru.trilan.cordovawp
{
    public class Billing : BaseCommand
    {
        public async void purchase(string purchaseID)
        {
            ListingInformation products = await CurrentApp.LoadListingInformationByProductIdsAsync(new[] { purchaseID });
            ProductListing productListing = null;
            if (!products.ProductListings.TryGetValue(purchaseID, out productListing))
            {
                MessageBox.Show("Could not find product information");
                DispatchCommandResult(new PluginResult(PluginResult.Status.ERROR, "Could not find product information"));
                return;
            }
            string receipt = await CurrentApp.RequestProductPurchaseAsync(productListing.ProductId, true);
            ProductLicense productLicense = null;
            if (CurrentApp.LicenseInformation.ProductLicenses.TryGetValue(purchaseID, out productLicense))
            {
                if (productLicense.IsActive)
                {
                    CurrentApp.ReportProductFulfillment(purchaseID);
                    DispatchCommandResult(new PluginResult(PluginResult.Status.OK, receipt));
                    return;
                }
            }
            DispatchCommandResult(new PluginResult(PluginResult.Status.ERROR, "Product not purchased"));
        }

        public virtual void OnReset()
        {

        }
    }
}
