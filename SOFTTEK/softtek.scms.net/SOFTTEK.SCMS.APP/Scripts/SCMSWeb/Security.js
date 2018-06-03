

var SecuritySCMS = (function () {
    var _iv;
    var _key;

    function encrypt(plainText) {
        var cipherText = CryptoJS.AES.encrypt(plainText, _key, { iv: _iv });
        return cipherText.toString();
    }
    function decrypt(ciphertext) {
        var plainText = CryptoJS.AES.decrypt(ciphertext, _key, { iv: _iv });
        return plainText.toString();
    }
    function init(salt, key) {
        var saltBytes = CryptoJS.enc.Base64.parse(salt);
        var utf8Key = CryptoJS.enc.Utf8.parse(key);
        var normalizedKey = CryptoJS.SHA256(utf8Key);
        _iv = CryptoJS.PBKDF2(normalizedKey, saltBytes, { keySize: 128 / 32, iterations: 1000 });
        console.log(_iv.toString());
        _key = CryptoJS.PBKDF2(normalizedKey, saltBytes, { keySize: 256 / 32, iterations: 1000 });
        console.log(_key.toString());
    }
    function authenticate(app, credentials, callback) {
        var apiPath = '/api/security/';
        $.post('/' + app + apiPath, credentials, function (data) {
            callback(data);
        });
    }

    return {
        encrypt: encrypt,
        decrypt: decrypt,
        init: init,
        authenticate: authenticate
    }

})();

