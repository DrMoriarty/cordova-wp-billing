
var exec = require('cordova/exec');

module.exports = {
  purchase: function(purchaseID, success, fail) {
    cordova.exec(success, fail, "WPBilling", "purchase", purchaseID);
  }
}
