
var exec = require('cordova/exec');

module.exports = {
  purchase: function(purchaseID, success, fail) {
    cordova.exec(success, fail, "ru.trilan.cordovawp.Billing", "purchase", purchaseID);
  }
}
